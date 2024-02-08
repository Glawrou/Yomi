Shader "Custom/StrangeTransparentShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0,1)) = 0.5
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };
            
            float4 _MainColor;
            float _Transparency;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _MainColor;
                
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.color;
                
                // Apply transparency
                col.a *= _Transparency;
                
                return col;
            }
            ENDCG
        }
    }
}
