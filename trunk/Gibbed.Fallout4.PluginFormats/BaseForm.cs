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

namespace Gibbed.Fallout4.PluginFormats
{
    public abstract class BaseForm
    {
        #region Fields
        private uint _Flags;
        private uint _Id;
        private uint _Revision;
        private ushort _Unknown;
        #endregion

        protected bool HasFlag(uint flag)
        {
            return (this._Flags & flag) == flag;
        }

        protected void SetFlag(uint flag, bool set)
        {
            if (set == true)
            {
                this.Flags |= flag;
            }
            else
            {
                this.Flags &= ~flag;
            }
        }

        #region Properties
        public abstract FormType Type { get; }
        public abstract ushort Version { get; }

        public uint Flags
        {
            get { return this._Flags; }
            set { this._Flags = value; }
        }

        public bool IsCompressed
        {
            get { return this.HasFlag(BaseFormFlags.IsCompressed); }
            set { this.SetFlag(BaseFormFlags.IsCompressed, value); }
        }

        public uint Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }

        public uint Revision
        {
            get { return this._Revision; }
            set { this._Revision = value; }
        }

        public ushort Unknown
        {
            get { return this._Unknown; }
            set { this._Unknown = value; }
        }
        #endregion

        public void Serialize(Stream output, Endian endian, bool isLocalized)
        {
            using (var data = new MemoryStream())
            {
                var writer = new FormWriter(this.Version, isLocalized, data, endian);
                this.WriteFields(writer);
                data.Flush();
                data.Position = 0;
                var size = (uint)data.Length;

                output.WriteValueU32((uint)this.Type, endian);
                output.WriteValueU32((uint)data.Length, endian);
                output.WriteValueU32(this._Flags, endian);
                output.WriteValueU32(this._Id, endian);
                output.WriteValueU32(this._Revision, 0);
                output.WriteValueU16(this.Version, endian);
                output.WriteValueU16(0, endian);

                output.WriteFromStream(data, size);
            }
        }

        public void Deserialize(Stream input, Endian endian, bool isLocalized)
        {
            var type = (FormType)input.ReadValueU32(endian);
            var size = input.ReadValueU32(endian);
            var flags = input.ReadValueU32(endian);
            var id = input.ReadValueU32(endian);
            var revision = input.ReadValueU32(endian);
            var version = input.ReadValueU16(endian);
            var unknown = input.ReadValueU16(endian);

            if (type != this.Type)
            {
                throw new FormatException();
            }

            this._Flags = flags;
            this._Id = id;
            this._Revision = revision;
            this._Unknown = unknown;

            byte[] bytes;
            if ((flags & BaseFormFlags.IsCompressed) == 0)
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

        internal abstract void ReadFields(IFormReader reader);
        internal abstract void WriteFields(IFormWriter writer);
    }
}
