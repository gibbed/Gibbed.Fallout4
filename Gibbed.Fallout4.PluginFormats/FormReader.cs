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
    internal class FormReader : IFormReader, IDisposable
    {
        private readonly ushort _Version;
        private readonly bool _IsLocalized;
        private readonly byte[] _Bytes;
        private readonly MemoryStream _Stream;
        private readonly Endian _Endian;

        public FormReader(ushort version, bool isLocalized, byte[] bytes, Endian endian)
        {
            this._Version = version;
            this._IsLocalized = isLocalized;
            this._Bytes = bytes;
            this._Stream = new MemoryStream(bytes, false);
            this._Endian = endian;
        }

        public ushort Version
        {
            get { return this._Version; }
        }

        public IFieldReader ReadField(out uint type)
        {
            var data = this._Stream;
            var endian = this._Endian;

            if (data.Position == data.Length)
            {
                type = 0;
                return null;
            }

            if (data.Position + 6 > data.Length)
            {
                throw new FormatException();
            }

            var fieldType = data.ReadValueU32(endian);
            int fieldSize = data.ReadValueU16(endian);

            if (fieldType == 0x58585858) // 'XXXX'
            {
                if (fieldSize != 4)
                {
                    throw new FormatException();
                }

                fieldSize = data.ReadValueS32(endian);
                fieldType = data.ReadValueU32(endian);

                var dummyFieldSize = data.ReadValueU16(endian);
                if (dummyFieldSize != 0)
                {
                    throw new FormatException();
                }
            }

            type = fieldType;
            var reader = new FieldReader(
                this._Version,
                this._IsLocalized,
                this._Bytes,
                (int)data.Position,
                fieldSize,
                endian);
            data.Position += fieldSize;
            return reader;
        }

        public void Dispose()
        {
        }
    }
}
