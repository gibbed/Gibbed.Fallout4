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
    public class PluginForm : Form
    {
        #region Fields
        private PluginHeader _Header;
        private string _Creator;
        private string _Summary;
        private readonly List<string> _Masters;
        #endregion

        public PluginForm()
            : base(FormType.TES4, 131)
        {
            this._Masters = new List<string>();
        }

        #region Properties
        public PluginHeader Header
        {
            get { return this._Header; }
            set { this._Header = value; }
        }

        public string Creator
        {
            get { return this._Creator; }
            set { this._Creator = value; }
        }

        public string Summary
        {
            get { return this._Summary; }
            set { this._Summary = value; }
        }

        public List<string> Masters
        {
            get { return this._Masters; }
        }
        #endregion

        private enum Fields : uint
        {
            // ReSharper disable InconsistentNaming
            HEDR = 0x52444548,
            CNAM = 0x4D414E43,
            SNAM = 0x4D414E53,
            MAST = 0x5453414D,
            TNAM = 0x4D414E54,
            INTV = 0x56544E49,
            INCC = 0x43434E49,
            DATA = 0x41544144,
            // ReSharper restore InconsistentNaming
        }

        internal override void ReadField(uint type, IFieldReader reader)
        {
            var size = reader.Size;

            switch ((Fields)type)
            {
                case Fields.HEDR:
                {
                    this.MarkField(0);
                    Debug.Assert(size == 12);
                    this._Header = reader.ReadObject<PluginHeader>();
                    break;
                }

                case Fields.CNAM:
                {
                    this.MarkField(1);
                    this._Creator = reader.ReadString(size);
                    break;
                }

                case Fields.SNAM:
                {
                    this.MarkField(2);
                    this._Summary = reader.ReadString(size);
                    break;
                }

                case Fields.MAST:
                {
                    this._Masters.Add(reader.ReadString(size));
                    break;
                }

                case Fields.DATA:
                {
                    break;
                }

                case Fields.TNAM:
                {
                    break;
                }

                case Fields.INTV:
                {
                    this.MarkField(6);
                    Debug.Assert(size == 4);
                    break;
                }

                case Fields.INCC:
                {
                    this.MarkField(7);
                    Debug.Assert(size == 4);
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
            if (this._Header != null)
            {
                writer.WriteObject((uint)Fields.HEDR, this._Header);
            }

            if (string.IsNullOrEmpty(this._Creator) == false)
            {
                writer.WriteString((uint)Fields.CNAM, this._Creator);
            }

            if (string.IsNullOrEmpty(this._Summary) == false)
            {
                writer.WriteString((uint)Fields.SNAM, this._Summary);
            }

            foreach (var master in this._Masters)
            {
                writer.WriteString((uint)Fields.MAST, master, 260);
                writer.WriteValueU64((uint)Fields.DATA, 0); // is this necessary?
            }
        }
    }
}
