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
    public class MaterialFile : BaseMaterialFile
    {
        public const uint Signature = 0x4D534742;

        #region Fields
        private string _DiffuseTexture = "";
        private string _NormalTexture = "";
        private string _SmoothSpecTexture = "";
        private string _GreyscaleTexture = "";
        private string _EnvmapTexture = "";
        private string _GlowTexture = "";
        private string _InnerLayerTexture = "";
        private string _WrinklesTexture = "";
        private string _DisplacementTexture = "";
        private bool _EnableEditorAlphaRef;
        private bool _RimLighting;
        private float _RimPower;
        private float _BackLightPower;
        private bool _SubsurfaceLighting;
        private float _SubsurfaceLightingRolloff;
        private bool _SpecularEnabled;
        private uint _SpecularColor;
        private float _SpecularMult;
        private float _Smoothness = 1.0f;
        private float _FresnelPower = 5.0f;
        private float _WetnessControlSpecScale = -1.0f;
        private float _WetnessControlSpecPowerScale = -1.0f;
        private float _WetnessControlSpecMinvar = -1.0f;
        private float _WetnessControlEnvMapScale = -1.0f;
        private float _WetnessControlFresnelPower = -1.0f;
        private float _WetnessControlMetalness = -1.0f;
        private string _RootMaterialPath = "";
        private bool _AnisoLighting;
        private bool _EmitEnabled;
        private uint _EmittanceColor;
        private float _EmittanceMult = 1.0f;
        private bool _ModelSpaceNormals;
        private bool _ExternalEmittance;
        private bool _BackLighting;
        private bool _ReceiveShadows;
        private bool _HideSecret;
        private bool _CastShadows;
        private bool _DissolveFade;
        private bool _AssumeShadowmask;
        private bool _Glowmap;
        private bool _EnvironmentMappingWindow;
        private bool _EnvironmentMappingEye;
        private bool _Hair;
        private uint _HairTintColor = 0x808080;
        private bool _Tree;
        private bool _Facegen;
        private bool _SkinTint;
        private bool _Tessellate;
        private float _DisplacementTextureBias;
        private float _DisplacementTextureScale;
        private float _TessellationPNScale;
        private float _TessellationBaseFactor;
        private float _TessellationFadeDistance;
        private float _GrayscaleToPaletteScale = 1.0f;
        private bool _SkewSpecularAlpha;
        #endregion

        public MaterialFile()
            : base(Signature)
        {
        }

        #region Properties
        [DefaultValue("")]
        [JsonProperty("sDiffuseTexture")]
        public string DiffuseTexture
        {
            get { return this._DiffuseTexture; }
            set { this._DiffuseTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sNormalTexture")]
        public string NormalTexture
        {
            get { return this._NormalTexture; }
            set { this._NormalTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sSmoothSpecTexture")]
        public string SmoothSpecTexture
        {
            get { return this._SmoothSpecTexture; }
            set { this._SmoothSpecTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sGreyscaleTexture")]
        public string GreyscaleTexture
        {
            get { return this._GreyscaleTexture; }
            set { this._GreyscaleTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sEnvmapTexture")]
        public string EnvmapTexture
        {
            get { return this._EnvmapTexture; }
            set { this._EnvmapTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sGlowTexture")]
        public string GlowTexture
        {
            get { return this._GlowTexture; }
            set { this._GlowTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sInnerLayerTexture")]
        public string InnerLayerTexture
        {
            get { return this._InnerLayerTexture; }
            set { this._InnerLayerTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sWrinklesTexture")]
        public string WrinklesTexture
        {
            get { return this._WrinklesTexture; }
            set { this._WrinklesTexture = value; }
        }

        [DefaultValue("")]
        [JsonProperty("sDisplacementTexture")]
        public string DisplacementTexture
        {
            get { return this._DisplacementTexture; }
            set { this._DisplacementTexture = value; }
        }

        [JsonProperty("bEnableEditorAlphaRef")]
        public bool EnableEditorAlphaRef
        {
            get { return this._EnableEditorAlphaRef; }
            set { this._EnableEditorAlphaRef = value; }
        }

        [JsonProperty("bRimLighting")]
        public bool RimLighting
        {
            get { return this._RimLighting; }
            set { this._RimLighting = value; }
        }

        [JsonProperty("fRimPower")]
        public float RimPower
        {
            get { return this._RimPower; }
            set { this._RimPower = value; }
        }

        [JsonProperty("fBackLightPower")]
        public float BackLightPower
        {
            get { return this._BackLightPower; }
            set { this._BackLightPower = value; }
        }

        [JsonProperty("bSubsurfaceLighting")]
        public bool SubsurfaceLighting
        {
            get { return this._SubsurfaceLighting; }
            set { this._SubsurfaceLighting = value; }
        }

        [JsonProperty("fSubsurfaceLightingRolloff")]
        public float SubsurfaceLightingRolloff
        {
            get { return this._SubsurfaceLightingRolloff; }
            set { this._SubsurfaceLightingRolloff = value; }
        }

        [JsonProperty("bSpecularEnabled")]
        public bool SpecularEnabled
        {
            get { return this._SpecularEnabled; }
            set { this._SpecularEnabled = value; }
        }

        [JsonConverter(typeof(StringColorConverter))]
        [JsonProperty("sSpecularEnabled")]
        public uint SpecularColor
        {
            get { return this._SpecularColor; }
            set { this._SpecularColor = value; }
        }

        [JsonProperty("fSpecularMult")]
        public float SpecularMult
        {
            get { return this._SpecularMult; }
            set { this._SpecularMult = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fSmoothness")]
        public float Smoothness
        {
            get { return this._Smoothness; }
            set { this._Smoothness = value; }
        }

        [DefaultValue(5.0f)]
        [JsonProperty("fFresnelPower")]
        public float FresnelPower
        {
            get { return this._FresnelPower; }
            set { this._FresnelPower = value; }
        }

        [DefaultValue(-1.0f)]
        [JsonProperty("fWetnessControl_SpecScale")]
        public float WetnessControlSpecScale
        {
            get { return this._WetnessControlSpecScale; }
            set { this._WetnessControlSpecScale = value; }
        }

        [DefaultValue(-1.0f)]
        [JsonProperty("fWetnessControl_SpecPowerScale")]
        public float WetnessControlSpecPowerScale
        {
            get { return this._WetnessControlSpecPowerScale; }
            set { this._WetnessControlSpecPowerScale = value; }
        }

        [DefaultValue(-1.0f)]
        [JsonProperty("fWetnessControl_SpecMinvar")]
        public float WetnessControlSpecMinvar
        {
            get { return this._WetnessControlSpecMinvar; }
            set { this._WetnessControlSpecMinvar = value; }
        }

        [DefaultValue(-1.0f)]
        [JsonProperty("fWetnessControl_EnvMapScale")]
        public float WetnessControlEnvMapScale
        {
            get { return this._WetnessControlEnvMapScale; }
            set { this._WetnessControlEnvMapScale = value; }
        }

        [DefaultValue(-1.0f)]
        [JsonProperty("fWetnessControl_FresnelPower")]
        public float WetnessControlFresnelPower
        {
            get { return this._WetnessControlFresnelPower; }
            set { this._WetnessControlFresnelPower = value; }
        }

        [DefaultValue(-1.0f)]
        [JsonProperty("fWetnessControl_Metalness")]
        public float WetnessControlMetalness
        {
            get { return this._WetnessControlMetalness; }
            set { this._WetnessControlMetalness = value; }
        }

        [JsonProperty("sRootMaterialPath")]
        public string RootMaterialPath
        {
            get { return this._RootMaterialPath; }
            set { this._RootMaterialPath = value; }
        }

        [JsonProperty("bAnisoLighting")]
        public bool AnisoLighting
        {
            get { return this._AnisoLighting; }
            set { this._AnisoLighting = value; }
        }

        [JsonProperty("bEmitEnabled")]
        public bool EmitEnabled
        {
            get { return this._EmitEnabled; }
            set { this._EmitEnabled = value; }
        }

        [JsonConverter(typeof(StringColorConverter))]
        [JsonProperty("cEmittanceColor")]
        public uint EmittanceColor
        {
            get { return this._EmittanceColor; }
            set { this._EmittanceColor = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fEmittanceMult")]
        public float EmittanceMult
        {
            get { return this._EmittanceMult; }
            set { this._EmittanceMult = value; }
        }

        [JsonProperty("bModelSpaceNormals")]
        public bool ModelSpaceNormals
        {
            get { return this._ModelSpaceNormals; }
            set { this._ModelSpaceNormals = value; }
        }

        [JsonProperty("bExternalEmittance")]
        public bool ExternalEmittance
        {
            get { return this._ExternalEmittance; }
            set { this._ExternalEmittance = value; }
        }

        [JsonProperty("bBackLighting")]
        public bool BackLighting
        {
            get { return this._BackLighting; }
            set { this._BackLighting = value; }
        }

        [JsonProperty("bReceiveShadows")]
        public bool ReceiveShadows
        {
            get { return this._ReceiveShadows; }
            set { this._ReceiveShadows = value; }
        }

        [JsonProperty("bHideSecret")]
        public bool HideSecret
        {
            get { return this._HideSecret; }
            set { this._HideSecret = value; }
        }

        [JsonProperty("bCastShadows")]
        public bool CastShadows
        {
            get { return this._CastShadows; }
            set { this._CastShadows = value; }
        }

        [JsonProperty("bDissolveFade")]
        public bool DissolveFade
        {
            get { return this._DissolveFade; }
            set { this._DissolveFade = value; }
        }

        [JsonProperty("bAssumeShadowmask")]
        public bool AssumeShadowmask
        {
            get { return this._AssumeShadowmask; }
            set { this._AssumeShadowmask = value; }
        }

        [JsonProperty("bGlowmap")]
        public bool Glowmap
        {
            get { return this._Glowmap; }
            set { this._Glowmap = value; }
        }

        [JsonProperty("bEnvironmentMappingWindow")]
        public bool EnvironmentMappingWindow
        {
            get { return this._EnvironmentMappingWindow; }
            set { this._EnvironmentMappingWindow = value; }
        }

        [JsonProperty("bEnvironmentMappingEye")]
        public bool EnvironmentMappingEye
        {
            get { return this._EnvironmentMappingEye; }
            set { this._EnvironmentMappingEye = value; }
        }

        [JsonProperty("bHair")]
        public bool Hair
        {
            get { return this._Hair; }
            set { this._Hair = value; }
        }

        [DefaultValue(0x808080u)]
        [JsonConverter(typeof(StringColorConverter))]
        [JsonProperty("cHairTintColor")]
        public uint HairTintColor
        {
            get { return this._HairTintColor; }
            set { this._HairTintColor = value; }
        }

        [JsonProperty("bTree")]
        public bool Tree
        {
            get { return this._Tree; }
            set { this._Tree = value; }
        }

        [JsonProperty("bFacegen")]
        public bool Facegen
        {
            get { return this._Facegen; }
            set { this._Facegen = value; }
        }

        [JsonProperty("bSkinTint")]
        public bool SkinTint
        {
            get { return this._SkinTint; }
            set { this._SkinTint = value; }
        }

        [JsonProperty("bTessellate")]
        public bool Tessellate
        {
            get { return this._Tessellate; }
            set { this._Tessellate = value; }
        }

        [JsonProperty("fDisplacementTextureBias")]
        public float DisplacementTextureBias
        {
            get { return this._DisplacementTextureBias; }
            set { this._DisplacementTextureBias = value; }
        }

        [JsonProperty("fDisplacementTextureScale")]
        public float DisplacementTextureScale
        {
            get { return this._DisplacementTextureScale; }
            set { this._DisplacementTextureScale = value; }
        }

        [JsonProperty("fTessellationPnScale")]
        public float TessellationPnScale
        {
            get { return this._TessellationPNScale; }
            set { this._TessellationPNScale = value; }
        }

        [JsonProperty("fTessellationBaseFactor")]
        public float TessellationBaseFactor
        {
            get { return this._TessellationBaseFactor; }
            set { this._TessellationBaseFactor = value; }
        }

        [JsonProperty("fTessellationFadeDistance")]
        public float TessellationFadeDistance
        {
            get { return this._TessellationFadeDistance; }
            set { this._TessellationFadeDistance = value; }
        }

        [DefaultValue(1.0f)]
        [JsonProperty("fGrayscaleToPaletteScale")]
        public float GrayscaleToPaletteScale
        {
            get { return this._GrayscaleToPaletteScale; }
            set { this._GrayscaleToPaletteScale = value; }
        }

        [JsonProperty("bSkewSpecularAlpha")]
        public bool SkewSpecularAlpha
        {
            get { return this._SkewSpecularAlpha; }
            set { this._SkewSpecularAlpha = value; }
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
            var version = this.Version;
            this._DiffuseTexture = ReadString(input, endian);
            this._NormalTexture = ReadString(input, endian);
            this._SmoothSpecTexture = ReadString(input, endian);
            this._GreyscaleTexture = ReadString(input, endian);
            this._EnvmapTexture = ReadString(input, endian);
            this._GlowTexture = ReadString(input, endian);
            this._InnerLayerTexture = ReadString(input, endian);
            this._WrinklesTexture = ReadString(input, endian);
            this._DisplacementTexture = ReadString(input, endian);
            this._EnableEditorAlphaRef = input.ReadValueB8();
            this._RimLighting = input.ReadValueB8();
            this._RimPower = input.ReadValueF32(endian);
            this._BackLightPower = input.ReadValueF32(endian);
            this._SubsurfaceLighting = input.ReadValueB8();
            this._SubsurfaceLightingRolloff = input.ReadValueF32(endian);
            this._SpecularEnabled = input.ReadValueB8();
            this._SpecularColor = Color.Read(input, endian).ToUInt32();
            this._SpecularMult = input.ReadValueF32(endian);
            this._Smoothness = input.ReadValueF32(endian);
            this._FresnelPower = input.ReadValueF32(endian);
            this._WetnessControlSpecScale = input.ReadValueF32(endian);
            this._WetnessControlSpecPowerScale = input.ReadValueF32(endian);
            this._WetnessControlSpecMinvar = input.ReadValueF32(endian);
            this._WetnessControlEnvMapScale = input.ReadValueF32(endian);
            this._WetnessControlFresnelPower = input.ReadValueF32(endian);
            this._WetnessControlMetalness = input.ReadValueF32(endian);
            this._RootMaterialPath = ReadString(input, endian); // "template\\defaultTemplate_wet.bgsm"
            this._AnisoLighting = input.ReadValueB8();
            this._EmitEnabled = input.ReadValueB8();
            this._EmittanceColor = this._EmitEnabled == true ? Color.Read(input, endian).ToUInt32() : 0;
            this._EmittanceMult = input.ReadValueF32(endian);
            this._ModelSpaceNormals = input.ReadValueB8();
            this._ExternalEmittance = input.ReadValueB8();
            this._BackLighting = input.ReadValueB8();
            this._ReceiveShadows = input.ReadValueB8();
            this._HideSecret = input.ReadValueB8();
            this._CastShadows = input.ReadValueB8();
            this._DissolveFade = input.ReadValueB8();
            this._AssumeShadowmask = input.ReadValueB8();
            this._Glowmap = input.ReadValueB8();
            this._EnvironmentMappingWindow = input.ReadValueB8();
            this._EnvironmentMappingEye = input.ReadValueB8();
            this._Hair = input.ReadValueB8();
            this._HairTintColor = Color.Read(input, endian).ToUInt32();
            this._Tree = input.ReadValueB8();
            this._Facegen = input.ReadValueB8();
            this._SkinTint = input.ReadValueB8();
            this._Tessellate = input.ReadValueB8();
            this._DisplacementTextureBias = input.ReadValueF32(endian);
            this._DisplacementTextureScale = input.ReadValueF32(endian);
            this._TessellationPNScale = input.ReadValueF32(endian);
            this._TessellationBaseFactor = input.ReadValueF32(endian);
            this._TessellationFadeDistance = input.ReadValueF32(endian);
            this._GrayscaleToPaletteScale = input.ReadValueF32(endian);
            this._SkewSpecularAlpha = version >= 1 && input.ReadValueB8();
        }
    }
}
