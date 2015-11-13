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

namespace Gibbed.Fallout4.FileFormats
{
    public abstract class ArchiveFile
    {
        public const uint Signature = 0x58445442; // 'BTDX'

        private readonly System.Text.Encoding _Encoding;
        private readonly ArchiveType _Type;
        private Endian _Endian;

        public ArchiveFile(ArchiveType type)
        {
            this._Encoding = System.Text.Encoding.GetEncoding(1252);
            this._Type = type;
        }

        public System.Text.Encoding Encoding
        {
            get { return this._Encoding; }
        }

        public Endian Endian
        {
            get { return this._Endian; }
            set { this._Endian = value; }
        }

        public virtual void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public virtual void Deserialize(Stream input)
        {
            var magic = input.ReadValueU32(Endian.Little);
            if (magic != Signature && magic.Swap() != Signature)
            {
                throw new FormatException();
            }
            var endian = magic == Signature ? Endian.Little : Endian.Big;

            var version = input.ReadValueU32(endian);
            if (version != 1)
            {
                throw new FormatException();
            }

            var type = (ArchiveType)input.ReadValueU32(endian);
            if (type != this._Type)
            {
                throw new FormatException();
            }
        }

        public static ArchiveType ReadType(Stream input)
        {
            var magic = input.ReadValueU32(Endian.Little);
            if (magic != Signature && magic.Swap() != Signature)
            {
                throw new FormatException();
            }
            var endian = magic == Signature ? Endian.Little : Endian.Big;

            var version = input.ReadValueU32(endian);
            if (version != 1)
            {
                throw new FormatException();
            }

            return (ArchiveType)input.ReadValueU32(endian);
        }
    }
}
