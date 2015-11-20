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
    internal class FieldReader : IFieldReader
    {
        protected static readonly Encoding Encoding;

        static FieldReader()
        {
            Encoding = Encoding.GetEncoding(1252);
        }

        #region Fields
        private readonly ushort _Version;
        private readonly bool _IsLocalized;
        private readonly byte[] _Bytes;
        private readonly MemoryStream _Stream;
        private readonly Endian _Endian;
        #endregion

        public FieldReader(ushort version, bool isLocalized, byte[] bytes, int offset, int count, Endian endian)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            this._Version = version;
            this._IsLocalized = isLocalized;
            this._Bytes = bytes;
            this._Stream = new MemoryStream(bytes, offset, count);
            this._Endian = endian;
        }

        #region Properties
        public ushort Version
        {
            get { return this._Version; }
        }

        public int Size
        {
            get { return (int)this._Stream.Length; }
        }

        public void SkipBytes(int count)
        {
            this._Stream.Position += count;
        }

        public bool ReadValueB8()
        {
            return this._Stream.ReadValueB8();
        }

        public byte ReadValueU8()
        {
            return this._Stream.ReadValueU8();
        }

        public ushort ReadValueU16()
        {
            return this._Stream.ReadValueU16(this._Endian);
        }

        public int ReadValueS32()
        {
            return this._Stream.ReadValueS32(this._Endian);
        }

        public uint ReadValueU32()
        {
            return this._Stream.ReadValueU32(this._Endian);
        }

        public float ReadValueF32()
        {
            return this._Stream.ReadValueF32(this._Endian);
        }

        public string ReadString(int maximumCapacity)
        {
            return this._Stream.ReadStringZ(Encoding);
        }

        public LocalizedString ReadLocalizedString()
        {
            if (this._IsLocalized == false)
            {
                var text = this._Stream.ReadStringZ(Encoding);
                return new LocalizedString(text);
            }
            else
            {
                var id = this._Stream.ReadValueU32(this._Endian);
                return new LocalizedString(id);
            }
        }

        public byte[] ReadBytes(int count)
        {
            return this._Stream.ReadBytes(count);
        }

        public T ReadObject<T>() where T : Field, new()
        {
            var instance = new T();
            instance.Read(this);
            return instance;
        }
        #endregion

        public void Dispose()
        {
        }
    }
}
