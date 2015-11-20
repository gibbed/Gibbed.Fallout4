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
    internal interface IFormWriter
    {
        ushort Version { get; }

        void WriteValueS32(uint type, int value);
        void WriteValueU32(uint type, uint value);
        void WriteValueU64(uint type, ulong value);
        void WriteValueF32(uint type, float value);

        void WriteString(uint type, string value);
        void WriteString(uint type, string value, int maximumLength);
        void WriteLocalizedString(uint type, LocalizedString value);

        void WriteData(uint type, Action<IFieldWriter> writeAction);
        void WriteObject<T>(uint type, T instance) where T : Field;
    }
}
