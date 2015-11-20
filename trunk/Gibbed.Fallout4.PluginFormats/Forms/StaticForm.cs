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
    public class StaticForm : Form
    {
        #region Fields
        private string _EditorId;
        private LocalizedString _FullName;
        private LocalizedString _Description;
        private string _Model;
        #endregion

        public StaticForm()
            : base(FormType.STAT, 131)
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

        public string Model
        {
            get { return this._Model; }
            set { this._Model = value; }
        }
        #endregion

        private enum FieldType : uint
        {
            // ReSharper disable InconsistentNaming
            EDID = 0x44494445,
            FULL = 0x4C4C5546,
            DESC = 0x43534544,
            OBND = 0x444E424F,
            MODL = 0x4C444F4D,
            MODT = 0x54444F4D,
            DNAM = 0x4D414E44,
            MNAM = 0x4D414E4D,
            PRPS = 0x53505250,
            PTRN = 0x4E525450,
            MODS = 0x53444F4D,
            NVNM = 0x4D4E564E, // Navmesh
            MODC = 0x43444F4D,
            VMAD = 0x44414D56,
            FTYP = 0x50595446,
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

                case FieldType.OBND:
                {
                    this.MarkField(3);
                    Assert(size == 12);
                    break;
                }

                case FieldType.MODL:
                {
                    this.MarkField(4);
                    this._Model = reader.ReadString(0);
                    break;
                }

                case FieldType.MODT:
                {
                    this.MarkField(5);
                    break;
                }

                case FieldType.DNAM:
                {
                    this.MarkField(6);
                    Assert((reader.Version >= 107 && size == 16) || (reader.Version < 107 && size == 8));

                    var value1 = reader.ReadValueF32();
                    var value2 = reader.ReadValueU32();
                    if (reader.Version >= 107)
                    {
                        var value3 = reader.ReadValueF32();
                        var value4 = reader.ReadValueF32();
                    }
                    else
                    {
                    }

                    break;
                }

                case FieldType.MNAM:
                {
                    this.MarkField(7);
                    Assert(size == 1040);
                    break;
                }

                case FieldType.PRPS:
                {
                    this.MarkField(8);
                    //Assert(size == 8);
                    break;
                }

                case FieldType.PTRN:
                {
                    this.MarkField(9);
                    Assert(size == 4);
                    break;
                }

                case FieldType.MODS:
                {
                    this.MarkField(10);
                    Assert(size == 4);
                    break;
                }

                case FieldType.NVNM:
                {
                    this.MarkField(11);
                    //Assert(size == 720);
                    break;
                }

                case FieldType.MODC:
                {
                    this.MarkField(12);
                    Assert(size == 4);
                    break;
                }

                case FieldType.VMAD:
                {
                    this.MarkField(13);
                    Assert(size == 36);
                    break;
                }

                case FieldType.FTYP:
                {
                    this.MarkField(14);
                    Assert(size == 4);
                    break;
                }
            }

            throw new NotSupportedException();
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
