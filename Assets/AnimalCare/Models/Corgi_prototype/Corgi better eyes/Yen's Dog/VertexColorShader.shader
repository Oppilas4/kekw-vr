Shader "Unlit/NewUnlitShader"
{
    
     Properties
    {
        _BaseMap("Base Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _LightPos("Light Position", Vector) = (0, 5, 0, 0)
        _LightRadius("Light Radius", Float) = 5.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            float4 _BaseMap_ST;

            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            float4 _NormalMap_ST;

            float4 _LightPos;
            float _LightRadius;

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
                float2 uv           : TEXCOORD0;
                float4 color        : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float4 color       : COLOR;
                float3 worldPos    : TEXCOORD1;
                float3 tangentWS   : TEXCOORD2;
                float3 binormalWS  : TEXCOORD3;
                float3 normalWS    : TEXCOORD4;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                float3 normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                float3 tangentWS = normalize(TransformObjectToWorldDir(IN.tangentOS.xyz));
                float3 binormalWS = cross(normalWS, tangentWS) * IN.tangentOS.w;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                OUT.color = IN.color;
                OUT.worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.normalWS = normalWS;
                OUT.tangentWS = tangentWS;
                OUT.binormalWS = binormalWS;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3x3 TBN = float3x3(IN.tangentWS, IN.binormalWS, IN.normalWS);
                float3 normalMap = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, IN.uv));
                float3 normalWS = normalize(mul(normalMap, TBN));

                float3 lightDir = normalize(_LightPos.xyz - IN.worldPos);
                float dist = distance(_LightPos.xyz, IN.worldPos);
                float attenuation = saturate(1.0 - dist / _LightRadius);
                float NdotL = saturate(dot(normalWS, lightDir)) * attenuation;

                float4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);
                return baseColor * IN.color * NdotL;
            }
            ENDHLSL
        }
    }
}
