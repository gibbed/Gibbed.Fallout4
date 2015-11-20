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

namespace Gibbed.Fallout4.PluginFormats.Forms
{
    public struct ConditionData
    {
        public byte Unknown00;
        public uint Unknown04;
        public ushort Unknown08;
        public uint Unknown0C;
        public uint Unknown10;
        public byte Unknown14;
        public uint Unknown18;
        public uint Unknown1C;

        internal static ConditionData Read(IFieldReader reader)
        {
            ConditionData instance;
            instance.Unknown00 = reader.ReadValueU8();
            reader.SkipBytes(3);
            instance.Unknown04 = reader.ReadValueU32();
            instance.Unknown08 = reader.ReadValueU16();
            reader.SkipBytes(2);
            instance.Unknown0C = reader.ReadValueU32();
            instance.Unknown10 = reader.ReadValueU32();
            instance.Unknown14 = reader.ReadValueU8();
            reader.SkipBytes(3);
            instance.Unknown18 = reader.ReadValueU32();
            instance.Unknown1C = reader.ReadValueU32();
            return instance;
        }
    }
}
