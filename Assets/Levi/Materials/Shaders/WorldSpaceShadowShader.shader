Shader "Custom/WorldSpaceShadowShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}		
		_Normal("Normal Map", 2D) = "white"{}
		_NormalScale("Bumpiness", Float) = 1
		_HeightMap("Height Map", 2D) = "white"{}
		_HeightMapScale("Height", Float) = 1
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_MetallicMap("Metallic", 2D) = "white"{}
		_Metallic("Metallic", Range(0,1)) = 0.0
		_OcclusionMap("Occlusion", 2D) = "white"{}
		_OcclusionStrength("Occlusion Strength", Float) = 1
		_LightOrigin("Light Origin", Vector) = (0,0,0,0)
		_LightDistance("Light Distance", Float) = 0
		_ShadowDarkness("Darkness", Range(0,10)) = 1
	}
	SubShader {
		Tags { "RenderType" = "Opaque"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _HeightMap;
		sampler2D _MetallicMap;
		sampler2D _OcclusionMap;

		struct Input 
		{
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _NormalScale;
		half _HeightMapScale;
		half _Glossiness;
		half _Metallic;
		half _OcclusionStrength;
		fixed4 _Color;
		fixed4 _LightOrigin;
		float _LightDistance;
		half _ShadowDarkness;

	
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			float4 heightMap = tex2Dlod(_HeightMap, float4(v.texcoord.xy, 0, 0));

			v.vertex.z += heightMap.b * _HeightMapScale;
		}

		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float dis = length(IN.worldPos.xyz - _LightOrigin.xyz) - _LightDistance;
			 

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c2 = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 gloss = tex2D(_MetallicMap, IN.uv_MainTex);
			o.Normal = UnpackScaleNormal(tex2D(_Normal, IN.uv_MainTex), _NormalScale);
			//o.Albedo = (dis < 0) * c.rgb - (c2 * dis) / _ShadowDarkness;
			o.Albedo = c - (c.rgb / dis) * _ShadowDarkness;
			//o.Albedo = c - (c.rgb / dis) * _ShadowDarkness;
			// Metallic and smoothness come from slider variables
			o.Metallic = gloss.r * _Metallic;
			o.Smoothness = gloss.a * _Glossiness;
			//o.Occlusion = tex2D(_OcclusionMap, IN.uv_MainTex) * _OcclusionStrength;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
