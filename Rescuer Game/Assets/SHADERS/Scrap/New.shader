Shader "GrabPassInvert"
{
	Properties
	{
	_MainTex("Texture", 2D) = "white" {}
	_BackgroundTexture("Normalmap", 2D) = "bump" {}
	}
	SubShader
	{  

		// Draw ourselves after all opaque geometry
		Tags{ "Queue" = "Transparent" }

		// Grab the screen behind the object into _BackgroundTexture
		GrabPass
	{
		"_BackgroundTexture"
	}

		// Render the object with the texture generated above, and invert the colors
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct appdata
		{

		float4 vertex : POSITION;

		};
		struct v2f
		{
		float4 grabpos : TEXCOORD0;
		float4 vertex : SV_POSITION;
		};

	v2f vert(appdata_base v) {
		v2f o;
 
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.grabpos.xy = (float2(o.vertex.x, o.vertex.y * 2) + o.vertex.w) * 0.5;
		return o;
	}

	sampler2D _BackgroundTexture;
	sampler2D _MainTex;
	float2 frag(v2f IN) : SV_Target
	{
		half4 bump = tex2D(_MainTex, IN.grabpos);
		half2 distortion = UnpackNormal(bump).rb;
		IN.grabpos.xy += distortion * 25;

		return bump;
	}
		ENDCG
	}

	}
}