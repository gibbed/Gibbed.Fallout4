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
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.IO;

namespace Gibbed.Fallout4.FileFormats
{
    public class TextureDX10ArchiveFile : ArchiveFile
    {
        private readonly List<Entry> _Entries;

        public TextureDX10ArchiveFile()
            : base(ArchiveType.TextureDX10)
        {
            this._Entries = new List<Entry>();
        }

        public List<Entry> Entries
        {
            get { return this._Entries; }
        }

        public override void Deserialize(Stream input)
        {
            var basePosition = input.Position;
            base.Deserialize(input);
            var endian = this.Endian;

            var entryCount = input.ReadValueS32(endian);
            var entryNameTableOffset = input.ReadValueS64(endian);

            var rawEntries = new RawEntry[entryCount];
            for (int i = 0; i < entryCount; i++)
            {
                rawEntries[i] = RawEntry.Read(input, endian);
            }

            var entryNames = new string[entryCount];
            if (entryCount > 0)
            {
                input.Position = basePosition + entryNameTableOffset;
                for (int i = 0; i < entryCount; i++)
                {
                    var nameLength = input.ReadValueU16(endian);
                    var name = input.ReadString(nameLength, Encoding.ASCII);
                    entryNames[i] = name;
                }
            }

            // we're assuming the entry names match up in order with the entries
            var entries = new Entry[entryCount];
            for (int i = 0; i < entryCount; i++)
            {
                var rawEntry = rawEntries[i];
                var rawMipMaps = rawEntry.MipMaps;

                var mipMaps = new MipMap[rawMipMaps.Length];
                for (int j = 0; j < rawMipMaps.Length; j++)
                {
                    var rawMipMap = rawMipMaps[j];
                    mipMaps[j] = new MipMap()
                    {
                        DataOffset = rawMipMap.DataOffset,
                        DataCompressedSize = rawMipMap.DataCompressedSize,
                        DataUncompressedSize = rawMipMap.DataUncompressedSize,
                        IndexStart = rawMipMap.IndexStart,
                        IndexEnd = rawMipMap.IndexEnd,
                    };
                }

                entries[i] = new Entry()
                {
                    Name = entryNames[i],
                    NameHash = rawEntry.NameHash,
                    Type = rawEntry.Type,
                    DirectoryNameHash = rawEntry.DirectoryNameHash,
                    Unknown0C = rawEntry.Unknown0C,
                    Width = rawEntry.Width,
                    Height = rawEntry.Height,
                    MipMapCount = rawEntry.MipMapCount,
                    Format = rawEntry.Format,
                    Unknown16 = rawEntry.Unknown16,
                    Unknown17 = rawEntry.Unknown17,
                    MipMaps = mipMaps,
                };
            }

            this._Entries.Clear();
            this._Entries.AddRange(entries);
        }

        public struct Entry
        {
            public string Name;
            public uint NameHash;
            public uint Type;
            public uint DirectoryNameHash;
            public byte Unknown0C;
            public ushort Width;
            public ushort Height;
            public byte MipMapCount;
            public byte Format;
            public byte Unknown16;
            public byte Unknown17;
            public MipMap[] MipMaps;
        }

        public struct MipMap
        {
            public long DataOffset;
            public uint DataCompressedSize;
            public uint DataUncompressedSize;
            public ushort IndexStart;
            public ushort IndexEnd;
        }

        private struct RawEntry
        {
            public uint NameHash;
            public uint Type;
            public uint DirectoryNameHash;
            public byte Unknown0C;
            public ushort Width;
            public ushort Height;
            public byte MipMapCount;
            public byte Format;
            public byte Unknown16;
            public byte Unknown17;
            public RawMipMap[] MipMaps;

            internal static RawEntry Read(Stream input, Endian endian)
            {
                RawEntry instance;
                instance.NameHash = input.ReadValueU32(endian);
                instance.Type = input.ReadValueU32(endian);
                instance.DirectoryNameHash = input.ReadValueU32(endian);
                instance.Unknown0C = input.ReadValueU8();
                var entryCount = input.ReadValueU8();
                var entrySize = input.ReadValueU16(endian);
                instance.Width = input.ReadValueU16(endian);
                instance.Height = input.ReadValueU16(endian);
                instance.MipMapCount = input.ReadValueU8();
                instance.Format = input.ReadValueU8();
                instance.Unknown16 = input.ReadValueU8();
                instance.Unknown17 = input.ReadValueU8();

                if (entrySize != 24)
                {
                    throw new FormatException();
                }

                var entries = new RawMipMap[entryCount];
                for (int i = 0; i < entryCount; i++)
                {
                    entries[i] = RawMipMap.Read(input, endian);
                }

                instance.MipMaps = entries;
                return instance;
            }
        }

        private struct RawMipMap
        {
            public long DataOffset;
            public uint DataCompressedSize;
            public uint DataUncompressedSize;
            public ushort IndexStart;
            public ushort IndexEnd;
            public uint Reserved; // BAADF00D

            internal static RawMipMap Read(Stream input, Endian endian)
            {
                RawMipMap instance;
                instance.DataOffset = input.ReadValueS64(endian);
                instance.DataCompressedSize = input.ReadValueU32(endian);
                instance.DataUncompressedSize = input.ReadValueU32(endian);
                instance.IndexStart = input.ReadValueU16(endian);
                instance.IndexEnd = input.ReadValueU16(endian);
                instance.Reserved = input.ReadValueU32(endian);
                return instance;
            }
        }
    }
}
