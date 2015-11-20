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
    public class ListForm : Form
    {
        #region Fields
        private string _EditorId;
        private LocalizedString _FullName;
        private readonly List<uint> _Ids;
        #endregion

        public ListForm()
            : base(FormType.FLST, 131)
        {
            this._Ids = new List<uint>();
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

        public List<uint> Ids
        {
            get { return this._Ids; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            FULL = 0x4C4C5546,
            LNAM = 0x4D414E4C,
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

                case FieldType.LNAM:
                {
                    this._Ids.Add(reader.ReadValueU32());
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

            foreach (var id in this._Ids)
            {
                writer.WriteValueU32((uint)FieldType.LNAM, id);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
