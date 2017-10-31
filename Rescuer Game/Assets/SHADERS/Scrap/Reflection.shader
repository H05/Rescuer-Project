// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FX/Mirror"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		[HideInInspector] _ReflectionTex("", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct appdata_t
	{
		float2 uv : TEXCOORD0;
		float4 refl : TEXCOORD1;
		float4 pos : POSITION;
		float4 col: COLOR;
	};
	struct v2f
	{
		half2 uv : TEXCOORD0;
		float4 refl : TEXCOORD1;
		float4 pos : SV_POSITION;
		fixed4 col : COLOR;
	};
	float4 _MainTex_ST;
	fixed4 _Color;
	v2f vert(appdata_t i)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(i.pos);
		o.uv = TRANSFORM_TEX(i.uv, _MainTex);
		o.col = i.col * _Color;
		o.refl = ComputeScreenPos(o.pos);
		return o;
	}
	sampler2D _MainTex;
	sampler2D _ReflectionTex;
	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 tex = tex2D(_MainTex, i.uv) * i.col;
	tex.rgb *= tex.a;
	fixed4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.refl));
	return tex * refl;
	}
		ENDCG
	}
	}
}