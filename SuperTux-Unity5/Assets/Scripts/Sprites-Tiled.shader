//
// Diogo_RBG - https://github.com/diogorbg
//

Shader "Sprites/Default Tiled" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _border ("Sprite Border", Vector) = (0,0,1,1)
		[PerRendererData] _minMax ("MinMax Points", Vector) = (0,0,1,1)
		[PerRendererData] _scale ("Scale Factor", Vector) = (0,0,1,1)
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0

	}

	SubShader {
		Tags { 
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

		Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
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
			
			fixed4 _Color;
			float4 _border;
			float4 _minMax;
			float4 _scale;

			v2f vert(appdata_t IN) {
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			float invLerp (float a, float b, float x) {
				return (x-a) / (b-a);
			}

			fixed4 SampleSpriteTexture (float2 uv) {

				//- o shader é só isso ! --------------------------------------------------------------
				_scale.z = _border.z-_border.x;
				_scale.w = _border.w-_border.y;
				if (uv.x < _minMax.x)
					uv.x = uv.x * _border.x / _minMax.x;
				else if (uv.x > _minMax.z)
					uv.x = lerp(_border.z, 1, invLerp(_minMax.z, 1, uv.x) );
				else
					uv.x = _border.x + fmod( (uv.x-_minMax.x) * _scale.x , _scale.z);
				if (uv.y < _minMax.y)
					uv.y = uv.y * _border.y / _minMax.y;
				else if (uv.y > _minMax.w)
					uv.y = lerp(_border.w, 1, invLerp(_minMax.w, 1, uv.y) );
				else
					uv.y = _border.y + fmod( (uv.y-_minMax.y) * _scale.y , _scale.w);
				fixed4 color = tex2D (_MainTex, uv);
				//- e acaba aqui ! --------------------------------------------------------------------

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target {
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
