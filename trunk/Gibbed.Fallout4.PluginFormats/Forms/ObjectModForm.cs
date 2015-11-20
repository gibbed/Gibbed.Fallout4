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
    public class ObjectModForm : Form
    {
        #region Fields
        private string _EditorId;
        private LocalizedString _FullName;
        private LocalizedString _Description;
        private readonly List<uint> _KeywordIds;
        private string _EditorFilter;
        private ObjectModData _Data;
        private uint _LNAM;
        #endregion

        public ObjectModForm()
            : base(FormType.OMOD, 131)
        {
            this._KeywordIds = new List<uint>();
        }

        #region Properties
        public string EditorId
        {
            get { return this._EditorId; }
            set { this._EditorId = value; }
        }

        public LocalizedString FullName
        {
            get { return this._FullName; }
            set { this._FullName = value; }
        }

        public LocalizedString Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        public string EditorFilter
        {
            get { return this._EditorFilter; }
            set { this._EditorFilter = value; }
        }

        public List<uint> KeywordIds
        {
            get { return this._KeywordIds; }
        }

        public ObjectModData Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }

        public uint LNAM
        {
            get { return this._LNAM; }
            set { this._LNAM = value; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            FULL = 0x4C4C5546,
            DESC = 0x43534544,
            MODL = 0x4C444F4D,
            MODT = 0x54444F4D,
            DATA = 0x41544144,
            MNAM = 0x4D414E4D,
            LNAM = 0x4D414E4C,
            NAM1 = 0x314D414E,
            FLTR = 0x52544C46,
            FNAM = 0x4D414E46,
            MODS = 0x53444F4D,
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

                case FieldType.FULL:
                {
                    this.MarkField(1);
                    this._FullName = reader.ReadLocalizedString();
                    break;
                }

                case FieldType.DESC:
                {
                    this.MarkField(2);
                    this._Description = reader.ReadLocalizedString();
                    break;
                }

                case FieldType.MODL:
                {
                    this.MarkField(3);
                    break;
                }

                case FieldType.MODT:
                {
                    this.MarkField(4);
                    break;
                }

                case FieldType.DATA:
                {
                    this.MarkField(5);
                    this._Data = reader.ReadObject<ObjectModData>();
                    break;
                }

                case FieldType.MNAM:
                {
                    this.MarkField(6);
                    Debug.Assert(size > 0 && (size % 4) == 0);
                    var keywordIds = new uint[size / 4];
                    for (int i = 0; i < keywordIds.Length; i++)
                    {
                        keywordIds[i] = reader.ReadValueU32();
                    }
                    this._KeywordIds.Clear();
                    this._KeywordIds.AddRange(keywordIds);
                    break;
                }

                case FieldType.LNAM:
                {
                    this.MarkField(7);
                    Debug.Assert(size == 4);
                    this._LNAM = reader.ReadValueU32();
                    break;
                }

                case FieldType.NAM1:
                {
                    this.MarkField(8);
                    break;
                }

                case FieldType.FLTR:
                {
                    this.MarkField(9);
                    this._EditorFilter = reader.ReadString(260);
                    break;
                }

                case FieldType.FNAM:
                {
                    this.MarkField(10);
                    break;
                }

                case FieldType.MODS:
                {
                    this.MarkField(11);
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

            if (this._FullName.IsEmpty == false)
            {
                writer.WriteLocalizedString((uint)FieldType.FULL, this._FullName);
            }

            if (this._Description.IsEmpty == false)
            {
                writer.WriteLocalizedString((uint)FieldType.DESC, this._Description);
            }

            if (this._KeywordIds.Count == 1)
            {
                writer.WriteValueU32((uint)FieldType.MNAM, this._KeywordIds[0]);
            }

            if (this._Data != null)
            {
                writer.WriteObject((uint)FieldType.DATA, this._Data);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
