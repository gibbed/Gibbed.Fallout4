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

namespace Gibbed.Fallout4.PluginFormats.Forms
{
    public class ObjectModData : Field
    {
        private bool _Unknown1B;
        private bool _Unknown1C;
        private FormType _Unknown18;
        private byte _Unknown19;
        private byte _Unknown1A;
        private uint _KeywordId;
        private readonly List<uint> _KeywordIds;
        private readonly List<Tuple<uint, uint>> _Unknown98;
        private readonly List<Tuple<uint, byte, bool, bool>> _Includes;
        private readonly List<Modifier> _Modifiers;

        public ObjectModData()
        {
            this._KeywordIds = new List<uint>();
            this._Unknown98 = new List<Tuple<uint, uint>>();
            this._Includes = new List<Tuple<uint, byte, bool, bool>>();
            this._Modifiers = new List<Modifier>();
        }

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

        public FormType Unknown18
        {
            get { return this._Unknown18; }
            set { this._Unknown18 = value; }
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

        public List<Modifier> Modifiers
        {
            get { return this._Modifiers; }
        }

        internal override void Read(IFieldReader reader)
        {
            if (reader.Version < 53)
            {
                reader.SkipBytes(4);
            }

            var includeCount = reader.Version >= 48 ? reader.ReadValueU32() : 0;
            var modifierCount = reader.Version >= 48 ? reader.ReadValueU32() : 0;

            if (reader.Version < 52)
            {
                reader.SkipBytes(4);
            }

            this._Unknown1B = reader.Version >= 48 && reader.ReadValueB8();
            this._Unknown1C = reader.Version >= 48 && reader.ReadValueB8();
            this._Unknown18 = reader.Version >= 63
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

            var modifiers = new Modifier[modifierCount];
            for (int i = 0; i < modifierCount; i++)
            {
                var modifierType = reader.ReadValueU8(); // & 7
                reader.SkipBytes(3);

                Modifier modifier;
                switch ((ModifierType)modifierType)
                {
                    case ModifierType.Int:
                    {
                        modifier = new IntModifier();
                        break;
                    }

                    case ModifierType.Float:
                    {
                        modifier = new FloatModifier();
                        break;
                    }

                    case ModifierType.Bool:
                    {
                        modifier = new BoolModifier();
                        break;
                    }

                    case ModifierType.Id:
                    {
                        modifier = new IdModifier();
                        break;
                    }

                    case ModifierType.Unknown5:
                    {
                        modifier = new Unknown5Modifier();
                        break;
                    }

                    case ModifierType.IdAndFloat:
                    {
                        modifier = new IdAndFloatModifier();
                        break;
                    }

                    default:
                    {
                        throw new NotImplementedException();
                    }
                }

                modifier.Read(reader);
                modifiers[i] = modifier;
            }
            this._Modifiers.Clear();
            this._Modifiers.AddRange(modifiers);
        }

        internal override void Write(IFieldWriter writer)
        {
            writer.WriteValueS32(this._Includes.Count);
            writer.WriteValueS32(this._Modifiers.Count);
            writer.WriteValueB8(this._Unknown1B);
            writer.WriteValueB8(this._Unknown1C);
            writer.WriteValueU32((uint)this._Unknown18);
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

            foreach (var modifier in this._Modifiers)
            {
                modifier.Write(writer);
            }
        }

        public enum ModifierType : byte
        {
            Int = 0,
            Float = 1,
            Bool = 2,
            Id = 4,
            Unknown5 = 5,
            IdAndFloat = 6,
        }

        public abstract class Modifier
        {
            private readonly ModifierType _Type;
            private byte _Unknown1;
            private ushort _Unknown2;
            private uint _Unknown3;

            internal Modifier(ModifierType type)
            {
                this._Type = type;
            }

            internal Modifier(ModifierType type, byte unknown1, ushort unknown2, uint unknown3)
            {
                this._Type = type;
                this._Unknown1 = unknown1;
                this._Unknown2 = unknown2;
                this._Unknown3 = unknown3;
            }

            public ModifierType Type
            {
                get { return this._Type; }
            }

            public byte Unknown1
            {
                get { return this._Unknown1; }
                set { this._Unknown1 = value; }
            }

            public ushort Unknown2
            {
                get { return this._Unknown2; }
                set { this._Unknown2 = value; }
            }

            public uint Unknown3
            {
                get { return this._Unknown3; }
                set { this._Unknown3 = value; }
            }

            internal void Read(IFieldReader reader)
            {
                this._Unknown1 = reader.ReadValueU8(); // & 3
                reader.SkipBytes(3);
                this._Unknown2 = reader.ReadValueU16(); // & 0x7FF
                reader.SkipBytes(2);
                this.ReadValues(reader);
                this._Unknown3 = reader.Version >= 88 ? reader.ReadValueU32() : 0;
            }

            internal abstract void ReadValues(IFieldReader reader);

            internal void Write(IFieldWriter writer)
            {
                writer.WriteValueU32((byte)this._Type);
                writer.WriteValueU32(this._Unknown1);
                writer.WriteValueU32(this._Unknown2);
                this.WriteValues(writer);
                writer.WriteValueU32(this._Unknown3);
            }

            internal abstract void WriteValues(IFieldWriter writer);
        }

        public sealed class IntModifier : Modifier
        {
            private int _Value1;
            private int _Value2;

            public IntModifier()
                : base(ModifierType.Int)
            {
            }

            public IntModifier(byte unknown1, ushort unknown2, int value1, int value2, uint unknown3)
                : base(ModifierType.Int, unknown1, unknown2, unknown3)
            {
                this._Value1 = value1;
                this._Value2 = value2;
            }

            public int Value1
            {
                get { return this._Value1; }
                set { this._Value1 = value; }
            }

            public int Value2
            {
                get { return this._Value2; }
                set { this._Value2 = value; }
            }

            internal override void ReadValues(IFieldReader reader)
            {
                this._Value1 = reader.ReadValueS32();
                this._Value2 = reader.ReadValueS32();
            }

            internal override void WriteValues(IFieldWriter writer)
            {
                writer.WriteValueS32(this._Value1);
                writer.WriteValueS32(this._Value2);
            }

            public override string ToString()
            {
                return string.Format("{0}={1},{2},({3},{4}),{5}",
                                     this.Type,
                                     this.Unknown1,
                                     this.Unknown2,
                                     this.Value1,
                                     this.Value2,
                                     this.Unknown3);
            }
        }

        public sealed class FloatModifier : Modifier
        {
            private float _Value1;
            private float _Value2;

            public FloatModifier()
                : base(ModifierType.Float)
            {
            }

            public float Value1
            {
                get { return this._Value1; }
                set { this._Value1 = value; }
            }

            public float Value2
            {
                get { return this._Value2; }
                set { this._Value2 = value; }
            }

            public FloatModifier(byte unknown1, ushort unknown2, float value1, float value2, uint unknown3)
                : base(ModifierType.Float, unknown1, unknown2, unknown3)
            {
                this._Value1 = value1;
                this._Value2 = value2;
            }

            internal override void ReadValues(IFieldReader reader)
            {
                this._Value1 = reader.ReadValueF32();
                this._Value2 = reader.ReadValueF32();
            }

            internal override void WriteValues(IFieldWriter writer)
            {
                writer.WriteValueF32(this._Value1);
                writer.WriteValueF32(this._Value2);
            }

            public override string ToString()
            {
                return string.Format("{0}={1},{2},({3},{4}),{5}",
                                     this.Type,
                                     this.Unknown1,
                                     this.Unknown2,
                                     this.Value1,
                                     this.Value2,
                                     this.Unknown3);
            }
        }

        public sealed class BoolModifier : Modifier
        {
            private bool _Value1;
            private bool _Value2;

            public BoolModifier()
                : base(ModifierType.Bool)
            {
            }

            public BoolModifier(byte unknown1, ushort unknown2, bool value1, bool value2, uint unknown3)
                : base(ModifierType.Bool, unknown1, unknown2, unknown3)
            {
                this._Value1 = value1;
                this._Value2 = value2;
            }

            public bool Value1
            {
                get { return this._Value1; }
                set { this._Value1 = value; }
            }

            public bool Value2
            {
                get { return this._Value2; }
                set { this._Value2 = value; }
            }

            internal override void ReadValues(IFieldReader reader)
            {
                this._Value1 = reader.ReadValueB8();
                reader.SkipBytes(3);
                this._Value2 = reader.ReadValueB8();
                reader.SkipBytes(3);
            }

            internal override void WriteValues(IFieldWriter writer)
            {
                writer.WriteValueU32(this._Value1 == true ? 1u : 0u);
                writer.WriteValueU32(this._Value2 == true ? 1u : 0u);
            }

            public override string ToString()
            {
                return string.Format("{0}={1},{2},({3},{4}),{5}",
                                     this.Type,
                                     this.Unknown1,
                                     this.Unknown2,
                                     this.Value1,
                                     this.Value2,
                                     this.Unknown3);
            }
        }

        public sealed class IdModifier : Modifier
        {
            private uint _Value1;
            private uint _Value2;

            public IdModifier()
                : base(ModifierType.Id)
            {
            }

            public IdModifier(byte unknown1, ushort unknown2, uint value1, uint value2, uint unknown3)
                : base(ModifierType.Id, unknown1, unknown2, unknown3)
            {
                this._Value1 = value1;
                this._Value2 = value2;
            }

            public uint Value1
            {
                get { return this._Value1; }
                set { this._Value1 = value; }
            }

            public uint Value2
            {
                get { return this._Value2; }
                set { this._Value2 = value; }
            }

            internal override void ReadValues(IFieldReader reader)
            {
                this._Value1 = reader.ReadValueU32();
                this._Value2 = reader.ReadValueU32();
            }

            internal override void WriteValues(IFieldWriter writer)
            {
                writer.WriteValueU32(this._Value1);
                writer.WriteValueU32(this._Value2);
            }

            public override string ToString()
            {
                return string.Format("{0}={1},{2},({3},{4}),{5}",
                                     this.Type,
                                     this.Unknown1,
                                     this.Unknown2,
                                     this.Value1,
                                     this.Value2,
                                     this.Unknown3);
            }
        }

        public sealed class Unknown5Modifier : Modifier
        {
            private uint _Value1;
            private uint _Value2;

            public Unknown5Modifier()
                : base(ModifierType.Unknown5)
            {
            }

            public Unknown5Modifier(byte unknown1, ushort unknown2, uint value1, uint value2, uint unknown3)
                : base(ModifierType.Unknown5, unknown1, unknown2, unknown3)
            {
                this._Value1 = value1;
                this._Value2 = value2;
            }

            public uint Value1
            {
                get { return this._Value1; }
                set { this._Value1 = value; }
            }

            public uint Value2
            {
                get { return this._Value2; }
                set { this._Value2 = value; }
            }

            internal override void ReadValues(IFieldReader reader)
            {
                this._Value1 = reader.ReadValueU32();
                this._Value2 = reader.ReadValueU32();
            }

            internal override void WriteValues(IFieldWriter writer)
            {
                writer.WriteValueU32(this._Value1);
                writer.WriteValueU32(this._Value2);
            }

            public override string ToString()
            {
                return string.Format("{0}={1},{2},({3},{4}),{5}",
                                     this.Type,
                                     this.Unknown1,
                                     this.Unknown2,
                                     this.Value1,
                                     this.Value2,
                                     this.Unknown3);
            }
        }

        public sealed class IdAndFloatModifier : Modifier
        {
            private uint _Value1;
            private float _Value2;

            public IdAndFloatModifier()
                : base(ModifierType.IdAndFloat)
            {
            }

            public IdAndFloatModifier(byte unknown1, ushort unknown2, uint value1, float value2, uint unknown3)
                : base(ModifierType.IdAndFloat, unknown1, unknown2, unknown3)
            {
                this._Value1 = value1;
                this._Value2 = value2;
            }

            public uint Value1
            {
                get { return this._Value1; }
                set { this._Value1 = value; }
            }

            public float Value2
            {
                get { return this._Value2; }
                set { this._Value2 = value; }
            }

            internal override void ReadValues(IFieldReader reader)
            {
                this._Value1 = reader.ReadValueU32();
                this._Value2 = reader.ReadValueF32();
            }

            internal override void WriteValues(IFieldWriter writer)
            {
                writer.WriteValueU32(this._Value1);
                writer.WriteValueF32(this._Value2);
            }

            public override string ToString()
            {
                return string.Format("{0}={1},{2},({3},{4}),{5}",
                                     this.Type,
                                     this.Unknown1,
                                     this.Unknown2,
                                     this.Value1,
                                     this.Value2,
                                     this.Unknown3);
            }
        }
    }
}
