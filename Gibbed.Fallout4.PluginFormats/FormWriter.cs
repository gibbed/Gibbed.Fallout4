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

namespace Gibbed.Fallout4.PluginFormats
{
    internal class FormWriter : IFormWriter
    {
        private readonly ushort _Version;
        private readonly bool _IsLocalized;
        private readonly MemoryStream _Stream;
        private readonly Endian _Endian;

        public FormWriter(ushort version, bool isLocalized, MemoryStream stream, Endian endian)
        {
            this._Version = version;
            this._IsLocalized = isLocalized;
            this._Stream = stream;
            this._Endian = endian;
        }

        public ushort Version
        {
            get { return this._Version; }
        }

        private void WriteFieldHeader(uint type, uint size)
        {
            var endian = this._Endian;
            var output = this._Stream;

            if (size > ushort.MaxValue)
            {
                output.WriteValueU32(0x58585858, endian);
                output.WriteValueU16(4, endian);
                output.WriteValueU32(size, endian);
                output.WriteValueU32(type, endian);
                output.WriteValueU16(0, endian);
            }
            else
            {
                output.WriteValueU32(type, endian);
                output.WriteValueU16((ushort)size, endian);
            }
        }

        private void WriteGeneric(uint type, Action<IFieldWriter> writeAction)
        {
            var output = this._Stream;
            var endian = this._Endian;

            using (var data = new MemoryStream())
            {
                var writer = new FieldWriter(this._Version, this._IsLocalized, data, endian);
                writeAction(writer);
                data.Flush();
                var size = (uint)data.Length;
                WriteFieldHeader(type, size);
                data.Position = 0;
                output.WriteFromStream(data, size);
            }
        }

        public void WriteValueS32(uint type, int value)
        {
            this.WriteGeneric(type, w => w.WriteValueS32(value));
        }

        public void WriteValueU32(uint type, uint value)
        {
            this.WriteGeneric(type, w => w.WriteValueU32(value));
        }

        public void WriteValueU64(uint type, ulong value)
        {
            this.WriteGeneric(type, w => w.WriteValueU64(value));
        }

        public void WriteValueF32(uint type, float value)
        {
            this.WriteGeneric(type, w => w.WriteValueF32(value));
        }

        public void WriteString(uint type, string value)
        {
            this.WriteGeneric(type, w => w.WriteString(value));
        }

        public void WriteString(uint type, string value, int maximumLength)
        {
            this.WriteString(type, value);
        }

        public void WriteLocalizedString(uint type, LocalizedString value)
        {
            this.WriteGeneric(type, w => w.WriteLocalizedString(value));
        }

        public void WriteData(uint type, Action<IFieldWriter> writeAction)
        {
            this.WriteGeneric(type, writeAction);
        }

        public void WriteObject<T>(uint type, T instance) where T : Field
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            this.WriteGeneric(type, instance.Write);
        }
    }
}
