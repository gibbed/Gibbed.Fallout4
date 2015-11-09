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
    public class ArchiveFile
    {
        public const uint Signature = 0x58445442; // 'BTDX'

        private Endian _Endian;
        private readonly List<Entry> _Entries;

        public ArchiveFile()
        {
            this._Entries = new List<Entry>();
        }

        public Endian Endian
        {
            get { return this._Endian; }
            set { this._Endian = value; }
        }

        public List<Entry> Entries
        {
            get { return this._Entries; }
        }

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            var basePosition = input.Position;

            var magic = input.ReadValueU32(Endian.Little);
            if (magic != Signature && magic.Swap() != Signature)
            {
                throw new FormatException();
            }
            var endian = magic == Signature ? Endian.Little : Endian.Big;

            // TODO(rick): determine if version/generalMagic are actually a count and is a sequential block ID

            var version = input.ReadValueU32(endian);
            if (version != 1)
            {
                throw new FormatException();
            }

            var generalMagic = input.ReadValueU32(endian);
            if (generalMagic != 0x4C524E47) // 'GNRL'
            {
                throw new FormatException();
            }

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
                // TODO(rick): validate flags
                entries[i] = new Entry()
                {
                    Name = entryNames[i],
                    NameHash = rawEntry.NameHash,
                    Type = rawEntry.Type,
                    DirectoryNameHash = rawEntry.DirectoryNameHash,
                    Flags = rawEntry.Flags,
                    DataOffset = rawEntry.DataOffset,
                    DataCompressedSize = rawEntry.DataCompressedSize,
                    DataUncompressedSize = rawEntry.DataUncompressedSize,
                };
            }

            this._Endian = endian;
            this._Entries.Clear();
            this._Entries.AddRange(entries);
        }

        [Flags]
        public enum EntryFlags : uint
        {
            None = 0u,
            Unknown0 = 1u << 0,
            Unknown1 = 1u << 1,
            Unknown2 = 1u << 2,
            Unknown3 = 1u << 3,
            Unknown4 = 1u << 4,
            Unknown5 = 1u << 5,
            Unknown6 = 1u << 6,
            Unknown7 = 1u << 7,
            Unknown8 = 1u << 8,
            Unknown9 = 1u << 9,
            Unknown10 = 1u << 10,
            Unknown11 = 1u << 11,
            Unknown12 = 1u << 12,
            Unknown13 = 1u << 13,
            Unknown14 = 1u << 14,
            Unknown15 = 1u << 15,
            Unknown16 = 1u << 16,
            Unknown17 = 1u << 17,
            Unknown18 = 1u << 18,
            Unknown19 = 1u << 19,
            Unknown20 = 1u << 20,
            Unknown21 = 1u << 21,
            Unknown22 = 1u << 22,
            Unknown23 = 1u << 23,
            Unknown24 = 1u << 24,
            Unknown25 = 1u << 25,
            Unknown26 = 1u << 26,
            Unknown27 = 1u << 27,
            Unknown28 = 1u << 28,
            Unknown29 = 1u << 29,
            Unknown30 = 1u << 30,
            Unknown31 = 1u << 31,
        }

        public struct Entry
        {
            public string Name;
            public uint NameHash;
            public uint Type;
            public uint DirectoryNameHash;
            public EntryFlags Flags;
            public long DataOffset;
            public uint DataCompressedSize;
            public uint DataUncompressedSize;
        }

        private struct RawEntry
        {
            public uint NameHash;
            public uint Type;
            public uint DirectoryNameHash;
            public EntryFlags Flags;
            public long DataOffset;
            public uint DataCompressedSize;
            public uint DataUncompressedSize;
            public uint Reserved; // BAADF00D

            internal static RawEntry Read(Stream input, Endian endian)
            {
                RawEntry instance;
                instance.NameHash = input.ReadValueU32(endian);
                instance.Type = input.ReadValueU32(endian);
                instance.DirectoryNameHash = input.ReadValueU32(endian);
                instance.Flags = (EntryFlags)input.ReadValueU32(endian);
                instance.DataOffset = input.ReadValueS64(endian);
                instance.DataCompressedSize = input.ReadValueU32(endian);
                instance.DataUncompressedSize = input.ReadValueU32(endian);
                instance.Reserved = input.ReadValueU32(endian);
                return instance;
            }
        }
    }
}
