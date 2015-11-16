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
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using Gibbed.IO;
using Newtonsoft.Json;

namespace Gibbed.Fallout4.FileFormats
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseMaterialFile
    {
        private readonly uint _Signature;

        #region Fields
        private Endian _Endian;
        private uint _Version;
        private bool _TileU;
        private bool _TileV;
        private float _UOffset;
        private float _VOffset;
        private float _UScale = 1.0f;
        private float _VScale = 1.0f;
        private float _Alpha = 1.0f;
        private AlphaBlendModeType _AlphaBlendMode;
        private byte _AlphaTestRef;
        private bool _AlphaTest;
        private bool _ZBufferWrite = true;
        private bool _ZBufferTest = true;
        private bool _ScreenSpaceReflections;
        private bool _WetnessControlScreenSpaceReflections;
        private bool _Decal;
        private bool _TwoSided;
        private bool _DecalNoFade;
        private bool _NonOccluder;
        private bool _Refraction;
        private bool _RefractionFalloff;
        private float _RefractionPower;
        private bool _EnvironmentMapping;
        private float _EnvironmentMappingMaskScale = 1.0f;
        private bool _GrayscaleToPaletteColor;
        #endregion

        protected BaseMaterialFile(uint signature)
        {
            this._Signature = signature;
        }

        #region Properties
        public Endian Endian
        {
            get { return this._Endian; }
            set { this._Endian = value; }
        }

        public uint Version
        {
            get { return this._Version; }
            set { this._Version = value; }
        }

        [JsonProperty("bTileU")]
        public bool TileU
        {
            get { return this._TileU; }
            set { this._TileU = value; }
        }

        [JsonProperty("bTileV")]
        public bool TileV
        {
            get { return this._TileV; }
            set { this._TileV = value; }
        }

        [JsonProperty("fUOffset")]
        public float UOffset
        {
            get { return this._UOffset; }
            set { this._UOffset = value; }
        }

        [JsonProperty("fVOffset")]
        public float VOffset
        {
            get { return this._VOffset; }
            set { this._VOffset = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fUScale")]
        public float UScale
        {
            get { return this._UScale; }
            set { this._UScale = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fVScale")]
        public float VScale
        {
            get { return this._VScale; }
            set { this._VScale = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fAlpha")]
        public float Alpha
        {
            get { return this._Alpha; }
            set { this._Alpha = value; }
        }

        [DefaultValue(AlphaBlendModeType.Unknown)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("eAlphaBlendMode")]
        public AlphaBlendModeType AlphaBlendMode
        {
            get { return this._AlphaBlendMode; }
            set { this._AlphaBlendMode = value; }
        }

        [JsonProperty("fAlphaTestRef")]
        public byte AlphaTestRef
        {
            get { return this._AlphaTestRef; }
            set { this._AlphaTestRef = value; }
        }

        [JsonProperty("bAlphaTest")]
        public bool AlphaTest
        {
            get { return this._AlphaTest; }
            set { this._AlphaTest = value; }
        }

        [DefaultValue(true)]
        [JsonProperty("bZBufferWrite")]
        public bool ZBufferWrite
        {
            get { return this._ZBufferWrite; }
            set { this._ZBufferWrite = value; }
        }

        [DefaultValue(true)]
        [JsonProperty("bZBufferTest")]
        public bool ZBufferTest
        {
            get { return this._ZBufferTest; }
            set { this._ZBufferTest = value; }
        }

        [JsonProperty("bScreenSpaceReflections")]
        public bool ScreenSpaceReflections
        {
            get { return this._ScreenSpaceReflections; }
            set { this._ScreenSpaceReflections = value; }
        }

        [JsonProperty("bWetnessControl_ScreenSpaceReflections")]
        public bool WetnessControlScreenSpaceReflections
        {
            get { return this._WetnessControlScreenSpaceReflections; }
            set { this._WetnessControlScreenSpaceReflections = value; }
        }

        [JsonProperty("bDecal")]
        public bool Decal
        {
            get { return this._Decal; }
            set { this._Decal = value; }
        }

        [JsonProperty("bTwoSided")]
        public bool TwoSided
        {
            get { return this._TwoSided; }
            set { this._TwoSided = value; }
        }

        [JsonProperty("bDecalNoFade")]
        public bool DecalNoFade
        {
            get { return this._DecalNoFade; }
            set { this._DecalNoFade = value; }
        }

        [JsonProperty("bNonOccluder")]
        public bool NonOccluder
        {
            get { return this._NonOccluder; }
            set { this._NonOccluder = value; }
        }

        [JsonProperty("bRefraction")]
        public bool Refraction
        {
            get { return this._Refraction; }
            set { this._Refraction = value; }
        }

        [JsonProperty("bRefractionFalloff")]
        public bool RefractionFalloff
        {
            get { return this._RefractionFalloff; }
            set { this._RefractionFalloff = value; }
        }

        [JsonProperty("fRefractionPower")]
        public float RefractionPower
        {
            get { return this._RefractionPower; }
            set { this._RefractionPower = value; }
        }

        [JsonProperty("bEnvironmentMapping")]
        public bool EnvironmentMapping
        {
            get { return this._EnvironmentMapping; }
            set { this._EnvironmentMapping = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fEnvironmentMappingMaskScale")]
        public float EnvironmentMappingMaskScale
        {
            get { return this._EnvironmentMappingMaskScale; }
            set { this._EnvironmentMappingMaskScale = value; }
        }

        [JsonProperty("bGrayscaleToPaletteColor")]
        public bool GrayscaleToPaletteColor
        {
            get { return this._GrayscaleToPaletteColor; }
            set { this._GrayscaleToPaletteColor = value; }
        }
        #endregion

        public virtual void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public virtual void Deserialize(Stream input)
        {
            var magic = input.ReadValueU32(Endian.Little);
            if (magic != _Signature && magic.Swap() != _Signature)
            {
                throw new FormatException();
            }
            var endian = magic == _Signature ? Endian.Little : Endian.Big;

            this._Endian = endian;
            this._Version = input.ReadValueU32(endian);

            var tileFlags = input.ReadValueU32(endian);
            this._TileU = (tileFlags & 2) != 0;
            this._TileV = (tileFlags & 1) != 0;
            this._UOffset = input.ReadValueF32(endian);
            this._VOffset = input.ReadValueF32(endian);
            this._UScale = input.ReadValueF32(endian);
            this._VScale = input.ReadValueF32(endian);
            this._Alpha = input.ReadValueF32(endian);
            var alphaBlendMode0 = input.ReadValueU8();
            var alphaBlendMode1 = input.ReadValueU32(endian);
            var alphaBlendMode2 = input.ReadValueU32(endian);
            this._AlphaBlendMode = ConvertAlphaBlendMode(alphaBlendMode0, alphaBlendMode1, alphaBlendMode2);
            this._AlphaTestRef = input.ReadValueU8();
            this._AlphaTest = input.ReadValueB8();
            this._ZBufferWrite = input.ReadValueB8();
            this._ZBufferTest = input.ReadValueB8();
            this._ScreenSpaceReflections = input.ReadValueB8();
            this._WetnessControlScreenSpaceReflections = input.ReadValueB8();
            this._Decal = input.ReadValueB8();
            this._TwoSided = input.ReadValueB8();
            this._DecalNoFade = input.ReadValueB8();
            this._NonOccluder = input.ReadValueB8();
            this._Refraction = input.ReadValueB8();
            this._RefractionFalloff = input.ReadValueB8();
            this._RefractionPower = input.ReadValueF32(endian);
            this._EnvironmentMapping = input.ReadValueB8();
            this._EnvironmentMappingMaskScale = input.ReadValueF32(endian);
            this._GrayscaleToPaletteColor = input.ReadValueB8();
        }

        protected static string ReadString(Stream input, Endian endian)
        {
            var length = input.ReadValueU32(endian);
            return input.ReadString(length, true, Encoding.ASCII);
        }

        public static BaseMaterialFile Read(Stream input)
        {
            var magic = input.ReadValueU32(Endian.Little);
            input.Seek(-4, SeekOrigin.Current);

            if (magic == MaterialFile.Signature || magic.Swap() == MaterialFile.Signature)
            {
                var instance = new MaterialFile();
                instance.Deserialize(input);
                return instance;
            }

            if (magic == EffectMaterialFile.Signature || magic.Swap() == EffectMaterialFile.Signature)
            {
                var instance = new EffectMaterialFile();
                instance.Deserialize(input);
                return instance;
            }

            throw new NotSupportedException();
        }

        public enum AlphaBlendModeType
        {
            Unknown = 0,
            None,
            Standard,
            Additive,
            Multiplicative,
        }

        private static AlphaBlendModeType ConvertAlphaBlendMode(byte a, uint b, uint c)
        {
            if (a == 0 && b == 6 && c == 7)
            {
                return AlphaBlendModeType.Unknown;
            }

            if (a == 0 && b == 0 && c == 0)
            {
                return AlphaBlendModeType.None;
            }

            if (a == 1 && b == 6 && c == 7)
            {
                return AlphaBlendModeType.Standard;
            }

            if (a == 1 && b == 6 && c == 0)
            {
                return AlphaBlendModeType.Additive;
            }

            if (a == 1 && b == 4 && c == 1)
            {
                return AlphaBlendModeType.Multiplicative;
            }

            throw new NotSupportedException();
        }

        protected struct Color
        {
            public readonly float R;
            public readonly float G;
            public readonly float B;

            public Color(float r, float g, float b)
            {
                this.R = r;
                this.G = g;
                this.B = b;
            }

            public uint ToUInt32()
            {
                uint value = 0;
                value |= (byte)(this.R * 255);
                value <<= 8;
                value |= (byte)(this.G * 255);
                value <<= 8;
                value |= (byte)(this.B * 255);
                return value;
            }

            public static Color FromUInt32(uint value)
            {
                const float multiplier = 1.0f / 255;
                var b = (byte)(value & 0xFF);
                value >>= 8;
                var g = (byte)(value & 0xFF);
                value >>= 8;
                var r = (byte)(value & 0xFF);
                return new Color(r * multiplier, g * multiplier, b * multiplier);
            }

            public static Color Read(Stream input, Endian endian)
            {
                var r = input.ReadValueF32(endian);
                var g = input.ReadValueF32(endian);
                var b = input.ReadValueF32(endian);
                return new Color(r, g, b);
            }

            public static void Write(Stream output, Color instance, Endian endian)
            {
                output.WriteValueF32(instance.R, endian);
                output.WriteValueF32(instance.G, endian);
                output.WriteValueF32(instance.B, endian);
            }

            public void Write(Stream output, Endian endian)
            {
                Write(output, this, endian);
            }
        }

        public class StringColorConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteNull();
                }
                else
                {
                    var color = (uint)value;
                    writer.WriteValue(string.Format("#{0:x6}", color & 0xFFFFFFu));
                }
            }

            public override object ReadJson(JsonReader reader,
                                            Type objectType,
                                            object existingValue,
                                            JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return new Color(0, 0, 0);
                }

                if (reader.TokenType == JsonToken.String)
                {
                    var text = reader.Value.ToString().ToLowerInvariant();
                    if (text.StartsWith("#") == true)
                    {
                        text = text.Substring(1);
                    }

                    if (text == "000")
                    {
                        return 0x000000u;
                    }

                    if (text == "fff")
                    {
                        return 0xFFFFFFu;
                    }

                    if (text.Length == 3)
                    {
                        uint value = uint.Parse(text, NumberStyles.AllowHexSpecifier);
                        value = ((value & 0xF00) << 8) |
                                ((value & 0x0F0) << 4) |
                                ((value & 0x00F) << 0);
                        value |= value << 4;
                        return value;
                    }

                    if (text.Length == 6)
                    {
                        return uint.Parse(text, NumberStyles.AllowHexSpecifier);
                    }
                }

                throw new NotSupportedException();
            }

            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }
        }
    }
}
