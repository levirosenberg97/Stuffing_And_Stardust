Shader "Unlit/SmokeShader"
{
	Properties
	{
		_Color("Color",Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		_SecondaryTex("2nd Albedo", 2D) = "white" {}
		_ThirdTex("3rd Albedo", 2D) = "white" {}

		//sets albedo scrolling speed
		_ScrollSpeedX("X Speed", Range(0,10)) = 1
		_ScrollSpeedY("Y Speed", Range(0,10)) = 1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			Zwrite off
			Blend SrcAlpha One
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv  : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			float4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SecondaryTex;
			float4 _SecondaryTex_ST;
			sampler2D _ThirdTex;
			float4 _ThirdTex_ST;
			
			float _ScrollSpeedX;
			float _ScrollSpeedY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv  = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.uv, _SecondaryTex);
				o.uv3 = TRANSFORM_TEX(v.uv, _ThirdTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				i.uv2 += fixed2(_ScrollSpeedX, _ScrollSpeedY);
				i.uv3 += fixed2(_ScrollSpeedY, _ScrollSpeedX);

				float4 c  = tex2D(_MainTex, i.uv);
				float4 c2 = tex2D(_SecondaryTex, i.uv2);
				float4 c3 = tex2D(_ThirdTex, i.uv3);


				float3 col1 = c.rgb;
				float3 col2 = c2.rgb;
				float3 col3 = c3.rgb;
				float blendAlpha = c.a * c2.a * c3.a;

				return float4 ((col1 * col2) * col3 , blendAlpha) * _Color;

				return _Color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
			}
			ENDCG
		}
	}
}
