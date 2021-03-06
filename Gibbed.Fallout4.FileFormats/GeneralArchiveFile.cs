﻿/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
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
using Gibbed.IO;

namespace Gibbed.Fallout4.FileFormats
{
    public class GeneralArchiveFile : ArchiveFile
    {
        private readonly List<Entry> _Entries;

        public GeneralArchiveFile()
            : base(ArchiveType.General)
        {
            this._Entries = new List<Entry>();
        }

        public List<Entry> Entries
        {
            get { return this._Entries; }
        }

        public override void Deserialize(Stream input)
        {
            var encoding = this.Encoding;

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
                    var name = input.ReadString(nameLength, encoding);
                    entryNames[i] = name;
                }
            }

            // we're assuming the entry names match up in order with the entries
            var entries = new Entry[entryCount];
            for (int i = 0; i < entryCount; i++)
            {
                var rawEntry = rawEntries[i];
                if (rawEntry.Unknown0C != 0x00100100)
                {
                    throw new FormatException();
                }
                entries[i] = new Entry()
                {
                    Name = entryNames[i],
                    NameHash = rawEntry.NameHash,
                    Type = rawEntry.Type,
                    DirectoryNameHash = rawEntry.DirectoryNameHash,
                    Unknown0C = rawEntry.Unknown0C,
                    DataOffset = rawEntry.DataOffset,
                    DataCompressedSize = rawEntry.DataCompressedSize,
                    DataUncompressedSize = rawEntry.DataUncompressedSize,
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
            public uint Unknown0C;
            public long DataOffset;
            public uint DataCompressedSize;
            public uint DataUncompressedSize;
        }

        private struct RawEntry
        {
            public uint NameHash;
            public uint Type;
            public uint DirectoryNameHash;
            public uint Unknown0C;
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
                instance.Unknown0C = input.ReadValueU32(endian);
                instance.DataOffset = input.ReadValueS64(endian);
                instance.DataCompressedSize = input.ReadValueU32(endian);
                instance.DataUncompressedSize = input.ReadValueU32(endian);
                instance.Reserved = input.ReadValueU32(endian);
                return instance;
            }
        }
    }
}
