using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public abstract class BaseValue
    {
        private readonly ValueType _Type;

        internal BaseValue(ValueType type)
        {
            this._Type = type;
        }

        public ValueType Type
        {
            get { return this._Type; }
        }

        internal abstract void Write(IFieldWriter writer);
    }
}
