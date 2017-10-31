Shader "Water/CustomWater"
{
	Properties
	{ _OceanHeight("Ocean Height", Float) = 0.3


		_OceanTex("Ocean Texture", 2D) = "white" {}
	_WaterColor("Ocean Color", Color) = (0,30,255,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)

		_OceanTex2("Wave Texture", 2D) = "white" {}
	_WaterColor2("Wave Color", Color) = (35,75,250,1)

		_WaveOffset("Wave Offset", Float) = 0
		_WaveAmplitude("Wave Amplitude", Float) = 0
		_WaveCount("Wave Count", Float) = 0
		_WaveSpeed("Wave Speed", Float) = 0
		_WaveWiggle("Wave Wiggle", Float) = 0
		_WaveDissplacement("Wave Dissplacement", Float) = 0
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent"  "IgnoreProjector" = "True" "RenderType" = "Transparent" "LightMode" = "Always" }
		LOD 200

		Cull Front
		ZWrite Off
		Blend One One

		CGINCLUDE
		#pragma vertex vert
		#pragma fragment frag
 
		#include "UnityCG.cginc"

		float _OceanHeight;

	sampler2D _OceanTex;
	fixed4 _WaterColor;

	sampler2D _OceanTex2;
	fixed4 _WaterColor2;

	float _WaveOffset;
	float _WaveWiggle;
	float _WaveAmplitude;
	float _WaveCount;
	float _WaveSpeed;
	float _WaveDissplacement;

	float WaveGenerator(float x, float Height, float Shift)
	{
		fixed wave = cos((x - _WaveWiggle * _Time) * _WaveDissplacement) * 0.008 + _WaveAmplitude / 100 * sin((x - Shift)* _WaveCount + _Time * _WaveSpeed);
		return wave + Height;
	}
	ENDCG
		Pass{
		CGPROGRAM

		struct appdata {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		float2 texcoord1 : TEXCOORD1;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		float2 texcoord1 : TEXCOORD1;
	};

	v2f vert(appdata v) {
		v2f o;
		o.texcoord = v.texcoord;
		o.texcoord1 = float2(v.texcoord1.x, v.texcoord1.y - _OceanHeight);
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	fixed4 frag(v2f IN) : SV_TARGET
	{
		fixed4 oceanDetail = tex2D(_OceanTex, IN.texcoord);

	float waveHeight = WaveGenerator(IN.texcoord.x, _OceanHeight, 0);

	fixed isTexelAbove = step(waveHeight, IN.texcoord.y);
	fixed isTexelBelow = 1 - isTexelAbove;

	fixed waterColBlendFac = isTexelBelow * _WaterColor.a;

	oceanDetail.r = oceanDetail.r * _WaterColor.r * waterColBlendFac;
	oceanDetail.g = oceanDetail.g * _WaterColor.g * waterColBlendFac;
	oceanDetail.b = oceanDetail.b * _WaterColor.b * waterColBlendFac;
 

	return oceanDetail.a;
	}
		ENDCG
	}
	}
}