Shader "CustomGraph/Tesselation Displacement lerp Cloudy Water" {
	 Properties {
       		_Change ("Change ratio", Range(0, 1.0)) = 0.0
            _EdgeLength ("Edge length", Range(2,50)) = 15
            _MainTex ("Base (RGB)", 2D) = "white" {}
           	_StartDMap ("Start Disp Map", 2D) = "gray" {}
           	_StartDMap2 ("Start Disp Map 2", 2D) = "gray" {}
            _StartNMap ("Start Normal map", 2D) = "bump" {}
            _StartNMap2 ("Start Normal map 2", 2D) = "bump" {}
            _TargetDMap ("Target Disp Map", 2D) = "gray" {}
            _TargetNMap ("Target Normal map", 2D) = "bump" {}
            
            _Phase("Phase", Float) = 0.1
            
            _FlowSpeed ("Wave Speed", float) = 0.05
		
			_FlowScale1U ("Flow Scale 1 horizontal speed", Range (-1.0, 1.0)) = 0.5
			_FlowScale1V ("Flow Scale 1 vertical speed", Range (-1.0, 1.0)) = 0.5
			_FlowScale2U ("Flow Scale 2 horizontal speed", Range (-1.0, 1.0)) = 0.5
			_FlowScale2V ("Flow Scale 2 vertical speed", Range (-1.0, 1.0)) = 0.5
            
            
            _StartDisp ("Start Displacement", Range(0, 2.0)) = 0.3
            _StartDisp2 ("Start Displacement 2", Range(0, 2.0)) = 0.3
            _TargetDisp ("Target Displacement", Range(0, 2.0)) = 0.3
            _Color ("Color", color) = (1,1,1,0)
            _SpecularColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
            _MinSpec ("min spec", Float) = 0.3
            _MaxSpec ("max spec", Float) = 0.9
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 300
            
            CGPROGRAM
            #pragma surface surf CustomBlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessEdge nolightmap
            #pragma target 5.0
            #include "Tessellation.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

			sampler2D _MainTex;
			sampler2D _StartNMap;
			sampler2D _StartNMap2;
            sampler2D _TargetNMap;
            fixed4 _Color;
            float _EdgeLength;
            float _SpecPower; 
            float4 _SpecularColor;
            
            float _Change;
			
            float _MinSpec;
            float _MaxSpec;
            
            float _Phase;
            
            float _FlowSpeed;
			float _FlowScale1U; 
			float _FlowScale1V;
			float _FlowScale2U;
			float _FlowScale2V;
            

			inline fixed4 LightingCustomBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, float atten)
			{
				float3 halfVector = normalize(lightDir + viewDir);
				
				float diff = max(0, dot(s.Normal, lightDir));
				
				float nh = max (0, dot(s.Normal, halfVector));
				
				float spec = smoothstep(_MinSpec, _MaxSpec, nh); //pow(nh, _SpecPower) * _SpecularColor;
				
				half4 c;
				c.rgb = (_LightColor0.rgb * _SpecularColor.rgb * spec) ;//* (atten * 1.5); //*2); //+ (s.Albedo * _LightColor0.rgb * diff) for normal effect
				c.a = s.Alpha;
				return c; 
			}
			
            float4 tessEdge (appdata v0, appdata v1, appdata v2)
            {
                return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
            }
			
			sampler2D _StartDMap;
			sampler2D _StartDMap2;
            sampler2D _TargetDMap;
            float _StartDisp;
            float _StartDisp2;
            float _TargetDisp;

            void disp (inout appdata v)
            {

            	//PROCESS FLOW
				float f = _Time.y * _FlowSpeed;//frac(_Time.y * _FlowSpeed);
				float2 dir1 = float2 (frac(_FlowScale1U * f), frac(_FlowScale1V * f));
				float2 dir2 = float2 (frac(_FlowScale2U * f), frac(_FlowScale2V * f));
            	
            	//WAVE
            	float ph = abs(sin(_Time.y * _Phase));
            	float d1 = tex2Dlod(_StartDMap, float4((v.texcoord.xy + dir1), 0, 0)).r * _StartDisp;
            	float d2 = tex2Dlod(_StartDMap2, float4((v.texcoord.xy + dir2),0,0)).r * _StartDisp2;
                float d = lerp(d1, d2, 0.25 + (ph * 0.5));
                
                //TARGET
                float targetD = tex2Dlod(_TargetDMap, float4(v.texcoord.xy,0,0)).r * _TargetDisp;
                
                v.vertex.xyz += v.normal * lerp(d, targetD, _Change);
            }

            struct Input {
                float2 uv_MainTex;
                float2 uv_StartNMap;
                float2 uv_StartNMap2;
                float2 uv_TargetNMap;
            };

           

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Specular = 0.2;
                o.Gloss = 1.0;
                
                //PROCESS FLOW
				float f = _Time.y * _FlowSpeed;
				float2 dir1 = float2 (frac(_FlowScale1U * f), frac(_FlowScale1V * f));
				float2 dir2 = float2 (frac(_FlowScale2U * f), frac(_FlowScale2V * f));
                
                //WAVE 
                float ph = abs(sin(_Time.y * _Phase));
                half3 n1 = UnpackNormal(tex2D(_StartNMap, IN.uv_StartNMap + dir1));
                half3 n2 = UnpackNormal(tex2D(_StartNMap2, IN.uv_StartNMap2 + dir2));
                half3 n = lerp(n1, n2, 0.25 + (ph * 0.5));
                
                
                //TARGET
                half3 targetN = UnpackNormal(tex2D(_TargetNMap, IN.uv_TargetNMap));
               	
                
                o.Normal = lerp(n, targetN, _Change);
            }
            ENDCG
        }
        FallBack "Diffuse"
    }