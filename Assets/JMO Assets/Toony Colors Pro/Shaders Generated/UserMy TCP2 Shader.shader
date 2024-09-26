// Toony Colors Pro+Mobile 2
// (c) 2014-2023 Jean Moreno

Shader "Toony Colors Pro 2/User/My TCP2 Shader"
{
	Properties
	{
		[Enum(Front, 2, Back, 1, Both, 0)] _Cull ("Render Face", Float) = 2.0
		[TCP2ToggleNoKeyword] _ZWrite ("Depth Write", Float) = 1.0
		[HideInInspector] _RenderingMode ("rendering mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("blending source", Float) = 1.0
		[HideInInspector] _DstBlend ("blending destination", Float) = 0.0
		[TCP2Separator]

		[TCP2HeaderHelp(Base)]
		_BaseColor ("Color", Color) = (1,1,1,1)
		[TCP2ColorNoAlpha] _HColor ("Highlight Color", Color) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] _SColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)
		[HideInInspector] __BeginGroup_ShadowHSV ("Shadow HSV", Float) = 0
		_Shadow_HSV_H ("Hue", Range(-180,180)) = 0
		_Shadow_HSV_S ("Saturation", Range(-1,1)) = 0
		_Shadow_HSV_V ("Value", Range(-1,1)) = 0
		[HideInInspector] __EndGroup ("Shadow HSV", Float) = 0
		[MainTexture] _BaseMap ("Albedo", 2D) = "white" {}
		[TCP2Separator]

		[TCP2Header(Ramp Shading)]
		
		_RampThreshold ("Threshold", Range(0.01,1)) = 0.5
		_RampSmoothing ("Smoothing", Range(0.001,1)) = 0.5
		[TCP2Separator]
		
		[TCP2HeaderHelp(Specular)]
		[Toggle(TCP2_SPECULAR)] _UseSpecular ("Enable Specular", Float) = 0
		[TCP2ColorNoAlpha] _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_SpecularSmoothness ("Smoothness", Float) = 0.2
		_AnisotropicSpread ("Anisotropic Spread", Range(0,2)) = 1
		_SpecularToonSize ("Toon Size", Range(0,1)) = 0.25
		_SpecularToonSmoothness ("Toon Smoothness", Range(0.001,0.5)) = 0.05
		[TCP2Separator]

		[TCP2HeaderHelp(Emission)]
		[TCP2ColorNoAlpha] [HDR] _Emission ("Emission Color", Color) = (0,0,0,1)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Rim Lighting)]
		[Toggle(TCP2_RIM_LIGHTING)] _UseRim ("Enable Rim Lighting", Float) = 0
		[TCP2ColorNoAlpha] _RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.5)
		_RimMinVert ("Rim Min", Range(0,2)) = 0.5
		_RimMaxVert ("Rim Max", Range(0,2)) = 1
		//Rim Direction
		_RimDirVert ("Rim Direction", Vector) = (0,0,1,1)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Subsurface Scattering)]
		[Toggle(TCP2_SUBSURFACE)] _UseSubsurface ("Enable Subsurface Scattering", Float) = 0
		_SubsurfaceDistortion ("Distortion", Range(0,2)) = 0.2
		_SubsurfacePower ("Power", Range(0.1,16)) = 3
		_SubsurfaceScale ("Scale", Float) = 1
		[TCP2ColorNoAlpha] _SubsurfaceColor ("Color", Color) = (0.5,0.5,0.5,1)
		[TCP2ColorNoAlpha] _SubsurfaceAmbientColor ("Ambient Color", Color) = (0.5,0.5,0.5,1)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Vertex Displacement)]
		[Toggle(TCP2_VERTEX_DISPLACEMENT)] _UseVertexDisplacement ("Enable Vertex Displacement", Float) = 0
		_DisplacementTex ("Displacement Texture", 2D) = "black" {}
		 _DisplacementStrength ("Displacement Strength", Range(-1,1)) = 0.01
		[TCP2Separator]
		
		[TCP2HeaderHelp(Normal Mapping)]
		[Toggle(_NORMALMAP)] _UseNormalMap ("Enable Normal Mapping", Float) = 0
		[NoScaleOffset] _BumpMap ("Normal Map", 2D) = "bump" {}
		_BumpScale ("Scale", Float) = 1
		[NoScaleOffset] _ParallaxMap ("Height Map", 2D) = "black" {}
		_Parallax ("Height", Range(0,0.08)) = 0.02
		[TCP2Separator]
		
		[TCP2HeaderHelp(Texture Blending)]
		[NoScaleOffset] _BlendingSource ("Blending Source", 2D) = "black" {}
		_BlendingContrast ("Blending Contrast", Vector) = (1,1,1,0)
		[TCP2Separator]
		[HideInInspector] __BeginGroup_ShadowHSV ("Shadow Line", Float) = 0
		_ShadowLineThreshold ("Threshold", Range(0,1)) = 0.5
		_ShadowLineSmoothing ("Smoothing", Range(0.001,0.1)) = 0.015
		_ShadowLineStrength ("Strength", Float) = 1
		_ShadowLineColor ("Color (RGB) Opacity (A)", Color) = (0,0,0,1)
		[HideInInspector] __EndGroup ("Shadow Line", Float) = 0
		
		[Toggle(TCP2_TEXTURED_THRESHOLD)] _UseTexturedThreshold ("Enable Textured Threshold", Float) = 0
		_StylizedThreshold ("Stylized Threshold", 2D) = "gray" {}
		[TCP2Separator]
		
		[TCP2ColorNoAlpha] _DiffuseTint ("Diffuse Tint", Color) = (1,0.5,0,1)
		[NoScaleOffset] _DiffuseTintMask ("Diffuse Tint Mask", 2D) = "white" {}
		[TCP2Separator]
		
		[TCP2HeaderHelp(Sketch)]
		[Toggle(TCP2_SKETCH)] _UseSketch ("Enable Sketch Effect", Float) = 0
		_SketchTexture ("Sketch Texture", 2D) = "black" {}
		_SketchTexture_OffsetSpeed ("Sketch Texture UV Offset Speed", Float) = 120
		_SketchMin ("Sketch Min", Range(0,1)) = 0
		_SketchMax ("Sketch Max", Range(0,1)) = 1
		[TCP2Separator]
		
		[TCP2HeaderHelp(Wind)]
		[Toggle(TCP2_WIND)] _UseWind ("Enable Wind", Float) = 0
		_WindDirection ("Direction", Vector) = (1,0,0,0)
		_WindStrength ("Strength", Range(0,0.2)) = 0.025
		_WindSpeed ("Speed", Range(0,10)) = 2.5
		
		_SubsurfaceScreenSpaceInfluence ("Screen-space Influence", Range(0,10)) = 0.5

		[ToggleOff(_RECEIVE_SHADOWS_OFF)] _ReceiveShadowsOff ("Receive Shadows", Float) = 1

		// Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType"="Opaque"
		}

		HLSLINCLUDE
		#define fixed half
		#define fixed2 half2
		#define fixed3 half3
		#define fixed4 half4

		#if UNITY_VERSION >= 202020
			#define URP_10_OR_NEWER
		#endif
		#if UNITY_VERSION >= 202120
			#define URP_12_OR_NEWER
		#endif
		#if UNITY_VERSION >= 202220
			#define URP_14_OR_NEWER
		#endif

		// Texture/Sampler abstraction
		#define TCP2_TEX2D_WITH_SAMPLER(tex)						TEXTURE2D(tex); SAMPLER(sampler##tex)
		#define TCP2_TEX2D_NO_SAMPLER(tex)							TEXTURE2D(tex)
		#define TCP2_TEX2D_SAMPLE(tex, samplertex, coord)			SAMPLE_TEXTURE2D(tex, sampler##samplertex, coord)
		#define TCP2_TEX2D_SAMPLE_LOD(tex, samplertex, coord, lod)	SAMPLE_TEXTURE2D_LOD(tex, sampler##samplertex, coord, lod)

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
		#if defined(URP_12_OR_NEWER)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
		#endif

		// Uniforms

		// Shader Properties
		TCP2_TEX2D_WITH_SAMPLER(_DisplacementTex);
		TCP2_TEX2D_WITH_SAMPLER(_ParallaxMap);
		TCP2_TEX2D_WITH_SAMPLER(_BumpMap);
		TCP2_TEX2D_WITH_SAMPLER(_BlendingSource);
		TCP2_TEX2D_WITH_SAMPLER(_BaseMap);
		TCP2_TEX2D_WITH_SAMPLER(_StylizedThreshold);
		TCP2_TEX2D_WITH_SAMPLER(_DiffuseTintMask);
		TCP2_TEX2D_WITH_SAMPLER(_SketchTexture);

		CBUFFER_START(UnityPerMaterial)
			
			// Shader Properties
			float4 _DisplacementTex_ST;
			float _DisplacementStrength;
			float _WindSpeed;
			float4 _WindDirection;
			float _WindStrength;
			float4 _RimDirVert;
			float _RimMinVert;
			float _RimMaxVert;
			float _Parallax;
			float _BumpScale;
			float4 _BlendingContrast;
			float4 _BaseMap_ST;
			fixed4 _BaseColor;
			half4 _Emission;
			float4 _StylizedThreshold_ST;
			float _RampThreshold;
			float _RampSmoothing;
			fixed4 _DiffuseTint;
			float _ShadowLineThreshold;
			float _ShadowLineStrength;
			float _ShadowLineSmoothing;
			fixed4 _ShadowLineColor;
			fixed4 _RimColor;
			float _AnisotropicSpread;
			float _SpecularSmoothness;
			float _SpecularToonSize;
			float _SpecularToonSmoothness;
			fixed4 _SpecularColor;
			float _SubsurfaceDistortion;
			float _SubsurfacePower;
			float _SubsurfaceScale;
			fixed4 _SubsurfaceColor;
			fixed4 _SubsurfaceAmbientColor;
			float _SubsurfaceScreenSpaceInfluence;
			float _Shadow_HSV_H;
			float _Shadow_HSV_S;
			float _Shadow_HSV_V;
			float4 _SketchTexture_ST;
			half _SketchTexture_OffsetSpeed;
			float _SketchMin;
			float _SketchMax;
			fixed4 _SColor;
			fixed4 _HColor;
		CBUFFER_END

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#endif

		#if defined(UNITY_INSTANCING_ENABLED) || defined(UNITY_DOTS_INSTANCING_ENABLED)
			#define unity_ObjectToWorld UNITY_MATRIX_M
			#define unity_WorldToObject UNITY_MATRIX_I_M
		#endif

		//--------------------------------
		// HSV HELPERS
		// source: http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
		
		float3 rgb2hsv(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
			float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
		
			float d = q.x - min(q.w, q.y);
			float e = 1.0e-10;
			return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}
		
		float3 hsv2rgb(float3 c)
		{
			c.g = max(c.g, 0.0); //make sure that saturation value is positive
			float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
		}
		
		float3 ApplyHSV_3(float3 color, float h, float s, float v)
		{
			float3 hsv = rgb2hsv(color.rgb);
			hsv += float3(h/360,s,v);
			return hsv2rgb(hsv);
		}
		float3 ApplyHSV_3(float color, float h, float s, float v) { return ApplyHSV_3(color.xxx, h, s ,v); }
		
		float4 ApplyHSV_4(float4 color, float h, float s, float v)
		{
			float3 hsv = rgb2hsv(color.rgb);
			hsv += float3(h/360,s,v);
			return float4(hsv2rgb(hsv), color.a);
		}
		float4 ApplyHSV_4(float color, float h, float s, float v) { return ApplyHSV_4(color.xxxx, h, s, v); }
		
		// Hash without sin and uniform across platforms
		// Adapted from: https://www.shadertoy.com/view/4djSRW (c) 2014 - Dave Hoskins - CC BY-SA 4.0 License
		float2 hash22(float2 p)
		{
			float3 p3 = frac(p.xyx * float3(443.897, 441.423, 437.195));
			p3 += dot(p3, p3.yzx + 19.19);
			return frac((p3.xx+p3.yz)*p3.zy);
		}
		
		static const float DITHER_THRESHOLD_8x8[64] =
		{
			0.2627,0.7725,0.3843,0.9529,0.2941,0.8196,0.4118,1.0000,
			0.5020,0.0392,0.6235,0.1412,0.5333,0.0510,0.6549,0.1725,
			0.3216,0.8627,0.2039,0.6824,0.3529,0.9098,0.2314,0.7294,
			0.5647,0.0824,0.4431,0.0078,0.5922,0.1137,0.4745,0.0235,
			0.2784,0.7961,0.4000,0.9765,0.2471,0.7529,0.3686,0.9333,
			0.5176,0.0471,0.6392,0.1569,0.4902,0.0314,0.6078,0.1294,
			0.3373,0.8863,0.2196,0.7059,0.3098,0.8431,0.1882,0.6706,
			0.5804,0.0980,0.4588,0.0157,0.5490,0.0667,0.4275,0.0001
		};
		float Dither8x8(float2 positionCS)
		{
			uint index = (uint(positionCS.x) % 8) * 8 + uint(positionCS.y) % 8;
			return DITHER_THRESHOLD_8x8[index];
		}
		
		static const float DITHER_THRESHOLDS_4x4[16] =
		{
			0.0588, 0.5294, 0.1765, 0.6471,
			0.7647, 0.2941, 0.8823, 0.4118,
			0.2353, 0.7059, 0.1176, 0.5882,
			0.9412, 0.4706, 0.8235, 0.3529
		};
		float Dither4x4(float2 positionCS)
		{
			uint index = (uint(positionCS.x) % 4) * 4 + uint(positionCS.y) % 4;
			return DITHER_THRESHOLDS_4x4[index];
		}
		
		//Specular help functions (from UnityStandardBRDF.cginc)
		inline float3 SpecSafeNormalize(float3 inVec)
		{
			half dp3 = max(0.001f, dot(inVec, inVec));
			return inVec * rsqrt(dp3);
		}
		
		//Subsurface Scattering directional light screen-space influence
		half subsurfaceScreenInfluence(half3 lightDir, half3 viewDir, half size)
		{
			half3 delta = lightDir + viewDir;
			half dist = length(delta);
			half spot = 1.0 - smoothstep(0.0, size, dist);
			return spot * spot;
		}
		
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ParallaxMapping.hlsl"
		
		// Calculates UV offset for parallax bump mapping
		inline float2 TCP2_ParallaxOffset( half h, half height, half3 viewDir )
		{
			h = h * height - height/2.0;
			float3 v = normalize(viewDir);
			v.z += 0.42;
			return h * (v.xy / v.z);
		}
		
		// Cubic pulse function
		// Adapted from: http://www.iquilezles.org/www/articles/functions/functions.htm (c) 2017 - Inigo Quilez - MIT License
		float linearPulse(float c, float w, float x)
		{
			x = abs(x - c);
			if (x > w)
			{
				return 0;
			}
			x /= w;
			return 1 - x;
		}
		
		// Built-in renderer (CG) to SRP (HLSL) bindings
		#define UnityObjectToClipPos TransformObjectToHClip
		#define _WorldSpaceLightPos0 _MainLightPosition
		
		#if defined(_DBUFFER)
			// Derived from 'ApplyDecal' in URP's DBuffer.hlsl, directly fetch the decal data so that we can blend it accordingly
			DecalSurfaceData GetDecals(float4 positionCS)
			{
				FETCH_DBUFFER(DBuffer, _DBufferTexture, int2(positionCS.xy));

				DecalSurfaceData decalSurfaceData = (DecalSurfaceData)0;
				DECODE_FROM_DBUFFER(DBuffer, decalSurfaceData);

				#if !defined(_DBUFFER_MRT3)
					decalSurfaceData.MAOSAlpha = 0;
				#endif

				return decalSurfaceData;
			}
		#endif

		ENDHLSL

		Pass
		{
			Name "Main"
			Tags
			{
				"LightMode"="UniversalForward"
			}
		Blend [_SrcBlend] [_DstBlend]
		Cull [_Cull]
		ZWrite [_ZWrite]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard SRP library
			// All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 3.0

			// -------------------------------------
			// Material keywords
			#pragma shader_feature_local _ _RECEIVE_SHADOWS_OFF

			// -------------------------------------
			// Universal Render Pipeline keywords
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile_fragment _ _SHADOWS_SOFT
			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
			#pragma multi_compile_fragment _ _LIGHT_COOKIES
			#pragma multi_compile_fragment _ _LIGHT_LAYERS
			#pragma multi_compile _ _FORWARD_PLUS
			#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS
			#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION

			// -------------------------------------
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DYNAMICLIGHTMAP_ON
			#pragma multi_compile_fog
			#pragma multi_compile _ LOD_FADE_CROSSFADE

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex Vertex
			#pragma fragment Fragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_fragment TCP2_SPECULAR
			#pragma shader_feature_local_vertex TCP2_VERTEX_DISPLACEMENT
			#pragma shader_feature_local TCP2_RIM_LIGHTING
		#pragma shader_feature_local _ _ALPHAPREMULTIPLY_ON
			#pragma shader_feature_local_fragment TCP2_SUBSURFACE
			#pragma shader_feature_local _NORMALMAP
			#pragma shader_feature_local_fragment TCP2_SKETCH
			#pragma shader_feature_local_fragment TCP2_TEXTURED_THRESHOLD
			#pragma shader_feature_local_vertex TCP2_WIND

			// vertex input
			struct Attributes
			{
				float4 vertex       : POSITION;
				float3 normal       : NORMAL;
				float4 tangent      : TANGENT;
				#if defined(LIGHTMAP_ON)
				float2 texcoord1    : TEXCOORD1;
				#endif
				#if defined(DYNAMICLIGHTMAP_ON)
				float2 texcoord2    : TEXCOORD2;
				#endif
				half4 vertexColor   : COLOR;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			// vertex output / fragment input
			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float3 normal         : NORMAL;
				float4 worldPosAndFog : TEXCOORD0;
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord    : TEXCOORD1; // compute shadow coord per-vertex for the main light
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				half3 vertexLights : TEXCOORD2;
			#endif
			#if defined(LIGHTMAP_ON) && defined(DYNAMICLIGHTMAP_ON)
				float4 lightmapUV  : TEXCOORD3;
				#define staticLightmapUV lightmapUV.xy
				#define dynamicLightmapUV lightmapUV.zw
			#elif defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
				float2 lightmapUV  : TEXCOORD3;
				#define staticLightmapUV lightmapUV.xy
				#define dynamicLightmapUV lightmapUV.xy
			#endif
				float4 screenPosition : TEXCOORD4;
				float4 pack1 : TEXCOORD5; /* pack1.xyz = tangent  pack1.w = fogFactor */
				float4 pack2 : TEXCOORD6; /* pack2.xyz = bitangent  pack2.w = rim */
				float2 pack3 : TEXCOORD7; /* pack3.xy = texcoord0 */
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if USE_FORWARD_PLUS
				// Fake InputData struct needed for Forward+ macro
				struct InputDataForwardPlusDummy
				{
					float3  positionWS;
					float2  normalizedScreenSpaceUV;
				};
			#endif

			Varyings Vertex(Attributes input)
			{
				Varyings output = (Varyings)0;

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				// Texture Coordinates
				output.pack3.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;
				// Shader Properties Sampling
				float3 __vertexDisplacement = ( input.normal.xyz * TCP2_TEX2D_SAMPLE_LOD(_DisplacementTex, _DisplacementTex, output.pack3.xy * _DisplacementTex_ST.xy + _DisplacementTex_ST.zw, 0).rgb * _DisplacementStrength );
				float __windTimeOffset = ( input.vertexColor.g );
				float __windSpeed = ( _WindSpeed );
				float __windFrequency = ( 1.0 );
				float4 __windSineScale2 = ( float4(2.3,1.7,1.4,1.2) );
				float __windSineStrength2 = ( .6 );
				float3 __windDirection = ( _WindDirection.xyz );
				float3 __windMask = ( input.vertexColor.rrr );
				float __windStrength = ( _WindStrength );
				float3 __rimDirVert = ( _RimDirVert.xyz );
				float __rimMinVert = ( _RimMinVert );
				float __rimMaxVert = ( _RimMaxVert );

				#if defined(LIGHTMAP_ON)
					output.staticLightmapUV = input.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif
				#if defined(DYNAMICLIGHTMAP_ON)
					output.dynamicLightmapUV = input.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif

				#if defined(TCP2_VERTEX_DISPLACEMENT)
				input.vertex.xyz += __vertexDisplacement;
				#endif
				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				#if defined(TCP2_WIND)
				// Wind Animation
				float windTimeOffset = __windTimeOffset;
				float windSpeed = __windSpeed;
				float3 windFrequency = worldPos.xyz * __windFrequency;
				float windPhase = (_Time.y + windTimeOffset) * windSpeed;
				float3 windFactor = sin(windPhase + windFrequency);
				float4 windSin2scale = __windSineScale2;
				float windSin2strength = __windSineStrength2;
				windFactor += sin(windPhase.xxx * windSin2scale.www + windFrequency * windSin2scale.xyz) * windSin2strength;
				float3 windDir = normalize(__windDirection);
				float3 windMask = __windMask;
				float windStrength = __windStrength;
				worldPos.xyz += windDir * windFactor * windMask * windStrength;
				#endif
				input.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				output.shadowCoord = GetShadowCoord(vertexInput);
			#endif
				float4 clipPos = vertexInput.positionCS;

				float4 screenPos = ComputeScreenPos(clipPos);
				output.screenPosition.xyzw = screenPos;

				VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(input.normal, input.tangent);
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				// Vertex lighting
				output.vertexLights = VertexLighting(vertexInput.positionWS, vertexNormalInput.normalWS);
			#endif

				// world position
				output.worldPosAndFog = float4(vertexInput.positionWS.xyz, 0);

				// Computes fog factor per-vertex
				output.worldPosAndFog.w = ComputeFogFactor(vertexInput.positionCS.z);

				// normal
				output.normal = normalize(vertexNormalInput.normalWS);

				// tangent
				output.pack1.xyz = vertexNormalInput.tangentWS;
				output.pack2.xyz = vertexNormalInput.bitangentWS;

				// clip position
				output.positionCS = vertexInput.positionCS;

				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - vertexInput.positionWS);

				#if defined(TCP2_RIM_LIGHTING)
				half3 rViewDir = viewDirWS;
				half3 rimDir = __rimDirVert;
				rViewDir = normalize(UNITY_MATRIX_V[0].xyz * rimDir.x + UNITY_MATRIX_V[1].xyz * rimDir.y + UNITY_MATRIX_V[2].xyz * rimDir.z);
				half rim = 1.0f - saturate(dot(rViewDir, input.normal.xyz));
				rim = smoothstep(__rimMinVert, __rimMaxVert, rim);
				output.pack2.w = rim;
				#endif

				return output;
			}

			half4 Fragment(Varyings input
			#ifdef _WRITE_RENDERING_LAYERS
				, out float4 outRenderingLayers : SV_Target1
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				#if defined(LOD_FADE_CROSSFADE)
					const float dither = Dither4x4(input.positionCS.xy);
					const float ditherThreshold = unity_LODFade.x - CopySign(dither, unity_LODFade.x);
					clip(ditherThreshold);
				#endif

				float3 positionWS = input.worldPosAndFog.xyz;
				float3 normalWS = normalize(input.normal);
				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - positionWS);
				half3 tangentWS = input.pack1.xyz;
				half3 bitangentWS = input.pack2.xyz;
				#if defined(_NORMALMAP)
				half3x3 tangentToWorldMatrix = half3x3(tangentWS.xyz, bitangentWS.xyz, normalWS.xyz);
				//Parallax Offset
				float __parallaxHeightMap = ( TCP2_TEX2D_SAMPLE(_ParallaxMap, _ParallaxMap, input.pack3.xy).a );
				float __parallaxHeight = ( _Parallax );
				half height = __parallaxHeightMap;
				half3 viewDirTS = GetViewDirectionTangentSpace(half4(tangentWS, 0), normalWS, viewDirWS);
				float2 offset = TCP2_ParallaxOffset(height, __parallaxHeight, viewDirTS);
				input.pack3.xy += offset;
				#endif
				float4 __normalMap = ( TCP2_TEX2D_SAMPLE(_BumpMap, _BumpMap, input.pack3.xy).rgba );
				float __bumpScale = ( _BumpScale );
				#if defined(_NORMALMAP)
				half4 normalMap = __normalMap;
				half3 normalTS = UnpackNormalScale(normalMap, __bumpScale);
					#if defined(_NORMALMAP)
				normalWS = normalize( mul(normalTS, tangentToWorldMatrix) );
					#endif
				#endif

				//Screen Space UV
				float2 screenUV = input.screenPosition.xyzw.xy / input.screenPosition.xyzw.w;
				
				// Shader Properties Sampling
				
				float4 __blendingSource = ( TCP2_TEX2D_SAMPLE(_BlendingSource, _BlendingSource, input.pack3.xy).rgba );
				float4 __blendingContrast = ( _BlendingContrast.xyzw );
				float4 __albedo = ( TCP2_TEX2D_SAMPLE(_BaseMap, _BaseMap, input.pack3.xy).rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __ambientIntensity = ( 1.0 );
				float3 __emission = ( _Emission.rgb );
				float __stylizedThreshold = ( TCP2_TEX2D_SAMPLE(_StylizedThreshold, _StylizedThreshold, input.pack3.xy * _StylizedThreshold_ST.xy + _StylizedThreshold_ST.zw).a );
				float __stylizedThresholdScale = ( 1.0 );
				float __rampThreshold = ( _RampThreshold );
				float __rampSmoothing = ( _RampSmoothing );
				float3 __diffuseTint = ( _DiffuseTint.rgb );
				float3 __diffuseTintMask = ( TCP2_TEX2D_SAMPLE(_DiffuseTintMask, _DiffuseTintMask, input.pack3.xy).rgb );
				float __shadowLineThreshold = ( _ShadowLineThreshold );
				float __shadowLineStrength = ( _ShadowLineStrength );
				float __shadowLineSmoothing = ( _ShadowLineSmoothing );
				float4 __shadowLineColor = ( _ShadowLineColor.rgba );
				float3 __rimColor = ( _RimColor.rgb );
				float __rimStrength = ( 1.0 );
				float __anisotropicSpread = ( _AnisotropicSpread );
				float __specularSmoothness = ( _SpecularSmoothness );
				float __specularToonSize = ( _SpecularToonSize );
				float __specularToonSmoothness = ( _SpecularToonSmoothness );
				float3 __specularColor = ( _SpecularColor.rgb );
				float __subsurfaceDistortion = ( _SubsurfaceDistortion );
				float __subsurfacePower = ( _SubsurfacePower );
				float __subsurfaceScale = ( _SubsurfaceScale );
				float3 __subsurfaceColor = ( _SubsurfaceColor.rgb );
				float3 __subsurfaceAmbientColor = ( _SubsurfaceAmbientColor.rgb );
				float __subsurfaceThickness = ( 1.0 );
				float __subsurfaceScreenSpaceInfluence = ( _SubsurfaceScreenSpaceInfluence );
				float __shadowHue = ( _Shadow_HSV_H );
				float __shadowSaturation = ( _Shadow_HSV_S );
				float __shadowValue = ( _Shadow_HSV_V );
				float __shadowHsvMask = ( __albedo.a );
				float3 __sketchTexture = ( TCP2_TEX2D_SAMPLE(_SketchTexture, _SketchTexture, screenUV * _ScreenParams.zw * _SketchTexture_ST.xy + _SketchTexture_ST.zw + hash22(floor(_Time.xx * _SketchTexture_OffsetSpeed.xx) / _SketchTexture_OffsetSpeed.xx)).aaa );
				float __sketchAntialiasing = ( 20.0 );
				float __sketchThresholdScale = ( 1.0 );
				float __sketchMin = ( _SketchMin );
				float __sketchMax = ( _SketchMax );
				float3 __shadowColor = ( _SColor.rgb );
				float3 __highlightColor = ( _HColor.rgb );
				float3 __sketchColor = ( float3(0,0,0) );

				// Texture Blending: initialize
				fixed4 blendingSource = __blendingSource;
				blendingSource.rgba = saturate(normalize(blendingSource.rgba) * dot(__blendingContrast, blendingSource.rgba));
				#if defined(_NORMALMAP)
				normalMap = __normalMap;
				
				// Texture Blending: normal maps
				normalTS = UnpackNormalScale(normalMap, __bumpScale);
					#if defined(_NORMALMAP)
				normalWS = normalize( mul(normalTS, tangentToWorldMatrix) );
					#endif
				#endif

				half ndv = abs(dot(viewDirWS, normalWS));
				half ndvRaw = ndv;

				// main texture
				half3 albedo = __albedo.rgb;
				half alpha = __alpha;

				// URP Decals
				#if defined(_DBUFFER)
					#if defined(_DBUFFER_MRT2) || defined(_DBUFFER_MRT3)
						#define HAS_DECAL_NORMALS
					#endif
					#if defined(_DBUFFER_MRT3)
						#define HAS_DECAL_MAOS
					#endif

					DecalSurfaceData decals = GetDecals(input.positionCS);
					albedo.rgb = albedo.rgb * decals.baseColor.a + decals.baseColor.rgb;
					#if defined(HAS_DECAL_NORMALS)
						// Always test the normal as we can have decompression artifact
						if (decals.normalWS.w < 1.0)
						{
							normalWS.xyz = normalize(normalWS.xyz * decals.normalWS.w + decals.normalWS.xyz);
						}
					#endif
				#endif

				half3 emission = half3(0,0,0);
				half4 albedoAlpha = half4(albedo, alpha);
				
				// Texture Blending: sample
				albedo = albedoAlpha.rgb;
				alpha = albedoAlpha.a;
				
				albedo *= __mainColor.rgb;

				// main light: direction, color, distanceAttenuation, shadowAttenuation
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord = input.shadowCoord;
			#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
				float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
			#else
				float4 shadowCoord = float4(0, 0, 0, 0);
			#endif

			#if defined(URP_10_OR_NEWER)
				#if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
					half4 shadowMask = SAMPLE_SHADOWMASK(input.staticLightmapUV);
				#elif !defined (LIGHTMAP_ON)
					half4 shadowMask = unity_ProbesOcclusion;
				#else
					half4 shadowMask = half4(1, 1, 1, 1);
				#endif

				#if defined(URP_14_OR_NEWER)
					uint meshRenderingLayers = GetMeshRenderingLayer();
				#elif defined(URP_12_OR_NEWER)
					uint meshRenderingLayers = GetMeshRenderingLightLayer();
				#endif

				Light mainLight = GetMainLight(shadowCoord, positionWS, shadowMask);
			#else
				Light mainLight = GetMainLight(shadowCoord);
			#endif

			#if defined(_SCREEN_SPACE_OCCLUSION) || defined(USE_FORWARD_PLUS)
				float2 normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
			#endif
			#if defined(_SCREEN_SPACE_OCCLUSION)
				AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(normalizedScreenSpaceUV);
				mainLight.color *= aoFactor.directAmbientOcclusion;
			#endif

				// ambient or lightmap
			#if defined(LIGHTMAP_ON) && defined(DYNAMICLIGHTMAP_ON)
				// Static & Dynamic Lightmap
				half3 bakedGI = SampleLightmap(input.staticLightmapUV, input.dynamicLightmapUV, normalWS);
				MixRealtimeAndBakedGI(mainLight, normalWS, bakedGI, half4(0, 0, 0, 0));
			#elif defined(LIGHTMAP_ON)
				// Static Lightmap
				half3 bakedGI = SampleLightmap(input.staticLightmapUV, 0, normalWS);
				MixRealtimeAndBakedGI(mainLight, normalWS, bakedGI, half4(0, 0, 0, 0));
			#elif defined(DYNAMICLIGHTMAP_ON)
				// Dynamic Lightmap
				half3 bakedGI = SampleLightmap(0, input.dynamicLightmapUV, normalWS);
				MixRealtimeAndBakedGI(mainLight, normalWS, bakedGI, half4(0, 0, 0, 0));
			#else
				// Samples SH fully per-pixel. SampleSHVertex and SampleSHPixel functions
				// are also defined in case you want to sample some terms per-vertex.
				half3 bakedGI = SampleSH(normalWS);
			#endif
				half occlusion = 1;

			#if defined(_SCREEN_SPACE_OCCLUSION)
				occlusion = min(occlusion, aoFactor.indirectAmbientOcclusion);
			#endif

				half3 indirectDiffuse = bakedGI;
				indirectDiffuse *= occlusion * albedo * __ambientIntensity;
				emission += __emission;

				#if defined(_LIGHT_LAYERS)
					half3 lightDir = half3(0, 1, 0);
					half3 lightColor = half3(0, 0, 0);
					if (IsMatchingLightLayer(mainLight.layerMask, meshRenderingLayers))
					{
						lightDir = mainLight.direction;
						lightColor = mainLight.color.rgb;
					}
				#else
					half3 lightDir = mainLight.direction;
					half3 lightColor = mainLight.color.rgb;
				#endif

				half atten = mainLight.shadowAttenuation * mainLight.distanceAttenuation;

				half ndl = dot(normalWS, lightDir);
				#if defined(TCP2_TEXTURED_THRESHOLD)
				float stylizedThreshold = __stylizedThreshold;
				stylizedThreshold -= 0.5;
				stylizedThreshold *= __stylizedThresholdScale;
				ndl += stylizedThreshold;
				#endif
				half3 ramp;
				
				half rampThreshold = __rampThreshold;
				half rampSmooth = __rampSmoothing * 0.5;
				ndl = saturate(ndl);
				ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

				// apply attenuation
				ramp *= atten;

				// Diffuse Tint
				half3 diffuseTint = saturate(__diffuseTint + ndl);
				ramp = lerp(ramp, ramp * diffuseTint, __diffuseTintMask);
				
				//Shadow Line
				float ndlAtten = ndl * atten;
				float shadowLineThreshold = __shadowLineThreshold;
				float shadowLineStrength = __shadowLineStrength;
				float shadowLineFw = fwidth(ndlAtten);
				float shadowLineSmoothing = __shadowLineSmoothing * shadowLineFw * 10;
				float shadowLine = min(linearPulse(ndlAtten, shadowLineSmoothing, shadowLineThreshold) * shadowLineStrength, 1.0);
				half4 shadowLineColor = __shadowLineColor;
				ramp = lerp(ramp.rgb, shadowLineColor.rgb, shadowLine * shadowLineColor.a);
				half3 color = half3(0,0,0);
				// Rim Lighting
				#if defined(TCP2_RIM_LIGHTING)
				half rim = input.pack2.w;
				rim = ( rim );
				half3 rimColor = __rimColor;
				half rimStrength = __rimStrength;
				//Rim light mask
				emission.rgb += ndl * atten * rim * rimColor * rimStrength;
				#endif
				half3 accumulatedRamp = ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
				half3 accumulatedColors = ramp * lightColor.rgb;

				half3 halfDir = SpecSafeNormalize(float3(lightDir) + float3(viewDirWS));
				
				#if defined(TCP2_SPECULAR)
				//Anisotropic Specular
				float ndh = max(0, dot (normalWS, halfDir));
				half3 binorm = bitangentWS.xyz;
				float aX = dot(halfDir, tangentWS) / __anisotropicSpread;
				float aY = dot(halfDir, binorm) / __specularSmoothness;
				float specAniso = sqrt(max(0.0, ndl / ndvRaw)) * exp(-2.0 * (aX * aX + aY * aY) / (1.0 + ndh));
				float spec = smoothstep(__specularToonSize + __specularToonSmoothness, __specularToonSize - __specularToonSmoothness,1 - (specAniso / (1+__specularToonSmoothness)));
				spec = saturate(spec);
				spec *= atten;
				
				//Apply specular
				emission.rgb += spec * lightColor.rgb * __specularColor;
				#endif
				
				//Subsurface Scattering for Main Light
				#if defined(TCP2_SUBSURFACE)
				half3 ssLight = lightDir + normalWS * __subsurfaceDistortion;
				half ssDot = pow(saturate(dot(viewDirWS, -ssLight)), __subsurfacePower) * __subsurfaceScale;
				half3 ssColor = ((ssDot * __subsurfaceColor) + __subsurfaceAmbientColor) * __subsurfaceThickness;
				ssColor *= subsurfaceScreenInfluence(lightDir, viewDirWS, __subsurfaceScreenSpaceInfluence);
				color.rgb *= albedo * ssColor;
				#endif

				// Additional lights loop
			#ifdef _ADDITIONAL_LIGHTS
				uint pixelLightCount = GetAdditionalLightsCount();

				#if USE_FORWARD_PLUS
					// Additional directional lights in Forward+
					for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
					{
						FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK

						Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);

						#if defined(_LIGHT_LAYERS)
							if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
						#endif
						{
							half atten = light.shadowAttenuation * light.distanceAttenuation;

							#if defined(_SCREEN_SPACE_OCCLUSION)
								light.color *= aoFactor.directAmbientOcclusion;
							#endif

							#if defined(_LIGHT_LAYERS)
								half3 lightDir = half3(0, 1, 0);
								half3 lightColor = half3(0, 0, 0);
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								{
									lightColor = light.color.rgb;
									lightDir = light.direction;
								}
							#else
								half3 lightColor = light.color.rgb;
								half3 lightDir = light.direction;
							#endif

							half ndl = dot(normalWS, lightDir);
							#if defined(TCP2_TEXTURED_THRESHOLD)
							float stylizedThreshold = __stylizedThreshold;
							stylizedThreshold -= 0.5;
							stylizedThreshold *= __stylizedThresholdScale;
							ndl += stylizedThreshold;
							#endif
							half3 ramp;
							
							ndl = saturate(ndl);
							ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

							// apply attenuation (shadowmaps & point/spot lights attenuation)
							ramp *= atten;

							// Diffuse Tint
							half3 diffuseTint = saturate(__diffuseTint + ndl);
							ramp = lerp(ramp, ramp * diffuseTint, __diffuseTintMask);
							
							//Shadow Line
							float ndlAtten = ndl * atten;
							float shadowLineThreshold = __shadowLineThreshold;
							float shadowLineStrength = __shadowLineStrength;
							float shadowLineFw = fwidth(ndlAtten);
							float shadowLineSmoothing = __shadowLineSmoothing * shadowLineFw * 10;
							float shadowLine = min(linearPulse(ndlAtten, shadowLineSmoothing, shadowLineThreshold) * shadowLineStrength, 1.0);
							half4 shadowLineColor = __shadowLineColor;
							ramp = lerp(ramp.rgb, shadowLineColor.rgb, shadowLine * shadowLineColor.a);
							accumulatedRamp += ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
							accumulatedColors += ramp * lightColor.rgb;

							half3 halfDir = SpecSafeNormalize(float3(lightDir) + float3(viewDirWS));
							
							#if defined(TCP2_SPECULAR)
							//Anisotropic Specular
							float ndh = max(0, dot (normalWS, halfDir));
							half3 binorm = bitangentWS.xyz;
							float aX = dot(halfDir, tangentWS) / __anisotropicSpread;
							float aY = dot(halfDir, binorm) / __specularSmoothness;
							float specAniso = sqrt(max(0.0, ndl / ndvRaw)) * exp(-2.0 * (aX * aX + aY * aY) / (1.0 + ndh));
							float spec = smoothstep(__specularToonSize + __specularToonSmoothness, __specularToonSize - __specularToonSmoothness,1 - (specAniso / (1+__specularToonSmoothness)));
							spec = saturate(spec);
							spec *= atten;
							
							//Apply specular
							emission.rgb += spec * lightColor.rgb * __specularColor;
							#endif
							
							//Subsurface Scattering for additional lights
							#if defined(TCP2_SUBSURFACE)
							half3 ssLight = lightDir + normalWS * __subsurfaceDistortion;
							half ssDot = pow(saturate(dot(viewDirWS, -ssLight)), __subsurfacePower) * __subsurfaceScale;
							half3 ssColor = ((ssDot * __subsurfaceColor) + __subsurfaceAmbientColor) * __subsurfaceThickness;
							ssColor *= atten;
							color.rgb *= albedo * ssColor;
							#endif
							#if defined(TCP2_RIM_LIGHTING)
							// Rim light mask
							half3 rimColor = __rimColor;
							half rimStrength = __rimStrength;
							emission.rgb += ndl * atten * rim * rimColor * rimStrength;
							#endif
						}
					}

					// Data with dummy struct used in Forward+ macro (LIGHT_LOOP_BEGIN)
					InputDataForwardPlusDummy inputData;
					inputData.normalizedScreenSpaceUV = normalizedScreenSpaceUV;
					inputData.positionWS = positionWS;
				#endif

				LIGHT_LOOP_BEGIN(pixelLightCount)
				{
					#if defined(URP_10_OR_NEWER)
						Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);
					#else
						Light light = GetAdditionalLight(lightIndex, positionWS);
					#endif
					half atten = light.shadowAttenuation * light.distanceAttenuation;
					#if defined(_SCREEN_SPACE_OCCLUSION)
						light.color *= aoFactor.directAmbientOcclusion;
					#endif

					#if defined(_LIGHT_LAYERS)
						half3 lightDir = half3(0, 1, 0);
						half3 lightColor = half3(0, 0, 0);
						if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
						{
							lightColor = light.color.rgb;
							lightDir = light.direction;
						}
					#else
						half3 lightColor = light.color.rgb;
						half3 lightDir = light.direction;
					#endif

					half ndl = dot(normalWS, lightDir);
					#if defined(TCP2_TEXTURED_THRESHOLD)
					float stylizedThreshold = __stylizedThreshold;
					stylizedThreshold -= 0.5;
					stylizedThreshold *= __stylizedThresholdScale;
					ndl += stylizedThreshold;
					#endif
					half3 ramp;
					
					ndl = saturate(ndl);
					ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

					// apply attenuation (shadowmaps & point/spot lights attenuation)
					ramp *= atten;

					// Diffuse Tint
					half3 diffuseTint = saturate(__diffuseTint + ndl);
					ramp = lerp(ramp, ramp * diffuseTint, __diffuseTintMask);
					
					//Shadow Line
					float ndlAtten = ndl * atten;
					float shadowLineThreshold = __shadowLineThreshold;
					float shadowLineStrength = __shadowLineStrength;
					float shadowLineFw = fwidth(ndlAtten);
					float shadowLineSmoothing = __shadowLineSmoothing * shadowLineFw * 10;
					float shadowLine = min(linearPulse(ndlAtten, shadowLineSmoothing, shadowLineThreshold) * shadowLineStrength, 1.0);
					half4 shadowLineColor = __shadowLineColor;
					ramp = lerp(ramp.rgb, shadowLineColor.rgb, shadowLine * shadowLineColor.a);
					accumulatedRamp += ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
					accumulatedColors += ramp * lightColor.rgb;

					half3 halfDir = SpecSafeNormalize(float3(lightDir) + float3(viewDirWS));
					
					#if defined(TCP2_SPECULAR)
					//Anisotropic Specular
					float ndh = max(0, dot (normalWS, halfDir));
					half3 binorm = bitangentWS.xyz;
					float aX = dot(halfDir, tangentWS) / __anisotropicSpread;
					float aY = dot(halfDir, binorm) / __specularSmoothness;
					float specAniso = sqrt(max(0.0, ndl / ndvRaw)) * exp(-2.0 * (aX * aX + aY * aY) / (1.0 + ndh));
					float spec = smoothstep(__specularToonSize + __specularToonSmoothness, __specularToonSize - __specularToonSmoothness,1 - (specAniso / (1+__specularToonSmoothness)));
					spec = saturate(spec);
					spec *= atten;
					
					//Apply specular
					emission.rgb += spec * lightColor.rgb * __specularColor;
					#endif
					
					//Subsurface Scattering for additional lights
					#if defined(TCP2_SUBSURFACE)
					half3 ssLight = lightDir + normalWS * __subsurfaceDistortion;
					half ssDot = pow(saturate(dot(viewDirWS, -ssLight)), __subsurfacePower) * __subsurfaceScale;
					half3 ssColor = ((ssDot * __subsurfaceColor) + __subsurfaceAmbientColor) * __subsurfaceThickness;
					ssColor *= atten;
					color.rgb *= albedo * ssColor;
					#endif
					#if defined(TCP2_RIM_LIGHTING)
					// Rim light mask
					half3 rimColor = __rimColor;
					half rimStrength = __rimStrength;
					emission.rgb += ndl * atten * rim * rimColor * rimStrength;
					#endif
				}
				LIGHT_LOOP_END
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				color += input.vertexLights * albedo;
			#endif

				accumulatedRamp = saturate(accumulatedRamp);
				
				//Shadow HSV
				float3 albedoShadowHSV = ApplyHSV_3(albedo, __shadowHue, __shadowSaturation, __shadowValue);
				albedo = lerp(albedoShadowHSV, albedo, accumulatedRamp + __shadowHsvMask * (1 - accumulatedRamp));
				
				// Sketch
				#if defined(TCP2_SKETCH)
				half3 sketch = __sketchTexture;
				half sketchThresholdWidth = __sketchAntialiasing * fwidth(ndl);
				sketch = smoothstep(sketch - sketchThresholdWidth, sketch, clamp(saturate(accumulatedRamp * __sketchThresholdScale), __sketchMin, __sketchMax));
				#endif
				half3 shadowColor = (1 - accumulatedRamp.rgb) * __shadowColor;
				accumulatedRamp = accumulatedColors.rgb * __highlightColor + shadowColor;
				color += albedo * accumulatedRamp;
				#if defined(TCP2_SKETCH)
				color.rgb *= lerp(__sketchColor, half3(1,1,1), sketch.rgb);
				#endif

				// apply ambient
				color += indirectDiffuse;

				// Premultiply blending
				#if defined(_ALPHAPREMULTIPLY_ON)
					color.rgb *= alpha;
				#endif

				color += emission;

				// Mix the pixel color with fogColor. You can optionally use MixFogColor to override the fogColor with a custom one.
				float fogFactor = input.worldPosAndFog.w;
				color = MixFog(color, fogFactor);

				#if defined(URP_14_OR_NEWER) && defined(_WRITE_RENDERING_LAYERS)
					outRenderingLayers = float4(EncodeMeshRenderingLayer(meshRenderingLayers), 0, 0, 0);
				#endif

				return half4(color, alpha);
			}
			ENDHLSL
		}

		// Depth & Shadow Caster Passes
		HLSLINCLUDE

		#if defined(SHADOW_CASTER_PASS) || defined(DEPTH_ONLY_PASS)

			#define fixed half
			#define fixed2 half2
			#define fixed3 half3
			#define fixed4 half4

			float3 _LightDirection;
			float3 _LightPosition;

			struct Attributes
			{
				float4 vertex   : POSITION;
				float3 normal   : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				half4 vertexColor : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float3 normal         : NORMAL;
			#if defined(DEPTH_NORMALS_PASS)
				float3 normalWS : TEXCOORD0;
			#endif
				float4 screenPosition : TEXCOORD1;
				float4 pack1 : TEXCOORD2; /* pack1.xyz = positionWS  pack1.w = rim */
				float2 pack2 : TEXCOORD3; /* pack2.xy = texcoord0 */
			#if defined(DEPTH_ONLY_PASS)
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			#endif
			};

			float4 GetShadowPositionHClip(Attributes input)
			{
				float3 positionWS = TransformObjectToWorld(input.vertex.xyz);
				float3 normalWS = TransformObjectToWorldNormal(input.normal);

				#if _CASTING_PUNCTUAL_LIGHT_SHADOW
					float3 lightDirectionWS = normalize(_LightPosition - positionWS);
				#else
					float3 lightDirectionWS = _LightDirection;
				#endif
				float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

				#if UNITY_REVERSED_Z
					positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#else
					positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#endif

				return positionCS;
			}

			Varyings ShadowDepthPassVertex(Attributes input)
			{
				Varyings output = (Varyings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				#if defined(DEPTH_ONLY_PASS)
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				#endif

				float3 worldNormalUv = mul(unity_ObjectToWorld, float4(input.normal, 1.0)).xyz;

				// Texture Coordinates
				output.pack2.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;
				// Shader Properties Sampling
				float3 __vertexDisplacement = ( input.normal.xyz * TCP2_TEX2D_SAMPLE_LOD(_DisplacementTex, _DisplacementTex, output.pack2.xy * _DisplacementTex_ST.xy + _DisplacementTex_ST.zw, 0).rgb * _DisplacementStrength );
				float __windTimeOffset = ( input.vertexColor.g );
				float __windSpeed = ( _WindSpeed );
				float __windFrequency = ( 1.0 );
				float4 __windSineScale2 = ( float4(2.3,1.7,1.4,1.2) );
				float __windSineStrength2 = ( .6 );
				float3 __windDirection = ( _WindDirection.xyz );
				float3 __windMask = ( input.vertexColor.rrr );
				float __windStrength = ( _WindStrength );

				#if defined(TCP2_VERTEX_DISPLACEMENT)
				input.vertex.xyz += __vertexDisplacement;
				#endif
				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				#if defined(TCP2_WIND)
				// Wind Animation
				float windTimeOffset = __windTimeOffset;
				float windSpeed = __windSpeed;
				float3 windFrequency = worldPos.xyz * __windFrequency;
				float windPhase = (_Time.y + windTimeOffset) * windSpeed;
				float3 windFactor = sin(windPhase + windFrequency);
				float4 windSin2scale = __windSineScale2;
				float windSin2strength = __windSineStrength2;
				windFactor += sin(windPhase.xxx * windSin2scale.www + windFrequency * windSin2scale.xyz) * windSin2strength;
				float3 windDir = normalize(__windDirection);
				float3 windMask = __windMask;
				float windStrength = __windStrength;
				worldPos.xyz += windDir * windFactor * windMask * windStrength;
				#endif
				input.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - vertexInput.positionWS);

				// Screen Space UV
				float4 screenPos = ComputeScreenPos(vertexInput.positionCS);
				output.screenPosition.xyzw = screenPos;
				output.normal = normalize(worldNormalUv);
				output.pack1.xyz = vertexInput.positionWS;

				#if defined(DEPTH_ONLY_PASS)
					output.positionCS = TransformObjectToHClip(input.vertex.xyz);
					#if defined(DEPTH_NORMALS_PASS)
						float3 normalWS = TransformObjectToWorldNormal(input.normal);
						output.normalWS = normalWS; // already normalized in TransformObjectToWorldNormal
					#endif
				#elif defined(SHADOW_CASTER_PASS)
					output.positionCS = GetShadowPositionHClip(input);
				#else
					output.positionCS = float4(0,0,0,0);
				#endif

				return output;
			}

			half4 ShadowDepthPassFragment(
				Varyings input
	#if defined(DEPTH_NORMALS_PASS) && defined(_WRITE_RENDERING_LAYERS)
				, out float4 outRenderingLayers : SV_Target1
	#endif
			) : SV_TARGET
			{
				#if defined(DEPTH_ONLY_PASS)
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
				#endif

				float3 positionWS = input.pack1.xyz;
				float3 normalWS = normalize(input.normal);

				// Shader Properties Sampling
				float4 __albedo = ( TCP2_TEX2D_SAMPLE(_BaseMap, _BaseMap, input.pack2.xy).rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );

				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - positionWS);
				half ndv = abs(dot(viewDirWS, normalWS));
				half ndvRaw = ndv;

				half3 albedo = half3(1,1,1);
				half alpha = __alpha;
				half3 emission = half3(0,0,0);

				#if defined(DEPTH_NORMALS_PASS)
					#if defined(_WRITE_RENDERING_LAYERS)
						uint meshRenderingLayers = GetMeshRenderingLayer();
						outRenderingLayers = float4(EncodeMeshRenderingLayer(meshRenderingLayers), 0, 0, 0);
					#endif

					#if defined(URP_12_OR_NEWER)
						return float4(input.normalWS.xyz, 0.0);
					#else
						return float4(PackNormalOctRectEncode(TransformWorldToViewDir(input.normalWS, true)), 0.0, 0.0);
					#endif
				#endif

				return 0;
			}

		#endif
		ENDHLSL

		Pass
		{
			Name "ShadowCaster"
			Tags
			{
				"LightMode" = "ShadowCaster"
			}

			ZWrite On
			ZTest LEqual

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile SHADOW_CASTER_PASS

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing
			#pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_vertex TCP2_WIND
			#pragma shader_feature_local_vertex TCP2_VERTEX_DISPLACEMENT

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

			ENDHLSL
		}

		Pass
		{
			Name "DepthOnly"
			Tags
			{
				"LightMode" = "DepthOnly"
			}

			ZWrite On
			ColorMask 0
			Cull [_Cull]

			HLSLPROGRAM

			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile DEPTH_ONLY_PASS

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_vertex TCP2_WIND
			#pragma shader_feature_local_vertex TCP2_VERTEX_DISPLACEMENT

			ENDHLSL
		}

		Pass
		{
			Name "DepthNormals"
			Tags
			{
				"LightMode" = "DepthNormals"
			}

			ZWrite On

			HLSLPROGRAM
			#pragma exclude_renderers gles gles3 glcore
			#pragma target 2.0

			#pragma multi_compile_instancing

			#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile DEPTH_ONLY_PASS
			#pragma multi_compile DEPTH_NORMALS_PASS

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			ENDHLSL
		}

		// Used for Baking GI. This pass is stripped from build.
		UsePass "Universal Render Pipeline/Lit/Meta"

	}

	FallBack "Hidden/InternalErrorShader"
	CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}

