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

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public static class Properties
    {
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
    }
}
