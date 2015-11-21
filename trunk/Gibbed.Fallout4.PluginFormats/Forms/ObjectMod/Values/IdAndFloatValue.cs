using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public class IdAndFloatValue : BaseValue
    {
        private uint _Value1;
        private float _Value2;

        public IdAndFloatValue(uint value1, float value2)
            : base(ValueType.IdAndFloat)
        {
            this._Value1 = value1;
            this._Value2 = value2;
        }

        public IdAndFloatValue(uint value1)
            : this(value1, 0.0f)
        {
        }

        public uint Value1
        {
            get { return this._Value1; }
            set { this._Value1 = value; }
        }

        public float Value2
        {
            get { return this._Value2; }
            set { this._Value2 = value; }
        }

        internal static IdAndFloatValue Read(IFieldReader reader)
        {
            var value1 = reader.ReadValueU32();
            var value2 = reader.ReadValueF32();
            return new IdAndFloatValue(value1, value2);
        }

        internal override void Write(IFieldWriter writer)
        {
            writer.WriteValueU32(this._Value1);
            writer.WriteValueF32(this._Value2);
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this._Value1, this._Value2);
        }
    }
}
