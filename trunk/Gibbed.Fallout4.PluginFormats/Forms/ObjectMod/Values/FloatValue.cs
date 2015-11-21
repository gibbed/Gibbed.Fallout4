using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public class FloatValue : BaseValue
    {
        private float _Value1;
        private float _Value2;

        public FloatValue(float value1, float value2)
            : base(ValueType.Float)
        {
            this._Value1 = value1;
            this._Value2 = value2;
        }

        public FloatValue(float value1)
            : this(value1, 0.0f)
        {
        }

        public float Value1
        {
            get { return this._Value1; }
            set { this._Value1 = value; }
        }

        public float Value2
        {
            get { return this._Value2; }
            set { this._Value2 = value; }
        }

        internal static FloatValue Read(IFieldReader reader)
        {
            var value1 = reader.ReadValueF32();
            var value2 = reader.ReadValueF32();
            return new FloatValue(value1, value2);
        }

        internal override void Write(IFieldWriter writer)
        {
            writer.WriteValueF32(this._Value1);
            writer.WriteValueF32(this._Value2);
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this._Value1, this._Value2);
        }
    }
}
