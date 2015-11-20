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
    public class MaterialSwapForm : Form
    {
        #region Fields
        private string _EditorId;
        private readonly List<string> _BaseNames;
        private readonly List<string> _SwapNames;
        private readonly List<string> _FNAM;
        private readonly List<uint> _CNAM;
        #endregion

        public MaterialSwapForm()
            : base(FormType.MSWP, 131)
        {
            this._BaseNames = new List<string>();
            this._SwapNames = new List<string>();
            this._FNAM = new List<string>();
            this._CNAM = new List<uint>();
        }

        #region Properties
        public string EditorId
        {
            get { return this._EditorId; }
            set { this._EditorId = value; }
        }

        public List<string> BaseNames
        {
            get { return this._BaseNames; }
        }

        public List<string> SwapNames
        {
            get { return this._SwapNames; }
        }

        public List<string> FNAM
        {
            get { return this._FNAM; }
        }

        public List<uint> CNAM
        {
            get { return this._CNAM; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            BNAM = 0x4D414E42,
            SNAM = 0x4D414E53,
            FNAM = 0x4D414E46,
            CNAM = 0x4D414E43,
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

                case FieldType.BNAM:
                {
                    this._BaseNames.Add(reader.ReadString(260));
                    break;
                }

                case FieldType.SNAM:
                {
                    this._SwapNames.Add(reader.ReadString(260));
                    break;
                }

                case FieldType.FNAM:
                {
                    this._FNAM.Add(reader.ReadString(260));
                    break;
                }

                case FieldType.CNAM:
                {
                    this._CNAM.Add(reader.ReadValueU32());
                    break;
                }
            }
        }

        internal override void WriteFields(IFormWriter writer)
        {
            if (string.IsNullOrEmpty(this._EditorId) == false)
            {
                writer.WriteString((uint)FieldType.EDID, this._EditorId);
            }

            for (int i = 0; i < Math.Max(this._BaseNames.Count, this._SwapNames.Count); i++)
            {
                if (i < this._BaseNames.Count)
                {
                    writer.WriteString((uint)FieldType.BNAM, this._BaseNames[i], 260);
                }

                if (i < this._SwapNames.Count)
                {
                    writer.WriteString((uint)FieldType.SNAM, this._SwapNames[i], 260);
                }
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._EditorId) == false ? this._EditorId : base.ToString();
        }
    }
}
