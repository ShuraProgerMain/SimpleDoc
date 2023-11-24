Shader "Custom/RhombusSprite" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _InternalLineSize ("InternalLineSize", Range(0, 5)) = 1
        _ExternalLineSize ("ExternalLineSize", Range(0, 5)) = 1
        [HDR]_Color ("Color", Color) = (1, 1, 1, 0)
        _ColorThickness ("ColorThickness", Color) = (1, 1, 1, 0)
        _ThicknessSize ("ThicknessSize", Range(0, 1)) = 1
        [MaterialToggle] _IsToggled("IsToggle", Float) = 0
    }

    SubShader {
        Tags { "RenderType" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            float4 _MainTex_ST;

            uniform half _ExternalLineSize;
            uniform half _InternalLineSize;
            uniform fixed4 _Color;
            uniform fixed4 _ColorThickness;
            uniform half _ThicknessSize;
            uniform float _IsToggled;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target {
                float2 center = float2(0.5, 0.5);
                float2 distance = abs(i.uv - center);

                half rhombusWidth = 0.1;
                half rhombusHeight = 0.1;

                // Применяем смещение и масштабирование к текстурным координатам
                float2 offset = float2(0, 0); // Задайте желаемое смещение
                float2 tiling = float2(1, 1); // Задайте желаемое масштабирование

                float2 uv = i.uv * tiling + offset;
                uv = frac(uv);

                fixed4 result;
                if(_IsToggled > 0)
                {
                    result = tex2D(_MainTex, uv) * _Color;
                }
                else
                {
                    result = tex2D(_MainTex, uv) + _Color;
                    result.a *= _Color.a;
                }
                
                if (distance.x / rhombusWidth + distance.y / rhombusHeight < _InternalLineSize) {
                    discard;
                }
                
                if (distance.x / rhombusWidth + distance.y / rhombusHeight > _ExternalLineSize) {
                    discard;
                }

                half distanceToBorder = distance.x / rhombusWidth + distance.y / rhombusHeight;

                if (distanceToBorder < _InternalLineSize + _ThicknessSize || distanceToBorder > _ExternalLineSize - _ThicknessSize)
                {
                    result = _ColorThickness;
                }
                
                return result;
            }
            ENDCG
        }
    }
}