/* TCP_DATA u config(ver:"2.9.10";unity:"2022.3.45f1";tmplt:"SG2_Template_URP";features:list["UNITY_5_4","UNITY_5_5","UNITY_5_6","UNITY_2017_1","UNITY_2018_1","UNITY_2018_2","UNITY_2018_3","UNITY_2019_1","UNITY_2019_2","UNITY_2019_3","UNITY_2019_4","UNITY_2020_1","UNITY_2021_1","UNITY_2021_2","UNITY_2022_2","SHADOW_HSV","SHADOW_HSV_MASK","SPECULAR","SPECULAR_ANISOTROPIC","SPECULAR_TOON","SPECULAR_SHADER_FEATURE","EMISSION","RIM","RIM_SHADER_FEATURE","RIM_VERTEX","RIM_DIR","RIM_DIR_PERSP_CORRECTION","RIM_LIGHTMASK","SUBSURFACE_SCATTERING","SS_ALL_LIGHTS","SS_SCREEN_INFLUENCE","SUBSURFACE_AMB_COLOR","SS_MULTIPLICATIVE","SS_NO_LIGHTCOLOR","SS_SHADER_FEATURE","VERTEX_DISPLACEMENT","VERTEX_DISP_SHADER_FEATURE","BUMP","BUMP_SCALE","PARALLAX","BUMP_SHADER_FEATURE","WORLD_NORMAL_FROM_BUMP","TEXTURE_BLENDING","TEXBLEND_LINEAR","TEXBLEND_BUMP","TEXBLEND_NORMALIZE","TEXTURED_THRESHOLD","TT_SHADER_FEATURE","SHADOW_LINE","SHADOW_LINE_CRISP_AA","DIFFUSE_TINT","DIFFUSE_TINT_MASK","SKETCH_GRADIENT","SKETCH_AMBIENT","SKETCH_SHADER_FEATURE","WIND_ANIM_SIN","WIND_ANIM","WIND_SHADER_FEATURE","WIND_SIN_2","AUTO_TRANSPARENT_BLENDING","ENABLE_DECALS","ENABLE_DEPTH_NORMALS_PASS","ENABLE_COOKIES","SSAO","ENABLE_DITHER_LOD","FOG","ENABLE_LIGHTMAP","ENABLE_META_PASS","ENABLE_LIGHT_LAYERS","ENABLE_RENDERING_LAYERS","ENABLE_FORWARD_PLUS","ENABLE_DOTS_INSTANCING","TEMPLATE_LWRP"];flags:list[];flags_extra:dict[];keywords:dict[RENDER_TYPE="Opaque",RampTextureDrawer="[TCP2Gradient]",RampTextureLabel="Ramp Texture",SHADER_TARGET="3.0",RIM_LABEL="Rim Lighting"];shaderProperties:list[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,sp(name:"Parallax Height";imps:list[imp_mp_range(def:0.02;min:0;max:0.08;prop:"_Parallax";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"5b6df811-5f5a-4704-b13b-d4db4d908735";op:Multiply;lbl:"Height";gpu_inst:False;dots_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];layer_blend:dict[];custom_blend:dict[];clones:dict[];isClone:False)];customTextures:list[];codeInjection:codeInjection(injectedFiles:list[];mark:False);matLayers:list[]) */
/* TCP_HASH 315cf8988886d7852efe1c6fe7701bab */
