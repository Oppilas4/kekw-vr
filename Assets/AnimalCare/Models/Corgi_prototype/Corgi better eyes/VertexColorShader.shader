Shader "Unlit/NewUnlitShader"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // P‰‰tekstuuri (Basemap)
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

            sampler2D _MainTex; // Tekstuurin sampler
            float4 _MainTex_ST; // UV-skaalaus

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0; // UV-koordinaatit
                float4 color : COLOR; // Vertex color
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0; // UV-koordinaatit fragment-shaderille
                float4 color : COLOR; 
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw; // Muokkaa UV:ta
                o.color = v.color; // Siirret‰‰n v‰ri fragment shaderille
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv); // Haetaan tekstuurin v‰ri
                return texColor * i.color; // Yhdistet‰‰n tekstuurin v‰ri ja vertex color
            }
            ENDCG
        }
    }
}
