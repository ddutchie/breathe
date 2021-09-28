Shader "NoiseBall/Surface"
{
    Properties
    {
        _Color ("", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Front
        CGPROGRAM

        #pragma surface surf Standard vertex:vert nolightmap addshadow 
        // #pragma surface surf Unlit vertex:vert nolightmap addshadow
        #pragma target 3.0
        #include "Common.cginc"

        struct Input { float dummy; };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        half4 LightingUnlit (SurfaceOutput s, half3 lightDir, half atten) {
            half4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            return c;
        }
        

        void vert(inout appdata_full v)
        {
            float3 v1 = displace(v.vertex.xyz);
            float3 v2 = displace(v.texcoord.xyz);
            float3 v3 = displace(v.texcoord1.xyz);
            v.vertex.xyz = v1;
            v.normal = normalize(cross(v2 - v1, v3 - v1));
        }

        // void surf(Input IN, inout SurfaceOutput o)
        // {
            //     o.Albedo = _Color.rgb;
            //     // o.Metallic = _Metallic;
            //     // o.Smoothness = _Glossiness;
        // }
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color.rgb;
            o.Alpha = 0.75f;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
