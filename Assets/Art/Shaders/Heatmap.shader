Shader "Custom/Heatmap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		
	}
	SubShader
	{
		Tags { "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha //Alpha Blend

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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				fixed3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			uniform int _DataPointCount = 0;
			uniform float3 _DataPoints[100];
			uniform float2 _Properties[100];

			fixed4 frag (v2f i) : COLOR
			{
				/*half h = 0;
				for (int it = 0; it < _DataPointCount; it++)
				{
					half di = distance(i.worldPos, _DataPoints[i].xyz);
					half ri = _Properties[i].x;
					half hi = 1 - saturate(di / ri);

					h += hi * _Properties[i].y;
				}*/






				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
