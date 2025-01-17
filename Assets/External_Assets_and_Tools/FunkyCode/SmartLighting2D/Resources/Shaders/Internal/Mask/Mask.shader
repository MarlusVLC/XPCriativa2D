﻿Shader "Light2D/Internal/Mask" {

	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
	
			#include "UnityCG.cginc"
		
			sampler2D _MainTex;
			//float4 _SecTex_ST;

			struct appdata_t {
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
		
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 color = fixed4(1, 1, 1, tex2D(_MainTex, IN.texcoord).a);
				
				if (color.a < 0.75) {
					color.a = 0;
				}

				color *= IN.color;
				color.rgb *= color.a;
		
				return color;
			}

			ENDCG
		}
	}
}