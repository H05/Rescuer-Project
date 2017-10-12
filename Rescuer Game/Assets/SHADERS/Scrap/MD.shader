Shader "Unlit/MobileDiff"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_MainTex2("Texture", 2D) = "white" {}
	_WaveSpeed("Wave Speed", float) = 0
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

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float _WaveSpeed;
	float4 _MainTex_ST;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);

		//LOOK! _MainTex_ST.xy is tiling and _MainTex_ST.zw is offset
		//o.uv = float2(v.uv.x, v.uv.y - 0.7) * float2(1, 1) + float2(_MainTex_ST.z, _MainTex_ST.w );
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		float Offset = (_MainTex_ST.z + _WaveSpeed * _Time);
	i.uv = float2(i.uv.x, i.uv.y - 0.7) * float2(1, 1) + float2(Offset, _MainTex_ST.w);
	fixed4 col = tex2D(_MainTex, i.uv);

	return col;
	}
		ENDCG
	}
	}
}
