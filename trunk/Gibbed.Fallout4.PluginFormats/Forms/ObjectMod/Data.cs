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

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public class Data : Field
    {
        #region Fields
        private bool _Unknown1B;
        private bool _Unknown1C;
        private FormType _ObjectType;
        private byte _Unknown19;
        private byte _Unknown1A;
        private uint _KeywordId;
        private readonly List<uint> _KeywordIds;
        private readonly List<Tuple<uint, uint>> _Unknown98;
        private readonly List<Tuple<uint, byte, bool, bool>> _Includes;
        private readonly List<Property> _Properties;
        #endregion

        public Data()
        {
            this._KeywordIds = new List<uint>();
            this._Unknown98 = new List<Tuple<uint, uint>>();
            this._Includes = new List<Tuple<uint, byte, bool, bool>>();
            this._Properties = new List<Property>();
        }

        #region Properties
        public bool Unknown1B
        {
            get { return this._Unknown1B; }
            set { this._Unknown1B = value; }
        }

        public bool Unknown1C
        {
            get { return this._Unknown1C; }
            set { this._Unknown1C = value; }
        }

        public FormType ObjectType
        {
            get { return this._ObjectType; }
            set { this._ObjectType = value; }
        }

        public byte Unknown19
        {
            get { return this._Unknown19; }
            set { this._Unknown19 = value; }
        }

        public byte Unknown1A
        {
            get { return this._Unknown1A; }
            set { this._Unknown1A = value; }
        }

        public uint KeywordId
        {
            get { return this._KeywordId; }
            set { this._KeywordId = value; }
        }

        public List<uint> KeywordIds
        {
            get { return this._KeywordIds; }
        }

        public List<Tuple<uint, uint>> Unknown98
        {
            get { return this._Unknown98; }
        }

        public List<Tuple<uint, byte, bool, bool>> Includes
        {
            get { return this._Includes; }
        }

        public List<Property> Properties
        {
            get { return this._Properties; }
        }
        #endregion

        internal override void Read(IFieldReader reader)
        {
            if (reader.Version < 53)
            {
                reader.SkipBytes(4);
            }

            var includeCount = reader.Version >= 48 ? reader.ReadValueU32() : 0;
            var propertyCount = reader.Version >= 48 ? reader.ReadValueU32() : 0;

            if (reader.Version < 52)
            {
                reader.SkipBytes(4);
            }

            this._Unknown1B = reader.Version >= 48 && reader.ReadValueB8();
            this._Unknown1C = reader.Version >= 48 && reader.ReadValueB8();
            this._ObjectType = reader.Version >= 63
                                  ? (FormType)reader.ReadValueU32()
                                  : (reader.Version >= 53
                                         ? FormTypes.GetTypeFromIndex(reader.ReadValueU8())
                                         : FormType.WEAP);

            this._Unknown19 = reader.Version >= 90 ? reader.ReadValueU8() : (byte)0;
            this._Unknown1A = reader.Version >= 107 ? reader.ReadValueU8() : (byte)0;
            this._KeywordId = reader.ReadValueU32();

            var keywordCount = reader.ReadValueU32();
            var keywordIds = new uint[keywordCount];
            for (uint i = 0; i < keywordCount; i++)
            {
                keywordIds[i] = reader.ReadValueU32();
            }
            this._KeywordIds.Clear();
            this._KeywordIds.AddRange(keywordIds);

            this._Unknown98.Clear();
            if (reader.Version >= 57)
            {
                var count3 = reader.ReadValueU32();
                var items = new Tuple<uint, uint>[count3];
                for (int i = 0; i < count3; i++)
                {
                    var item1 = reader.ReadValueU32();
                    var item2 = reader.ReadValueU32();
                    items[i] = new Tuple<uint, uint>(item1, item2);
                }
                this._Unknown98.AddRange(items);
            }

            var includes = new Tuple<uint, byte, bool, bool>[includeCount];
            for (int i = 0; i < includeCount; i++)
            {
                var unknown17 = reader.ReadValueU32();
                var unknown18 = reader.Version >= 49 ? reader.ReadValueU8() : (byte)0;
                var unknown19 = reader.Version < 49 || reader.ReadValueB8();
                var unknown20 = reader.Version < 49 || reader.ReadValueB8();
                includes[i] = new Tuple<uint, byte, bool, bool>(unknown17, unknown18, unknown19, unknown20);
            }
            this._Includes.Clear();
            this._Includes.AddRange(includes);

            var properties = new Property[propertyCount];
            for (int i = 0; i < propertyCount; i++)
            {
                properties[i] = Property.Read(reader);
            }
            this._Properties.Clear();
            this._Properties.AddRange(properties);
        }

        internal override void Write(IFieldWriter writer)
        {
            writer.WriteValueS32(this._Includes.Count);
            writer.WriteValueS32(this._Properties.Count);
            writer.WriteValueB8(this._Unknown1B);
            writer.WriteValueB8(this._Unknown1C);
            writer.WriteValueU32((uint)this._ObjectType);
            writer.WriteValueU8(this._Unknown19);
            writer.WriteValueU8(this._Unknown1A);
            writer.WriteValueU32(this._KeywordId);

            writer.WriteValueS32(this._KeywordIds.Count);
            foreach (var keywordId in this._KeywordIds)
            {
                writer.WriteValueU32(keywordId);
            }

            writer.WriteValueS32(this._Unknown98.Count);
            foreach (var item in this._Unknown98)
            {
                writer.WriteValueU32(item.Item1);
                writer.WriteValueU32(item.Item2);
            }

            foreach (var include in this._Includes)
            {
                writer.WriteValueU32(include.Item1);
                writer.WriteValueU8(include.Item2);
                writer.WriteValueB8(include.Item3);
                writer.WriteValueB8(include.Item4);
            }

            foreach (var property in this._Properties)
            {
                property.Write(writer);
            }
        }
    }
}
