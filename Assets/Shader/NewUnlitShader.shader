Shader "Mobile/SprayPaint_TextShadow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        // Chroma key
        _KeyColor ("Key Color", Color) = (1,1,0,1)
        _Threshold ("Chroma Threshold", Range(0,1)) = 0.32

        // Shadow
        _ShadowColor ("Shadow Color", Color) = (0,0,0,0.6)
        _ShadowOffset ("Shadow Offset", Vector) = (1,-1,0,0)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            fixed4 _KeyColor;
            float _Threshold;

            fixed4 _ShadowColor;
            float4 _ShadowOffset;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 shadowUV : TEXCOORD1;
                fixed4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = v.uv;
                o.shadowUV = v.uv + _ShadowOffset.xy * 0.001;

                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);

                // --- CHROMA KEY ---
                float dist = distance(tex.rgb, _KeyColor.rgb);

                if (dist < _Threshold)
                    discard;

                // --- SHADOW ---
                fixed4 shadowTex = tex2D(_MainTex, i.shadowUV);

                fixed shadowAlpha = shadowTex.a * _ShadowColor.a;

                // --- FINAL COLOR ---
                fixed4 col;
                col.rgb = tex.rgb * i.color.rgb;
                col.a = tex.a * i.color.a;

                // blend shadow behind
                col.rgb = lerp(_ShadowColor.rgb, col.rgb, col.a);
                col.a = max(col.a, shadowAlpha);

                return col;
            }

            ENDCG
        }
    }
}