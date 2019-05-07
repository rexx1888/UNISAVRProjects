// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Alan Zucconi
// www.alanzucconi.com


Shader "Hidden/Heatmap" {
	Properties{
		_HeatTex("Texture", 2D) = "white" {}
	}
	
	SubShader {
		Tags { "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha // Alpha blend

		Pass {
			CGPROGRAM
			#pragma vertex vert             
			#pragma fragment frag
			#define arrayLength 2000

			struct vertInput {
				float4 pos : POSITION;
			};

			struct vertOutput {
				float4 pos : POSITION;
				fixed3 worldPos : TEXCOORD1;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = UnityObjectToClipPos(input.pos);
				o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
				return o;
			}

			uniform float HeatMapOutCome = 0;


			sampler2D _HeatTex;

			half4 frag(vertOutput output) : COLOR {
				
				//then uses h to pick a point along the line, according to intensity.
				half4 color = tex2D(_HeatTex, fixed2(HeatMapOutCome, 0.5));
				return color;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}