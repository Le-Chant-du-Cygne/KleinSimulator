Shader "CustomGraph/Tessellation Displacement lerp" {
       Properties {
       		_Change ("Change ratio", Range(0, 1.0)) = 0.0
            _EdgeLength ("Edge length", Range(2,50)) = 15
            _MainTex ("Base (RGB)", 2D) = "white" {}
           	_StartDMap ("Strat Disp Map", 2D) = "gray" {}
            _StartNMap ("Start Normal map", 2D) = "bump" {}
            _TargetDMap ("Target Disp Map", 2D) = "gray" {}
            _TargetNMap ("Target Normal map", 2D) = "bump" {}
            _StartDisp ("Start Displacement", Range(0, 2.0)) = 0.3
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
            sampler2D _TargetNMap;
            fixed4 _Color;
            float _EdgeLength;
            float _SpecPower; 
            float4 _SpecularColor;
            
            float _Change;
			
            float _MinSpec;
            float _MaxSpec;
            

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
            sampler2D _TargetDMap;
            float _StartDisp;
            float _TargetDisp;

            void disp (inout appdata v)
            {
            	float d1 = tex2Dlod(_StartDMap, float4(v.texcoord.xy,0,0)).r * _StartDisp;
                float d2 = tex2Dlod(_TargetDMap, float4(v.texcoord.xy,0,0)).r * _TargetDisp;
                v.vertex.xyz += v.normal * lerp(d1, d2, _Change);
            }

            struct Input {
                float2 uv_MainTex;
                float2 uv_StartNMap;
                float2 uv_TargetNMap;
            };

           

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Specular = 0.2;
                o.Gloss = 1.0;
                
                half3 n1 = UnpackNormal(tex2D(_StartNMap, IN.uv_StartNMap));
                half3 n2 = UnpackNormal(tex2D(_TargetNMap, IN.uv_TargetNMap));
                
                o.Normal = lerp(n1, n2, _Change);
            }
            ENDCG
        }
        FallBack "Diffuse"
    }