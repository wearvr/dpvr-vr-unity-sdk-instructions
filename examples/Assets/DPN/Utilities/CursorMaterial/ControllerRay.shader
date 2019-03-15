﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/************************************************************************************

Copyright   :   Copyright 2017 DeePoon LLC. All Rights reserved.

Deepoon Developer Website: http://developer.deepoon.com

************************************************************************************/
Shader "DeePoon/ControllerRay"
{
	Properties
	{
		_Color("Color", Color) = (0, 0, 0, 0.2)
		_Length("Length", Float) = 1.0
	}
	SubShader
	{
		Tags{ "Queue" = "Overlay" }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			AlphaTest Off
			Cull Off
			Lighting Off
			ZWrite Off
			ZTest Always

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform float4 _Color;
			uniform float _Length;

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float alpha : COLOR4;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.alpha = - v.vertex.z * 0.1;
				v.vertex.z *= _Length;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 ret = _Color;
				ret.a = i.alpha;
				return ret;
			}
			ENDCG
		}
	}
}
