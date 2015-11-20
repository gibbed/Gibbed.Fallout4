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
using System.Diagnostics;

namespace Gibbed.Fallout4.PluginFormats.Forms
{
    public class ConstructibleObjectForm : Form
    {
        #region Fields
        private string _EditorId;
        private LocalizedString _Description;
        private readonly List<ConditionData> _Conditions;
        private readonly List<Ingredient> _Ingredients;
        private uint _OutputId;
        private uint _OutputCount;
        private uint _BenchKeywordId;
        private readonly List<uint> _FilterKeywordIds;
        private uint _PickupSoundId;
        private uint _PutdownSoundId;
        private uint _ArtId;
        #endregion

        public ConstructibleObjectForm()
            : base(FormType.COBJ, 131)
        {
            this._Conditions = new List<ConditionData>();
            this._Ingredients = new List<Ingredient>();
            this._FilterKeywordIds = new List<uint>();
        }

        public struct Ingredient
        {
            public readonly uint InputId;
            public readonly int Quantity;

            public Ingredient(uint inputId, int quantity)
            {
                this.InputId = inputId;
                this.Quantity = quantity;
            }
        }

        #region Properties
        public string EditorId
        {
            get { return this._EditorId; }
            set { this._EditorId = value; }
        }

        public LocalizedString Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        public List<ConditionData> Conditions
        {
            get { return this._Conditions; }
        }

        public List<Ingredient> Ingredients
        {
            get { return this._Ingredients; }
        }

        public uint OutputId
        {
            get { return this._OutputId; }
            set { this._OutputId = value; }
        }

        public uint OutputCount
        {
            get { return this._OutputCount; }
            set { this._OutputCount = value; }
        }

        public uint BenchKeywordId
        {
            get { return this._BenchKeywordId; }
            set { this._BenchKeywordId = value; }
        }

        public List<uint> FilterKeywordIds
        {
            get { return this._FilterKeywordIds; }
        }

        public uint PickupSoundId
        {
            get { return this._PickupSoundId; }
            set { this._PickupSoundId = value; }
        }

        public uint PutdownSoundId
        {
            get { return this._PutdownSoundId; }
            set { this._PutdownSoundId = value; }
        }

