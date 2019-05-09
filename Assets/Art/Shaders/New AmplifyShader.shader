// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WaterTest"
{
	Properties
	{
		_Texture0("Texture 0", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			half filler;
		};

		uniform sampler2D _Texture0;
		uniform float4 _Texture0_ST;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_1 = 10;
			return temp_cast_1;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 uv_Texture0 = v.texcoord * _Texture0_ST.xy + _Texture0_ST.zw;
			float3 tex2DNode14 = UnpackNormal( tex2Dlod( _Texture0, float4( uv_Texture0, 0, 0.0) ) );
			float3 temp_cast_0 = (tex2DNode14.r).xxx;
			float3 break3_g1 = temp_cast_0;
			float mulTime4_g1 = _Time.y * 2.0;
			float3 appendResult11_g1 = (float3(break3_g1.x , ( break3_g1.y + ( sin( ( ( break3_g1.x * ( sin( unity_DeltaTime.x ) * 1.0 ) ) + mulTime4_g1 ) ) * 1 ) ) , break3_g1.z));
			v.vertex.xyz += appendResult11_g1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
-194.4;341.6;1524;643;2537.322;93.82807;2.369529;True;True
Node;AmplifyShaderEditor.DeltaTime;7;-1325.299,462.6412;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;12;-1528.253,127.1114;Float;True;Property;_Texture0;Texture 0;0;0;Create;True;0;0;False;0;dd2fd2df93418444c8e280f1d34deeb5;None;True;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1053.599,430.1412;Float;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;6;-1026.298,324.8419;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1204.154,74.11127;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;10;-745.4986,489.9411;Float;False;Constant;_Int0;Int 0;0;0;Create;True;0;0;False;0;1;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-838.9987,341.6416;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;4;-393.3537,242.8113;Float;False;Waving Vertex;-1;;1;872b3757863bb794c96291ceeebfb188;0;3;1;FLOAT3;0,0,0;False;12;FLOAT;0;False;13;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;5;-1250.055,-140.6888;Float;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-773.5542,-110.6887;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.IntNode;11;-146.8866,711.0636;Float;False;Constant;_Int1;Int 1;0;0;Create;True;0;0;False;0;10;0;0;1;INT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;WaterTest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;7;1
WireConnection;14;0;12;0
WireConnection;8;0;6;0
WireConnection;8;1;9;0
WireConnection;4;1;14;1
WireConnection;4;12;8;0
WireConnection;4;13;10;0
WireConnection;15;0;5;0
WireConnection;15;1;14;0
WireConnection;0;11;4;0
WireConnection;0;14;11;0
ASEEND*/
//CHKSM=CE5CEB4B49444E2BAECDA508A60BEF6CF670D7DC