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
using System.Collections.Generic;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Fallout4.PluginFormats
{
    public class PluginReader
    {
        internal const uint Signature = (uint)FormType.TES4;
        internal const int RecordHeaderSize = 24;

        private readonly Endian _Endian;
        private readonly Stream _Stream;
        private readonly Dictionary<uint, Tuple<FormType, long>> _Forms;
        private readonly bool _IsLocalized;

        private PluginReader(Stream stream, Dictionary<uint, Tuple<FormType, long>> forms, Endian endian)
        {
            if (forms == null)
            {
                throw new ArgumentNullException("forms");
            }

            this._Endian = endian;
            this._Stream = stream;
            this._Forms = forms;

            var header = this.ReadForm<Forms.PluginForm>(0);
            if (header == null)
            {
                throw new FormatException();
            }

            this._IsLocalized = header.IsLocalized;
        }

        public IEnumerable<RawForm> ReadRawForms(FormType type)
        {
            var endian = this._Endian;
            var input = this._Stream;
            var isLocalized = this._IsLocalized;

            foreach (var kv in this._Forms)
            {
                if (kv.Value.Item1 == type)
                {
                    input.Position = kv.Value.Item2;
                    var instance = new RawForm();
                    instance.Deserialize(input, endian, isLocalized);
                    yield return instance;
                }
            }
        }

        public IEnumerable<T> ReadForms<T>()
            where T : BaseForm, new()
        {
            var endian = this._Endian;
            var input = this._Stream;
            var isLocalized = this._IsLocalized;

            var instance = new T();
            foreach (var kv in this._Forms)
            {
                if (kv.Value.Item1 == instance.Type)
                {
                    input.Position = kv.Value.Item2;
                    instance.Deserialize(input, endian, isLocalized);
                    yield return instance;
                    instance = new T();
                }
            }
        }

        public RawForm ReadRawForm(uint id)
        {
            if (this._Forms.ContainsKey(id) == false)
            {
                return null;
            }

            var endian = this._Endian;
            var input = this._Stream;
            var isLocalized = this._IsLocalized;

            var kv = this._Forms[id];
            input.Position = kv.Item2;
            var instance = new RawForm();
            instance.Deserialize(input, endian, isLocalized);
            return instance;
        }

        public T ReadForm<T>(uint id)
            where T : BaseForm, new()
        {
            var instance = new T();
            if (this._Forms.ContainsKey(id) == false)
            {
                return default(T);
            }

            if (this._Forms[id].Item1 != instance.Type)
            {
                throw new InvalidOperationException();
            }

            var endian = this._Endian;
            var input = this._Stream;
            var isLocalized = this._IsLocalized;

            var kv = this._Forms[id];
            input.Position = kv.Item2;
            instance.Deserialize(input, endian, isLocalized);
            return instance;
        }

        public static PluginReader Read(Stream input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var basePosition = input.Position;

            var magic = input.ReadValueU32(Endian.Little);
            if (magic != Signature && magic.Swap() != Signature)
            {
                throw new FormatException();
            }
            var endian = magic == Signature ? Endian.Little : Endian.Big;

            var queue = new Queue<Tuple<FormType, long, long>>();
            queue.Enqueue(new Tuple<FormType, long, long>(FormType.NONE, basePosition, input.Length));

            var forms = new Dictionary<uint, Tuple<FormType, long>>();
            while (queue.Count > 0)
            {
                var kv = queue.Dequeue();
                var rangeType = kv.Item1;
                var startPosition = kv.Item2;
                var endPosition = kv.Item3;

                input.Position = startPosition;
                while (input.Position < endPosition)
                {
                    if (input.Position + RecordHeaderSize > input.Length)
                    {
                        throw new EndOfStreamException();
                    }

                    var position = input.Position;
                    var type = (FormType)input.ReadValueU32(endian);
                    if (type == FormType.GRUP)
                    {
                        var groupSize = input.ReadValueU32(endian);
                        var groupLabel = (FormType)input.ReadValueU32(endian);
                        var groupType = input.ReadValueU32(endian);

                        if (groupSize < 24)
                        {
                            throw new FormatException();
                        }

                        if (groupType != 0)
                        {
                            // skip non-form groups
                            input.Seek(groupSize - 16, SeekOrigin.Current);
                        }
                        else
                        {
                            if (rangeType != FormType.NONE && rangeType != groupLabel)
                            {
                                throw new FormatException();
                            }

                            var groupStartPosition = input.Position + 4 + 2 + 2;
                            var groupEndPosition = groupStartPosition + groupSize - 24;
                            queue.Enqueue(new Tuple<FormType, long, long>(
                                              groupLabel,
                                              groupStartPosition,
                                              groupEndPosition));
                            queue.Enqueue(new Tuple<FormType, long, long>(rangeType, groupEndPosition, endPosition));
                            break;
                        }
                    }
                    else
                    {
                        if (rangeType != FormType.NONE && rangeType != type)
                        {
                            throw new FormatException();
                        }

                        var formSize = input.ReadValueU32(endian);
                        var formFlags = input.ReadValueU32(endian);
                        var formId = input.ReadValueU32(endian);
                        input.Seek(8 + formSize, SeekOrigin.Current);
                        forms.Add(formId, new Tuple<FormType, long>(type, position));
                    }
                }
            }

            return new PluginReader(input, forms, endian);
        }
    }
}
