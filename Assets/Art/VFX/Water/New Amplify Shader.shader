// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader " "
{
	Properties
	{
		_Deep("Deep", Color) = (0,0.03798892,0.1320755,0)
		_Shallow("Shallow", Color) = (0,1,0.8622582,0)
		_Waternormal("Waternormal", 2D) = "bump" {}
		_SpeedX("SpeedX", Vector) = (1,0,0,0)
		_SpeedY("SpeedY", Vector) = (0,1,0,0)
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metalness("Metalness", Range( 0 , 1)) = 0
		_UVTiling("UVTiling", Vector) = (1,1,0,0)
		_CausticTiling("CausticTiling", Vector) = (1,1,0,0)
		_Caustics("Caustics", 2D) = "white" {}
		_Distortion("Distortion", Range( 0 , 0.1)) = 0.1619385
		_DepthBlend("DepthBlend", Range( 1 , 50)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
		#pragma surface surf Standard alpha:fade keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Waternormal;
		uniform float2 _SpeedY;
		uniform float2 _UVTiling;
		uniform float2 _SpeedX;
		uniform sampler2D _GrabTexture;
		uniform float _Distortion;
		uniform float4 _Shallow;
		uniform sampler2D _Caustics;
		uniform float2 _CausticTiling;
		uniform float4 _Deep;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _DepthBlend;
		uniform float _Metalness;
		uniform float _Smoothness;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord30 = i.uv_texcoord * _UVTiling;
			float2 panner31 = ( _Time.y * _SpeedY + uv_TexCoord30);
			float2 panner32 = ( _Time.y * _SpeedX + uv_TexCoord30);
			float3 temp_output_36_0 = BlendNormals( UnpackNormal( tex2D( _Waternormal, panner31 ) ) , UnpackNormal( tex2D( _Waternormal, panner32 ) ) );
			o.Normal = temp_output_36_0;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 appendResult66 = (float2(ase_screenPosNorm.x , ase_screenPosNorm.y));
			float4 screenColor73 = tex2D( _GrabTexture, ( float3( ( appendResult66 / ase_screenPosNorm.w ) ,  0.0 ) + ( _Distortion * temp_output_36_0 ) ).xy );
			float2 uv_TexCoord86 = i.uv_texcoord * _CausticTiling;
			float2 panner85 = ( 2.0 * _Time.y * _SpeedY + uv_TexCoord86);
			float2 panner87 = ( 2.0 * _Time.y * _SpeedX + uv_TexCoord86);
			float screenDepth78 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float distanceDepth78 = abs( ( screenDepth78 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthBlend ) );
			float clampResult48 = clamp( abs( distanceDepth78 ) , 0.0 , 1.0 );
			float4 lerpResult26 = lerp( ( _Shallow + CalculateContrast(1.2,( tex2D( _Caustics, panner85 ) * tex2D( _Caustics, sin( panner87 ) ) )) ) , _Deep , clampResult48);
			float4 lerpResult71 = lerp( screenColor73 , lerpResult26 , clampResult48);
			o.Albedo = lerpResult71.rgb;
			o.Metallic = _Metalness;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
-1529.6;0.8;1524;796;469.0798;224.1299;1;True;True
Node;AmplifyShaderEditor.Vector2Node;88;-3002.104,564.3829;Float;False;Property;_CausticTiling;CausticTiling;8;0;Create;True;0;0;False;0;1,1;100,100;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;35;-2736.101,952.7826;Float;False;Property;_SpeedX;SpeedX;3;0;Create;True;0;0;False;0;1,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;86;-2694.68,553.3801;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;43;-3273.532,-366.8945;Float;False;Property;_UVTiling;UVTiling;7;0;Create;True;0;0;False;0;1,1;40,40;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;34;-2803.849,-90.3363;Float;False;Property;_SpeedY;SpeedY;4;0;Create;True;0;0;False;0;0,1;0,0.09;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;33;-2172.938,441.9241;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;87;-2234.165,717.4036;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-2914.117,-352.0678;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;32;-2215.347,-3.052848;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-1907.715,-376.5759;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SinOpNode;93;-2006.75,814.3984;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;85;-2250.139,510.2204;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;61;-979.9377,-800.4714;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;41;-1758.675,653.9813;Float;True;Property;_TextureSample2;Texture Sample 2;9;0;Create;True;0;0;False;0;None;9fbef4b79ca3b784ba023cb1331520d5;True;0;False;white;Auto;False;Instance;40;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;40;-1803.853,356.508;Float;True;Property;_Caustics;Caustics;9;0;Create;True;0;0;False;0;None;9fbef4b79ca3b784ba023cb1331520d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;-1031.882,-247.7877;Float;True;Property;_Waternormal;Waternormal;2;0;Create;True;0;0;False;0;None;dd2fd2df93418444c8e280f1d34deeb5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-1009.747,19.55585;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Instance;27;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;80;-1434.271,1239.974;Float;False;Property;_DepthBlend;DepthBlend;11;0;Create;True;0;0;False;0;1;10.6;1;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;70;-889.9075,-618.9683;Float;False;Property;_Distortion;Distortion;10;0;Create;True;0;0;False;0;0.1619385;0.0373;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;78;-1050.01,1089.979;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1436.497,489.9389;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;36;-545.793,-23.9549;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;66;-724.6748,-828.6205;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-588.7379,-564.7214;Float;True;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;84;-684.2404,613.3127;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;1.2;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;25;-1055.83,297.3523;Float;False;Property;_Shallow;Shallow;1;0;Create;True;0;0;False;0;0,1,0.8622582,0;0.245194,0.3584906,0.2616735,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;67;-497.176,-731.7809;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;22;-686.2811,1392.254;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;83;-396.3734,254.2487;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;24;-896.7977,883.4877;Float;False;Property;_Deep;Deep;0;0;Create;True;0;0;False;0;0,0.03798892,0.1320755,0;0.1830723,0.1900651,0.2075472,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;68;-322.2504,-621.3871;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;48;307.7958,1140.157;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;73;-111.7392,-616.0679;Float;False;Global;_GrabScreen0;Grab Screen 0;7;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;26;-124.7652,627.7722;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;71;174.5695,-368.6207;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.4150943;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-189.1529,139.816;Float;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-172.42,43.15973;Float;False;Property;_Metalness;Metalness;6;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;620.7043,-15.9585;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard; ;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;False;True;False;False;False;False;False;False;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;86;0;88;0
WireConnection;87;0;86;0
WireConnection;87;2;35;0
WireConnection;30;0;43;0
WireConnection;32;0;30;0
WireConnection;32;2;35;0
WireConnection;32;1;33;0
WireConnection;31;0;30;0
WireConnection;31;2;34;0
WireConnection;31;1;33;0
WireConnection;93;0;87;0
WireConnection;85;0;86;0
WireConnection;85;2;34;0
WireConnection;41;1;93;0
WireConnection;40;1;85;0
WireConnection;27;1;31;0
WireConnection;29;1;32;0
WireConnection;78;0;80;0
WireConnection;42;0;40;0
WireConnection;42;1;41;0
WireConnection;36;0;27;0
WireConnection;36;1;29;0
WireConnection;66;0;61;1
WireConnection;66;1;61;2
WireConnection;69;0;70;0
WireConnection;69;1;36;0
WireConnection;84;1;42;0
WireConnection;67;0;66;0
WireConnection;67;1;61;4
WireConnection;22;0;78;0
WireConnection;83;0;25;0
WireConnection;83;1;84;0
WireConnection;68;0;67;0
WireConnection;68;1;69;0
WireConnection;48;0;22;0
WireConnection;73;0;68;0
WireConnection;26;0;83;0
WireConnection;26;1;24;0
WireConnection;26;2;48;0
WireConnection;71;0;73;0
WireConnection;71;1;26;0
WireConnection;71;2;48;0
WireConnection;0;0;71;0
WireConnection;0;1;36;0
WireConnection;0;3;38;0
WireConnection;0;4;37;0
ASEEND*/
//CHKSM=78A251D541A9B4D1C0AB592C455175D2C40A95AF