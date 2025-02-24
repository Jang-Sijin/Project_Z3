Shader "Custom/DissolveEffect"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}  // ���� �ؽ�ó ����
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}  // ������ ������ �ؽ�ó
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0.0  // ������ ���� ����
        _EdgeColor ("Edge Color", Color) = (1,0,0,1)  // ��輱 �÷�
        _EdgeWidth ("Edge Width", Range(0,0.2)) = 0.05  // ������ ���� �ʺ�
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
                // ���� �ؽ�ó �ҷ�����
                fixed4 baseColor = tex2D(_MainTex, i.uv);

                // ������ ������ �ؽ�ó �� ���ø�
                float dissolveNoise = tex2D(_DissolveTex, i.uv).r;

                // ������ ó��
                if (dissolveNoise < _DissolveAmount)
                    discard;  // �����ϰ� ����

                // ���� ȿ�� ����
                float edgeFactor = smoothstep(_DissolveAmount, _DissolveAmount + _EdgeWidth, dissolveNoise);
                fixed4 finalColor = lerp(_EdgeColor, baseColor, edgeFactor);

                return finalColor;
            }
            ENDCG
        }
    }
}
