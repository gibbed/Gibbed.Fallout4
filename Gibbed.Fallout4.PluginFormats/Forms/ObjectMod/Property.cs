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

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public class Property
    {
        #region Fields
        private FunctionType _Function;
        private ushort _Index;
        private BaseValue _Value;
        private float _Factor;
        #endregion

        public Property(ushort index, FunctionType function, BaseValue value, float factor)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this._Function = function;
            this._Index = index;
            this._Value = value;
            this._Factor = factor;
        }

        public Property(ushort index, FunctionType function, BaseValue value)
            : this(index, function, value, 0.0f)
        {
        }

        #region Properties
        public FunctionType Function
        {
            get { return this._Function; }
            set { this._Function = value; }
        }

        public ushort Index
        {
            get { return this._Index; }
            set { this._Index = value; }
        }

        public BaseValue Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        public float Factor
        {
            get { return this._Factor; }
            set { this._Factor = value; }
        }
        #endregion

        private static BaseValue ReadValue(ValueType type, IFieldReader reader)
        {
            switch (type)
            {
                case ValueType.Int:
                {
                    return IntValue.Read(reader);
                }

                case ValueType.Float:
                {
                    return FloatValue.Read(reader);
                }

                case ValueType.Bool:
                {
                    return BoolValue.Read(reader);
                }

                case ValueType.IdAndInt:
                {
                    return IdAndIntValue.Read(reader);
                }

                case ValueType.Enum:
                {
                    return EnumValue.Read(reader);
                }

                case ValueType.IdAndFloat:
                {
                    return IdAndFloatValue.Read(reader);
                }
            }

            throw new NotImplementedException();
        }

        internal static Property Read(IFieldReader reader)
        {
            var valueType = (ValueType)reader.ReadValueU8(); // & 7
            reader.SkipBytes(3);
            var function = (FunctionType)reader.ReadValueU8(); // & 3
            reader.SkipBytes(3);
            var index = reader.ReadValueU16(); // & 0x7FF
            reader.SkipBytes(2);
            var value = ReadValue(valueType, reader);
            var factor = reader.Version >= 88 ? reader.ReadValueF32() : 0;
            return new Property(index, function, value, factor);
        }

        internal void Write(IFieldWriter writer)
        {
            writer.WriteValueU32((byte)this._Value.Type);
            writer.WriteValueU32(((byte)this._Function) & 3u);
            writer.WriteValueU32(this._Index & 0x7FFu);
            this._Value.Write(writer);
            writer.WriteValueF32(this._Factor);
        }

        public override string ToString()
        {
            if (this._Factor.Equals(0.0f) == true)
            {
                return string.Format("{1}={0}({2})",
                                     this._Function,
                                     this._Index,
                                     this._Value);
            }

            return string.Format("{1}={0}({2}){3}",
                                 this._Function,
                                 this._Index,
                                 this._Value,
                                 this._Factor);
        }

        #region Helpers
        public static Property Set(ushort index, int value1, int value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Set, new IntValue(value1, value2), factor);
        }

        public static Property Set(ushort index, float value1, float value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Set, new FloatValue(value1, value2), factor);
        }

        public static Property Set(ushort index, bool value1, bool value2 = false, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Set, new BoolValue(value1, value2), factor);
        }

        public static Property Set(ushort index, uint value1, int value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Set, new IdAndIntValue(value1, value2), factor);
        }

        public static Property Set(ushort index, uint value1, uint value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Set, new EnumValue(value1, value2), factor);
        }

        public static Property Set(ushort index, uint value1, float value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Set, new IdAndFloatValue(value1, value2), factor);
        }

        public static Property Multiply(ushort index, int value1, int value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Multiply, new IntValue(value1, value2), factor);
        }

        public static Property Multiply(ushort index, float value1, float value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Multiply, new FloatValue(value1, value2), factor);
        }

        public static Property Multiply(ushort index, bool value1, bool value2 = false, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Multiply, new BoolValue(value1, value2), factor);
        }

        public static Property Multiply(ushort index, uint value1, int value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Multiply, new IdAndIntValue(value1, value2), factor);
        }

        public static Property Multiply(ushort index, uint value1, uint value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Multiply, new EnumValue(value1, value2), factor);
        }

        public static Property Multiply(ushort index, uint value1, float value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Multiply, new IdAndFloatValue(value1, value2), factor);
        }

        public static Property Add(ushort index, int value1, int value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Add, new IntValue(value1, value2), factor);
        }

        public static Property Add(ushort index, float value1, float value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Add, new FloatValue(value1, value2), factor);
        }

        public static Property Add(ushort index, bool value1, bool value2 = false, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Add, new BoolValue(value1, value2), factor);
        }

        public static Property Add(ushort index, uint value1, int value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Add, new IdAndIntValue(value1, value2), factor);
        }

        public static Property Add(ushort index, uint value1, uint value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Add, new EnumValue(value1, value2), factor);
        }

        public static Property Add(ushort index, uint value1, float value2 = 0, float factor = 0.0f)
        {
            return new Property(index, FunctionType.Add, new IdAndFloatValue(value1, value2), factor);
        }
        #endregion
    }
}
