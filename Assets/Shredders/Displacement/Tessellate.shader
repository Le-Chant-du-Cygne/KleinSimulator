Shader "CustomGraph/Tessellation edge length" {
       Properties {
            _EdgeLength ("Edge length", Range(2,50)) = 15
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _DispTex ("Disp Texture", 2D) = "gray" {}
            _NormalMap ("Normalmap", 2D) = "bump" {}
            _Displacement ("Displacement", Range(0, 2.0)) = 0.3
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
            sampler2D _NormalMap;
            fixed4 _Color;
            float _EdgeLength;
            float _SpecPower; 
            float4 _SpecularColor;
            
            //uniform float4 _MainTex_ST;	

          
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

            sampler2D _DispTex;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz += v.normal * d;
            }

            struct Input {
                float2 uv_MainTex;
                float2 uv_NormalMap;
            };

           

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Specular = 0.2;
                o.Gloss = 1.0;
                o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
            }
            ENDCG
        }
        FallBack "Diffuse"
    }