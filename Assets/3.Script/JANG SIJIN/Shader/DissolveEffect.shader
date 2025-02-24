Shader "Custom/DissolveEffect"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}  // 기존 텍스처 유지
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}  // 디졸브 노이즈 텍스처
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0.0  // 디졸브 진행 정도
        _EdgeColor ("Edge Color", Color) = (1,0,0,1)  // 경계선 컬러
        _EdgeWidth ("Edge Width", Range(0,0.2)) = 0.05  // 디졸브 에지 너비
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _DissolveAmount;
            float4 _EdgeColor;
            float _EdgeWidth;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 원래 텍스처 불러오기
                fixed4 baseColor = tex2D(_MainTex, i.uv);

                // 디졸브 노이즈 텍스처 값 샘플링
                float dissolveNoise = tex2D(_DissolveTex, i.uv).r;

                // 디졸브 처리
                if (dissolveNoise < _DissolveAmount)
                    discard;  // 투명하게 만듦

                // 에지 효과 적용
                float edgeFactor = smoothstep(_DissolveAmount, _DissolveAmount + _EdgeWidth, dissolveNoise);
                fixed4 finalColor = lerp(_EdgeColor, baseColor, edgeFactor);

                return finalColor;
            }
            ENDCG
        }
    }
}
