CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _OverlayTex;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv2_OverlayTex;
};

void surf(Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	fixed4 c2 = tex2D(_OverlayTex, IN.uv2_OverlayTex) * _Color;
	o.Albedo = lerp(c.rgb, c2.rgb, c2.a);
	o.Alpha = c.a;
}
ENDCG
 }