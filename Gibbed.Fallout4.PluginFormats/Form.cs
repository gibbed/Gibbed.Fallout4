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

namespace Gibbed.Fallout4.PluginFormats
{
    public abstract class Form : BaseForm
    {
        private readonly FormType _Type;
        private readonly ushort _Version;

        protected Form(FormType type, ushort version)
        {
            this._Type = type;
            this._Version = version;
        }

        public override FormType Type
        {
            get { return this._Type; }
        }

        public override ushort Version
        {
            get { return this._Version; }
        }

        internal override void ReadFields(IFormReader reader)
        {
            while (true)
            {
                uint fieldType;
                using (var fieldReader = reader.ReadField(out fieldType))
                {
                    if (fieldReader == null)
                    {
                        break;
                    }

                    this.ReadField(fieldType, fieldReader);
                }
            }
        }

        internal abstract void ReadField(uint type, IFieldReader reader);

        private ulong _MarkedFields;

        protected void MarkField(int index)
        {
            var flag = 1ul << index;
            var flags = this._MarkedFields;
            if ((flags & flag) == flag)
            {
                throw new InvalidOperationException();
            }
            this._MarkedFields = flags | flag;
        }
    }
}
