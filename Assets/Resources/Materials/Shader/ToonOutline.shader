Shader "MarcoMinganna/ToonOutline" {
		Properties{
			_MainTex("Texture",2D) = "white"{}
			_OutlineColor ("Outline Color",Color)=(0,0,0,1)
			_Outline ("Outline Width",Range(.002,0.1))=.0005
			_RampTex("Ramp Texture", 2D) = "white"{}
			_myBump("Normal Texture",2D) = "bump"{}
		}
			SubShader{
			CGPROGRAM
			#pragma surface surf ToonRamp

	

			sampler2D _MainTex;
			sampler2D _RampTex;
			sampler2D _myBump;

			float4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				float diff = dot(s.Normal, lightDir);
				float h = diff * 0.5 + 0.5;
				float2 rh = h;
				float3 ramp = tex2D(_RampTex, rh).rgb;

				float4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
				c.a = s.Alpha;
				return c;
			}

			struct Input {
				float2 uv_MainTex;
				float2 uv_myBump;
			};
			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex,IN.uv_MainTex).rgb;

			}
			ENDCG

			Pass{
				Cull Front
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex:POSITION;
					float3 normal: NORMAL;
					float2 uv:TEXCOORD0;
				};
				struct v2f {
					float4 pos:SV_POSITION;
					fixed4 color : COLOR;
					float3 normal:TEXCOORD0;
					float2 uv:TEXCOORD1;
				};
				float _Outline;
				float4 _OutlineColor;
				sampler2D _myBump;
				
				v2f vert(appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					float3 norm = v.normal;
					float2 offset = TransformViewToProjection(norm.xy);
					o.pos.xy += offset * o.pos.z * _Outline;
					o.color = _OutlineColor;
					return o;
				}
				fixed4 frag(v2f i) :SV_Target
				{
					float3 normal = UnpackNormal(tex2D(_myBump, i.uv));
					return i.color;
				}
				ENDCG
				}


			}
				FallBack "Diffuse"
	}
