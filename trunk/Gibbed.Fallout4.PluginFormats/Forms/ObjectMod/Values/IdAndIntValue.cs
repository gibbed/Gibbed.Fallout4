using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public class IdAndIntValue : BaseValue
    {
        private uint _Value1;
        private int _Value2;

        public IdAndIntValue(uint value1, int value2)
            : base(ValueType.IdAndInt)
        {
            this._Value1 = value1;
            this._Value2 = value2;
        }

        public IdAndIntValue(uint value1)
            : this(value1, 0)
        {
        }

        public uint Value1
        {
            get { return this._Value1; }
            set { this._Value1 = value; }
        }

        public int Value2
        {
            get { return this._Value2; }
            set { this._Value2 = value; }
        }

        internal static IdAndIntValue Read(IFieldReader reader)
        {
            var value1 = reader.ReadValueU32();
            var value2 = reader.ReadValueS32();
            return new IdAndIntValue(value1, value2);
        }

        internal override void Write(IFieldWriter writer)
        {
            writer.WriteValueU32(this._Value1);
            writer.WriteValueS32(this._Value2);
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this._Value1, this._Value2);
        }
    }
}
