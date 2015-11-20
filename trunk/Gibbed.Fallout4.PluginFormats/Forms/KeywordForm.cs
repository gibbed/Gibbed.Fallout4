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
    public class KeywordForm : Form
    {
        #region Fields
        private string _EditorId;
        private LocalizedString _FullName;
        private uint _CNAM;
        private uint _TNAM;
        private uint _DATA;
        private string _NNAM;
        private string _DNAM;
        #endregion

        public KeywordForm()
            : base(FormType.KYWD, 131)
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

        public uint TNAM
        {
            get { return this._TNAM; }
            set { this._TNAM = value; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            FULL = 0x4C4C5546,
            CNAM = 0x4D414E43,
            DNAM = 0x4D414E44,
            TNAM = 0x4D414E54,
            DATA = 0x41544144,
            NNAM = 0x4D414E4E,
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

                case FieldType.CNAM:
                {
                    this.MarkField(2);
                    Assert(size == 4);
                    this._CNAM = reader.ReadValueU32();
                    break;
                }

                case FieldType.TNAM:
                {
                    this.MarkField(3);
                    Assert(size == 4);
                    this._TNAM = reader.ReadValueU32();
                    break;
                }

                case FieldType.DNAM:
                {
                    this.MarkField(4);
                    this._DNAM = reader.ReadString(0);
                    break;
                }

                case FieldType.DATA:
                {
                    this.MarkField(5);
                    Assert(size == 4);
                    this._DATA = reader.ReadValueU32();
                    break;
                }

                case FieldType.NNAM:
                {
                    this.MarkField(6);
                    this._NNAM = reader.ReadString(260);
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

            if (this._TNAM != 0)
            {
                writer.WriteValueU32((uint)FieldType.TNAM, this._TNAM);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
