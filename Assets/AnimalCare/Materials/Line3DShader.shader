Shader"Unlit/Line3DShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {} // Base texture
        _LineWidth ("Line Width", Float) = 0.1 // Line width for extrusion
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

sampler2D _MainTex; // Texture sampler
float4 _MainTex_ST; // Texture UV scale and offset
float _LineWidth; // Line width for extrusion

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0; // UV coordinates
    float4 color : COLOR; // Vertex color
};

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0; // UV coordinates for fragment shader
    float4 color : COLOR; // Color passed to fragment shader
};

v2f vert(appdata v)
{
    v2f o;

                // Calculate the extrusion direction
    float3 tangent = normalize(float3(1, 0, 0)); // This should follow the line direction; modify as needed
    float3 normal = cross(tangent, float3(0, 1, 0)); // Calculate normal for the extrusion

                // Apply extrusion to the line's vertices
    v.vertex.xyz += normal * _LineWidth;

                // Transform vertex position to clip space
    o.pos = UnityObjectToClipPos(v.vertex);

                // UV calculations
    o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;

                // Pass the color
    o.color = v.color;

    return o;
}

fixed4 frag(v2f i) : SV_Target
{
                // Fetch texture color
    fixed4 texColor = tex2D(_MainTex, i.uv);

                // Combine texture color and vertex color
    return texColor * i.color;
}
            ENDCG
        }
    }

    // Fallback shader
Fallback"Unlit/Color"
}
