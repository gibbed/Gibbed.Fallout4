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

namespace Gibbed.Fallout4.PluginFormats.Forms
{
    public class ArmorForm : Form
    {
        #region Fields
        private string _EditorId;
        private LocalizedString _FullName;
        private LocalizedString _Description;
        private string _ModelMale;
        private string _ModelFemale;
        #endregion

        public ArmorForm()
            : base(FormType.ARMO, 131)
        {
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

        public string ModelMale
        {
            get { return this._ModelMale; }
            set { this._ModelMale = value; }
        }

        public string ModelFemale
        {
            get { return this._ModelFemale; }
            set { this._ModelFemale = value; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            FULL = 0x4C4C5546,
            DESC = 0x43534544,
            OBND = 0x444E424F,
            PTRN = 0x4E525450,
            MOD2 = 0x32444F4D,
            MO2T = 0x54324F4D,
            BOD2 = 0x32444F42,
            RNAM = 0x4D414E52,
            KSIZ = 0x5A49534B,
            KWDA = 0x4144574B,
            INRD = 0x44524E49,
            INDX = 0x58444E49,
            MODL = 0x4C444F4D,
            DATA = 0x41544144,
            FNAM = 0x4D414E46,
            DAMA = 0x414D4144,
            APPR = 0x52505041,
            OBTE = 0x4554424F,
            OBTS = 0x5354424F,
            OBTF = 0x4654424F,
            STOP = 0x504F5453,

            EITM = 0x4D544945,
            MOD4 = 0x34444f4D,
            MO4T = 0x54344f4D,
            ETYP = 0x50595445,
            MO2S = 0x53324F4D,
            YNAM = 0x4D414E59,
            ZNAM = 0x4D414E5A,
            MO4S = 0x53344F4D,
            VMAD = 0x44414D56,
            BAMT = 0x544D4142,

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
                    this._FullName = reader.ReadLocalizedString();
                    break;
                }

                case FieldType.DESC:
                {
                    this.MarkField(2);
                    this._Description = reader.ReadLocalizedString();
                    break;
                }

                case FieldType.OBND:
                {
                    this.MarkField(3);
                    Assert(size == 12);
                    break;
                }

                case FieldType.PTRN:
                {
                    this.MarkField(4);
                    Assert(size == 4);
                    break;
                }

                case FieldType.MOD2:
                {
                    this.MarkField(5);
                    this._ModelMale = reader.ReadString(260);
                    break;
                }

                case FieldType.MO2T:
                {
                    this.MarkField(6);
                    break;
                }

                case FieldType.BOD2:
                {
                    this.MarkField(7);
                    Assert(size == 4);
                    break;
                }

                case FieldType.RNAM:
                {
                    this.MarkField(8);
                    Assert(size == 4);
                    break;
                }

                case FieldType.KSIZ:
                {
                    this.MarkField(9);
                    Assert(size == 4);
                    break;
                }

                case FieldType.KWDA:
                {
                    this.MarkField(10);
                    break;
                }

                case FieldType.INRD:
                {
                    this.MarkField(11);
                    Assert(size == 4);
                    break;
                }

                case FieldType.INDX:
                {
                    //this.MarkField(12);
                    Assert(size == 2);
                    break;
                }

                case FieldType.MODL:
                {
                    //this.MarkField(13);
                    Assert(size == 4);
                    break;
                }

                case FieldType.DATA:
                {
                    this.MarkField(14);
                    Assert(size == 12);
                    break;
                }

                case FieldType.FNAM:
                {
                    this.MarkField(15);
                    Assert(size == 8);
                    break;
                }

                case FieldType.DAMA:
                {
                    this.MarkField(16);
                    Assert(size == 8 || size == 16);
                    break;
                }

                case FieldType.APPR:
                {
                    this.MarkField(17);
                    Assert(size == 4 || size == 8 || size == 16 || size == 28);
                    break;
                }

                case FieldType.OBTE:
                {
                    this.MarkField(18);
                    Assert(size == 4);
                    break;
                }

                case FieldType.OBTS:
                {
                    //this.MarkField(19);
                    //0x32 0x1D
                    break;
                }

                case FieldType.OBTF:
                {
                    //this.MarkField(20);
                    Assert(size == 0);
                    break;
                }

                case FieldType.STOP:
                {
                    this.MarkField(21);
                    Assert(size == 0);
                    break;
                }

                case FieldType.EITM:
                {
                    this.MarkField(22);
                    Assert(size == 4);
                    break;
                }

                case FieldType.MOD4:
                {
                    this.MarkField(23);
                    this._ModelFemale = reader.ReadString(260);
                    break;
                }

                case FieldType.MO4T:
                {
                    this.MarkField(24);
                    break;
                }

                case FieldType.ETYP:
                {
                    this.MarkField(25);
                    Assert(size == 4);
                    break;
                }

                case FieldType.MO2S:
                {
                    this.MarkField(26);
                    Assert(size == 4);
                    break;
                }

                case FieldType.YNAM:
                {
                    this.MarkField(27);
                    Assert(size == 4);
                    break;
                }

                case FieldType.ZNAM:
                {
                    this.MarkField(28);
                    Assert(size == 4);
                    break;
                }

                case FieldType.MO4S:
                {
                    this.MarkField(29);
                    Assert(size == 4);
                    break;
                }

                case FieldType.VMAD:
                {
                    this.MarkField(30);
                    break;
                }

                case FieldType.BAMT:
                {
                    this.MarkField(31);
                    Assert(size == 4);
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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
