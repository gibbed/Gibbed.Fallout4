using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public class BoolValue : BaseValue
    {
        private bool _Value1;
        private bool _Value2;

        public BoolValue(bool value1, bool value2)
            : base(ValueType.Bool)
        {
            this._Value1 = value1;
            this._Value2 = value2;
        }

        public BoolValue(bool value1)
            : this(value1, false)
        {
        }

        public bool Value1
        {
            get { return this._Value1; }
            set { this._Value1 = value; }
        }

        public bool Value2
        {
            get { return this._Value2; }
            set { this._Value2 = value; }
        }

        internal static BoolValue Read(IFieldReader reader)
        {
            var value1 = reader.ReadValueB8();
            reader.SkipBytes(3);
            var value2 = reader.ReadValueB8();
            reader.SkipBytes(3);
            return new BoolValue(value1, value2);
        }

        internal override void Write(IFieldWriter writer)
        {
            writer.WriteValueU32(this._Value1 == true ? 1u : 0u);
            writer.WriteValueU32(this._Value2 == true ? 1u : 0u);
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this._Value1, this._Value2);
        }
    }
}
