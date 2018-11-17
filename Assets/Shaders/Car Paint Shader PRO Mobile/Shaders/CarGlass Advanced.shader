Shader "RedDotGames/Mobile/Car Glass Advanced" {
Properties {
	_Color ("Main Color (RGB)", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color (RGB)", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) Mask (A)", 2D) = "black" {} 
	_Cube ("Reflection Cubemap (CUBE)", Cube) = "_Skybox" { TexGen CubeReflect }
	_FresnelPower ("Fresnel Power", Range(0.05,5.0)) = 0.75
	_TintColor ("Tint Color (RGB)", Color) = (1,1,1,1)
	_AlphaPower ("Alpha", Range(0.0,2.0)) = 1.0
	_OpaqueReflection ("Opaque Reflection", Range(0.0,1.0)) = 0.0
	
}
SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	
CGPROGRAM
#pragma surface surf Lambert alpha
#pragma target 2.0
#pragma exclude_renderers d3d11 xbox360 ps3 d3d11_9x flash
sampler2D _MainTex;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _TintColor;
fixed4 _ReflectColor;
half _AlphaPower;
half _OpaqueReflection;

half _FresnelPower;

struct Input {
	fixed2 uv_MainTex;
	fixed3 worldRefl;
	fixed3 viewDir;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) 
{
	half4 tex = tex2D(_MainTex, IN.uv_MainTex);
	half4 c = tex * _Color;
	
	half3 worldReflVec = WorldReflectionVector(IN, o.Normal);	
	half4 reflcol = texCUBE(_Cube, worldReflVec);
	
	fixed fcbias = 0.20373;
	fixed facing = saturate(1.0 - max(dot( normalize(IN.viewDir.xyz), normalize(o.Normal)), 0.0));
	fixed refl2Refr = max(fcbias + (1.0-fcbias) * pow(facing, _FresnelPower), 0);			
	
	o.Albedo =  reflcol.rgb * _ReflectColor.rgb * _TintColor * (1-(tex.a*(1-_OpaqueReflection))) + c.rgb;
	o.Emission = o.Albedo * 0.25;
	o.Alpha = saturate(refl2Refr * _AlphaPower); 
	//o.Alpha = max(tex.a,saturate(refl2Refr * _AlphaPower)); 
	
}
ENDCG

}
	
FallBack "Reflective/VertexLit"
} 

