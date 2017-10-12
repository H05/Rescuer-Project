 Shader "Mobile/Diffuse2" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_MainTex2("Base2 (RGB)", 2D) = "white" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 150

		CGPROGRAM
#pragma surface surf Lambert noforwardadd

		sampler2D _MainTex;
		sampler2D _MainTex2;
	struct Input {
		float2 uv_MainTex;
		float2 uv_MainTex2;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);
		o.Albedo = lerp(c.rgb, c2.rgb, c2.a);
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Mobile/VertexLit"
}
