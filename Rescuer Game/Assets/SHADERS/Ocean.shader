Shader "Ocean/Ocean Setup Shader"
{
	Properties
	{
		_Surface("Surface", 2D) = "white" {}

		_OceanLeft("Ocean Left", 2D) = "white" {}
		_OceanMiddle("Ocean Middle", 2D) = "white" {}
		_OceanRight("Ocean Right", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _Surface;

		sampler2D _OceanLeft;
		sampler2D _OceanMiddle;
		sampler2D _OceanRight;

		struct Input
		{
			float2 uv_Surface;

			float2 uv_OceanLeft;
			float2 uv_OceanMiddle;
			float2 uv_OceanRight;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 Surface = tex2D(_Surface, IN.uv_Surface);

			fixed4 OceanLeft = tex2D(_OceanLeft, IN.uv_OceanLeft);
			fixed4 OceanMiddle = tex2D(_OceanMiddle, IN.uv_OceanMiddle);
			fixed4 OceanRight = tex2D(_OceanRight, IN.uv_OceanRight);	 

			half3 LC = lerp(OceanLeft.rgb, OceanMiddle.rgb, 1 - OceanLeft.a);
			half3 RC = lerp(OceanRight.rgb, LC.rgb, OceanMiddle.a + OceanLeft.a);
			half3 TP = lerp(OceanRight.rgb, RC.rgb, 1 - OceanLeft.a + 1 - OceanMiddle.a + OceanRight.a );

			o.Albedo = lerp(Surface.rgb, TP.rgb, 1 - Surface.a);
			o.Alpha = OceanLeft.a;
		}
	ENDCG
	}
		FallBack "Diffuse"
}