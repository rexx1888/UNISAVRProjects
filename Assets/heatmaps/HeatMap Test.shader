Shader "Custom/Heatmap Test"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DeltaX("Delta X", Float) = 0.01
		_DeltaY("Delta Y", Float) = 0.01
		
		_HeatTex("Texture", 2D) = "white" {}

	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
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
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _DeltaX;
			float _DeltaY;
			sampler2D _CameraDepthTexture;

			float sobel(sampler2D tex, float2 uv)
			{
				float2 delta = float2(_DeltaX, _DeltaY);
				float4 horizontal = float4(0, 0, 0, 0);
				float4 vert = float4(0, 0, 0, 0);

				horizontal += tex2D(tex, (uv + float2(-1, -1) * delta)) * 1.0f;
				horizontal += tex2D(tex, (uv + float2(-1, 0) * delta)) * 2.0f;
				horizontal += tex2D(tex, (uv + float2(-1, 1) * delta)) * 1.0f;
				horizontal += tex2D(tex, (uv + float2(1, -1) * delta)) * -1.0f;
				horizontal += tex2D(tex, (uv + float2(1, 0) * delta)) * -2.0f;
				horizontal += tex2D(tex, (uv + float2(1, 1) * delta)) * -1.0f;
								  
				vert += tex2D(tex, (uv + float2(-1, -1) * delta)) * 1.0f;
				vert += tex2D(tex, (uv + float2(0, -1) * delta)) * 2.0f;
				vert += tex2D(tex, (uv + float2(1, -1) * delta)) * 1.0f;
				vert += tex2D(tex, (uv + float2(-1, 1) * delta)) * -1.0f;
				vert += tex2D(tex, (uv + float2(0, 1) * delta)) * -2.0f;
				vert += tex2D(tex, (uv + float2(1, 1) * delta)) * -1.0f;

				return saturate(20 * sqrt(horizontal * horizontal + vert * vert));
			}

			fixed4 frag (v2f i) : SV_Target
			{
				half h = 0;
				
				
				//fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//col.rgb = 1 - col.rgb;
				
				//float sb = sobel(_CameraDepthTexture, i.uv);
				//fixed4 col = fixed4(sb, sb, sb, 1);
				
				
				h = saturate(h);
				half4 color = tex2D(_HeatTex, fixed2(h, 0.5));
				return color;
			}
			ENDCG
		}
	}
			FallBack "Diffuse"
}
