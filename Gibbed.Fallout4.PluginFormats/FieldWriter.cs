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
using System.Text;
using Gibbed.IO;

namespace Gibbed.Fallout4.PluginFormats
{
    internal class FieldWriter : IFieldWriter
    {
        protected static readonly Encoding Encoding;

        static FieldWriter()
        {
            Encoding = Encoding.GetEncoding(1252);
        }

        #region Fields
        private readonly ushort _Version;
        private readonly bool _IsLocalized;
        private readonly MemoryStream _Stream;
        private readonly Endian _Endian;
        #endregion

        public FieldWriter(ushort version, bool isLocalized, MemoryStream stream, Endian endian)
        {
            this._Version = version;
            this._IsLocalized = isLocalized;
            this._Stream = stream;
            this._Endian = endian;
        }

        #region Properties
        public ushort Version
        {
            get { return this._Version; }
        }

        public void WriteValueB8(bool value)
        {
            this._Stream.WriteValueB8(value);
        }

        public void WriteValueU8(byte value)
        {
            this._Stream.WriteValueU8(value);
        }

        public void WriteValueS32(int value)
        {
            this._Stream.WriteValueS32(value, this._Endian);
        }

        public void WriteValueU32(uint value)
        {
            this._Stream.WriteValueU32(value, this._Endian);
        }

        public void WriteValueU64(ulong value)
        {
            this._Stream.WriteValueU64(value, this._Endian);
        }

        public void WriteValueF32(float value)
        {
            this._Stream.WriteValueF32(value, this._Endian);
        }

        public void WriteString(string value)
        {
            this._Stream.WriteStringZ(value, Encoding);
        }

        public void WriteLocalizedString(LocalizedString value)
        {
            if (this._IsLocalized == false)
            {
                this.WriteString(value.Text);
            }
            else
            {
                if (value.Id.HasValue == false)
                {
                    throw new InvalidOperationException();
                }

                this.WriteValueU32(value.Id.Value);
            }
        }
        #endregion
    }
}
