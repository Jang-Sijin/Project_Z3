Shader "UI/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // UI���� ����� �⺻ �ؽ�ó
        _DissolveTex ("Dissolve Noise", 2D) = "white" {}  // ������ ���� �ؽ�ó
        _Cutoff ("Dissolve Amount", Range(0,1)) = 0.0  // ������ ���� ���� (1 �� 0)
        _EdgeSize ("Dissolve Edge Size", Range(0,1)) = 0.2  // ������ ��輱 �β�
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)  // ������ ��輱 ����
    }

    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // UI ���� ����
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _Cutoff;
            float _EdgeSize;
            float4 _EdgeColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float noise = tex2D(_DissolveTex, i.uv).r;

                // ������ ���
                float dissolveEdge = smoothstep(_Cutoff, _Cutoff - _EdgeSize, 1 - noise);
                
                // ��輱 ����
                float4 edgeHighlight = _EdgeColor * (1 - dissolveEdge);

                // ������ ȿ�� ����
                clip(dissolveEdge - _Cutoff);

                return col + edgeHighlight;
            }
            ENDCG
        }
    }
}
