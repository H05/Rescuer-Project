Shader "Water/Waves" 
{
		Properties
		{
			_OceanHeight("Ocean Height", Float) = 0.3
			_OutlineColor("Outline Color", Color) = (5,0,0,1)
			_WaterColor("Water Color", Color) = (35,75,250,1)
			_MainTex("Base (RGB)", 2D) = "white" { }
			_MainTex2("Base (RGB)", 2D) = "white" { }
			_WaveAmplitude("Wave Amplitude", Float) = 0
			_WaveCount("Wave Count", Float) = 0
			_WaveSpeed("Wave Speed", Float) = 0
			_WaveWiggle("Wave Wiggle ", Float) = 0
			_WaveWiggleSpeed ("Wave Wiggle Speed", Float) = 0
			_WaveDissplacement("Wave Dissplacement", Float) = 0

		}

		CGINCLUDE
		#include "UnityCG.cginc"

		struct appdata 
		{

		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
 
		};

		struct v2f 
		{

		float4 pos : SV_POSITION;
		float2 texcoord : TEXCOORD0;

		};

		sampler2D _MainTex;
		sampler2D _MainTex2;
		float _OceanHeight;
		uniform float4 _OutlineColor;
		uniform float4 _WaterColor;
		float _WaveOffset;
		float _WaveWiggle;
		float _WaveWiggleSpeed;
		float _WaveAmplitude;
		float _WaveCount;
		float _WaveSpeed;
		float _WaveDissplacement;

		float WaveGenerator(float x, float Height, float Shift)
		{
			fixed wave = cos((x - _WaveWiggle * _Time) * _WaveDissplacement) * _WaveWiggleSpeed + (_WaveAmplitude / 100) * sin((x - Shift)* _WaveCount + _Time * -_WaveSpeed);
			return wave + Height;
		}

		v2f vert(appdata v) 
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.texcoord = v.texcoord;
			return o;
		}
 
		ENDCG

		SubShader{
		Pass
		{
			Cull Off
			ZWrite On
			ZTest Greater
			ColorMask RGB  

			Blend SrcAlpha OneMinusSrcAlpha

										 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f IN) : SV_TARGET
			{

			fixed4 oceanDetail = tex2D(_MainTex, IN.texcoord) * _OutlineColor;

			float waveHeight = WaveGenerator(IN.texcoord.x,- _OceanHeight, 0);

			fixed isTexelAbove = step(waveHeight, IN.texcoord.y);
			fixed isTexelBelow = 1 - isTexelAbove;

			fixed waterColBlendFac = isTexelBelow;

			oceanDetail.r = oceanDetail.r * _OutlineColor.r * waterColBlendFac;
			oceanDetail.g = oceanDetail.g * _OutlineColor.g * waterColBlendFac;
			oceanDetail.b = oceanDetail.b * _OutlineColor.b * waterColBlendFac;
			oceanDetail.a = oceanDetail.a * _OutlineColor.a * waterColBlendFac;

			return oceanDetail * _OutlineColor;
			}

		ENDCG
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			Material
			{
				Diffuse[_Color]
			}
				Lighting On	 
		}
	}
}
