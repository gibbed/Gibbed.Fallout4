/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.IO;
using Gibbed.IO;
using InflaterInputStream = ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream;
using System.Collections.Generic;

namespace Gibbed.Fallout4.PluginFormats
{
    public class RawForm
    {
        private FormType _Type;
        private FormFlags _Flags;
        private uint _Id;
        private uint _Revision;
        private ushort _Version;
        private ushort _Unknown;

        private string _EditorId;
        private readonly List<Tuple<uint, byte[]>> _Fields;

        public RawForm()
        {
            this._Type = FormType.NONE;
            this._Fields = new List<Tuple<uint, byte[]>>();
        }

        public FormType Type
        {
            get { return this._Type; }
        }

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            // ReSharper restore InconsistentNaming
        }

        public void Serialize(Stream output, Endian endian, bool isLocalized)
        {
            using (var data = new MemoryStream())
            {
                var writer = new FormWriter(this._Version, isLocalized, data, endian);
                this.WriteFields(writer);
                data.Flush();
                data.Position = 0;
                var size = (uint)data.Length;

                output.WriteValueU32((uint)this.Type, endian);
                output.WriteValueU32((uint)data.Length, endian);
                output.WriteValueU32((uint)this._Flags, endian);
                output.WriteValueU32(this._Id, endian);
                output.WriteValueU32(this._Revision, 0);
                output.WriteValueU16(this._Version, endian);
                output.WriteValueU16(0, endian);

                output.WriteFromStream(data, size);
            }
        }

        public void Deserialize(Stream input, Endian endian, bool isLocalized)
        {
            var type = (FormType)input.ReadValueU32(endian);
            var size = input.ReadValueU32(endian);
            var flags = (FormFlags)input.ReadValueU32(endian);
            var id = input.ReadValueU32(endian);
            var revision = input.ReadValueU32(endian);
            var version = input.ReadValueU16(endian);
            var unknown = input.ReadValueU16(endian);

            this._Type = type;
            this._Flags = flags & ~FormFlags.IsCompressed;
            this._Id = id;
            this._Revision = revision;
            this._Unknown = unknown;

            byte[] bytes;
            if ((flags & FormFlags.IsCompressed) == 0)
            {
                bytes = input.ReadBytes(size);
            }
            else
            {
                if (size < 4)
                {
                    throw new FormatException();
                }

                var uncompressedSize = input.ReadValueU32(endian);
                var zlib = new InflaterInputStream(input);
                bytes = zlib.ReadBytes(uncompressedSize);
            }

            using (var reader = new FormReader(version, isLocalized, bytes, endian))
            {
                this.ReadFields(reader);
            }
        }

        private void ReadFields(IFormReader reader)
        {
            while (true)
            {
                uint fieldType;
                using (var fieldReader = reader.ReadField(out fieldType))
                {
                    if (fieldReader == null)
                    {
                        break;
                    }

                    this.ReadField(fieldType, fieldReader);
                }
            }
        }

        private void ReadField(uint type, IFieldReader reader)
        {
            var size = reader.Size;
            switch ((FieldType)type)
            {
                case FieldType.EDID:
                {
                    this._EditorId = reader.ReadString(size);
                    break;
                }

                default:
                {
                    this._Fields.Add(new Tuple<uint, byte[]>(type, reader.ReadBytes(size)));
                    break;
                }
            }
        }

        internal void WriteFields(IFormWriter writer)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