        public uint ArtId
        {
            get { return this._ArtId; }
            set { this._ArtId = value; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            DESC = 0x43534544,
            FVPA = 0x41505646,
            CTDA = 0x41445443,
            CNAM = 0x4D414E43,
            BNAM = 0x4D414E42,
            FNAM = 0x4D414E46,
            INTV = 0x56544E49,
            YNAM = 0x4D414E59,
            ZNAM = 0x4D414E5A,
            ANAM = 0x4D414E41,
            NAM1 = 0x314D414E,
            NAM2 = 0x324D414E,
            NAM3 = 0x334D414E,
            // ReSharper restore InconsistentNaming
        }

        internal override void ReadField(uint type, IFieldReader reader)
        {
            var size = reader.Size;
            switch ((FieldType)type)
            {
                case FieldType.EDID:
                {
                    this.MarkField(0);
                    this._EditorId = reader.ReadString(size);
                    break;
                }

                case FieldType.DESC:
                {
                    this.MarkField(1);
                    this._Description = reader.ReadLocalizedString();
                    break;
                }

                case FieldType.FVPA:
                {
                    this.MarkField(2);
                    Debug.Assert(size > 0 && (size % 8) == 0);
                    var ingredients = new Ingredient[size / 8];
                    for (int i = 0; i < ingredients.Length; i++)
                    {
                        var inputId = reader.ReadValueU32();
                        var quantity = reader.ReadValueS32();
                        ingredients[i] = new Ingredient(inputId, quantity);
                    }
                    this._Ingredients.Clear();
                    this._Ingredients.AddRange(ingredients);
                    break;
                }

                case FieldType.CTDA:
                {
                    Debug.Assert(size == 32);
                    this._Conditions.Add(ConditionData.Read(reader));
                    break;
                }

                case FieldType.CNAM:
                {
                    this.MarkField(4);
                    Debug.Assert(size == 4);
                    this._OutputId = reader.ReadValueU32();
                    break;
                }

                case FieldType.INTV:
                {
                    this.MarkField(5);
                    if (size == 4)
                    {
                        this._OutputCount = reader.ReadValueU32();
                    }
                    else if (size == 2)
                    {
                        this._OutputCount = reader.ReadValueU16();
                    }
                    else
                    {
                        throw new FormatException();
                    }
                    break;
                }

                case FieldType.BNAM:
                {
                    this.MarkField(6);
                    Debug.Assert(size == 4);
                    this._BenchKeywordId = reader.ReadValueU32();
                    break;
                }

                case FieldType.FNAM:
                {
                    this.MarkField(7);
                    Debug.Assert(size > 0 && (size % 4) == 0);
                    var ids = new uint[size / 4];
                    for (int i = 0; i < ids.Length; i++)
                    {
                        ids[i] = reader.ReadValueU32();
                    }
                    this._FilterKeywordIds.Clear();
                    this._FilterKeywordIds.AddRange(ids);
                    break;
                }

                case FieldType.YNAM:
                {
                    this.MarkField(8);
                    Debug.Assert(size == 4);
                    this._PickupSoundId = reader.ReadValueU32();
                    break;
                }

                case FieldType.ZNAM:
                {
                    this.MarkField(9);
                    Debug.Assert(size == 4);
                    this._PutdownSoundId = reader.ReadValueU32();
                    break;
                }

                case FieldType.ANAM:
                {
                    this.MarkField(11);
                    Debug.Assert(size == 4);
                    this._ArtId = reader.ReadValueU32();
                    break;
                }

                case FieldType.NAM1:
                case FieldType.NAM2:
                case FieldType.NAM3:
                {
                    // deprecated
                    break;
                }

                default:
                {
                    throw new NotSupportedException();
                }
            }
        }

        internal override void WriteFields(IFormWriter writer)
        {
            if (string.IsNullOrEmpty(this._EditorId) == false)
            {
                writer.WriteString((uint)FieldType.EDID, this._EditorId);
            }

            if (this._Description.IsEmpty == false)
            {
                writer.WriteLocalizedString((uint)FieldType.DESC, this._Description);
            }

            if (this._Ingredients.Count > 0)
            {
                writer.WriteData(
                    (uint)FieldType.FVPA,
                    w =>
                    {
                        foreach (var ingredient in this._Ingredients)
                        {
                            w.WriteValueU32(ingredient.InputId);
                            w.WriteValueS32(ingredient.Quantity);
                        }
                    });
            }

            if (this._OutputId != 0)
            {
                writer.WriteValueU32((uint)FieldType.CNAM, this._OutputId);
            }

            if (this._OutputCount != 0)
            {
                writer.WriteValueU32((uint)FieldType.INTV, this._OutputCount);
            }

            if (this._BenchKeywordId != 0)
            {
                writer.WriteValueU32((uint)FieldType.BNAM, this._BenchKeywordId);
            }

            if (this._FilterKeywordIds.Count > 0)
            {
                writer.WriteData(
                    (uint)FieldType.FNAM,
                    w =>
                    {
                        foreach (var id in this._FilterKeywordIds)
                        {
                            w.WriteValueU32(id);
                        }
                    });
            }

            if (this._PickupSoundId != 0)
            {
                writer.WriteValueU32((uint)FieldType.YNAM, this._PickupSoundId);
            }

            if (this._PutdownSoundId != 0)
            {
                writer.WriteValueU32((uint)FieldType.ZNAM, this._PutdownSoundId);
            }

            if (this._ArtId != 0)
            {
                writer.WriteValueU32((uint)FieldType.ANAM, this._ArtId);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
