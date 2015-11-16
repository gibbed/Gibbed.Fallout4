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
using System.IO;
using Gibbed.IO;
using Newtonsoft.Json;

namespace Gibbed.Fallout4.FileFormats
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EffectMaterialFile : BaseMaterialFile
    {
        public const uint Signature = 0x4D534742;

        #region Fields
        private string _BaseTexture;
        private string _GrayscaleTexture;
        private string _EnvmapTexture;
        private string _NormalTexture;
        private string _EnvmapMaskTexture;
        private bool _BloodEnabled;
        private bool _EffectLightingEnabled;
        private bool _FalloffEnabled;
        private bool _FalloffColorEnabled;
        private bool _GrayscaleToPaletteAlpha;
        private bool _SoftEnabled;
        private uint _BaseColor;
        private float _BaseColorScale = 1.0f;
        private float _FalloffStartAngle;
        private float _FalloffStopAngle;
        private float _FalloffStartOpacity;
        private float _FalloffStopOpacity;
        private float _LightingInfluence = 1.0f;
        private byte _EnvmapMinLOD;
        private float _SoftDepth = 100.0f;
        #endregion

        public EffectMaterialFile()
            : base(Signature)
        {
        }

        #region Properties
        [DefaultValue("")]
        [JsonProperty("sBaseTexture")]
        public string BaseTexture
        {
            get { return this._BaseTexture; }
            set { this._BaseTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sGrayscaleTexture")]
        public string GrayscaleTexture
        {
            get { return this._GrayscaleTexture; }
            set { this._GrayscaleTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sEnvmapTexture")]
        public string EnvmapTexture
        {
            get { return this._EnvmapTexture; }
            set { this._EnvmapTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sNormalTexture")]
        public string NormalTexture
        {
            get { return this._NormalTexture; }
            set { this._NormalTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sEnvmapMaskTexture")]
        public string EnvmapMaskTexture
        {
            get { return this._EnvmapMaskTexture; }
            set { this._EnvmapMaskTexture = value; }
        }

        [JsonProperty("bBloodEnabled")]
        public bool BloodEnabled
        {
            get { return this._BloodEnabled; }
            set { this._BloodEnabled = value; }
        }

        [JsonProperty("bEffectLightingEnabled")]
        public bool EffectLightingEnabled
        {
            get { return this._EffectLightingEnabled; }
            set { this._EffectLightingEnabled = value; }
        }

        [JsonProperty("bFalloffEnabled")]
        public bool FalloffEnabled
        {
            get { return this._FalloffEnabled; }
            set { this._FalloffEnabled = value; }
        }

        [JsonProperty("bFalloffColorEnabled")]
        public bool FalloffColorEnabled
        {
            get { return this._FalloffColorEnabled; }
            set { this._FalloffColorEnabled = value; }
        }

        [JsonProperty("bGrayscaleToPaletteAlpha")]
        public bool GrayscaleToPaletteAlpha
        {
            get { return this._GrayscaleToPaletteAlpha; }
            set { this._GrayscaleToPaletteAlpha = value; }
        }

        [JsonProperty("bSoftEnabled")]
        public bool SoftEnabled
        {
            get { return this._SoftEnabled; }
            set { this._SoftEnabled = value; }
        }

        [JsonConverter(typeof(StringColorConverter))]
        [JsonProperty("cBaseColor")]
        public uint BaseColor
        {
            get { return this._BaseColor; }
            set { this._BaseColor = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fBaseColorScale")]
        public float BaseColorScale
        {
            get { return this._BaseColorScale; }
            set { this._BaseColorScale = value; }
        }

        [JsonProperty("fFalloffStartAngle")]
        public float FalloffStartAngle
        {
            get { return this._FalloffStartAngle; }
            set { this._FalloffStartAngle = value; }
        }

        [JsonProperty("fFalloffStopAngle")]
        public float FalloffStopAngle
        {
            get { return this._FalloffStopAngle; }
            set { this._FalloffStopAngle = value; }
        }

        [JsonProperty("fFalloffStartOpacity")]
        public float FalloffStartOpacity
        {
            get { return this._FalloffStartOpacity; }
            set { this._FalloffStartOpacity = value; }
        }

        [JsonProperty("fFalloffStopOpacity")]
        public float FalloffStopOpacity
        {
            get { return this._FalloffStopOpacity; }
            set { this._FalloffStopOpacity = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fLightingInfluence")]
        public float LightingInfluence
        {
            get { return this._LightingInfluence; }
            set { this._LightingInfluence = value; }
        }

        [JsonProperty("iEnvmapMinLOD")]
        public byte EnvmapMinLOD
        {
            get { return this._EnvmapMinLOD; }
            set { this._EnvmapMinLOD = value; }
        }

        [DefaultValue(100.0f)]
        [JsonProperty("fSoftDepth")]
        public float SoftDepth
        {
            get { return this._SoftDepth; }
            set { this._SoftDepth = value; }
        }
        #endregion

        public override void Serialize(Stream output)
        {
            base.Serialize(output);
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            base.Deserialize(input);
            var endian = this.Endian;
            this._BaseTexture = ReadString(input, endian);
            this._GrayscaleTexture = ReadString(input, endian);
            this._EnvmapTexture = ReadString(input, endian);
            this._NormalTexture = ReadString(input, endian);
            this._EnvmapMaskTexture = ReadString(input, endian);
            this._BloodEnabled = input.ReadValueB8();
            this._EffectLightingEnabled = input.ReadValueB8();
            this._FalloffEnabled = input.ReadValueB8();
            this._FalloffColorEnabled = input.ReadValueB8();
            this._GrayscaleToPaletteAlpha = input.ReadValueB8();
            this._SoftEnabled = input.ReadValueB8();
            this._BaseColor = Color.Read(input, endian).ToUInt32();
            this._BaseColorScale = input.ReadValueF32(endian);
            this._FalloffStartAngle = input.ReadValueF32(endian);
            this._FalloffStopAngle = input.ReadValueF32(endian);
            this._FalloffStartOpacity = input.ReadValueF32(endian);
            this._FalloffStopOpacity = input.ReadValueF32(endian);
            this._LightingInfluence = input.ReadValueF32(endian);
            this._EnvmapMinLOD = input.ReadValueU8();
            this._SoftDepth = input.ReadValueF32(endian);
        }
    }
}
