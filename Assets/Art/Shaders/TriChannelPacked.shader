// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TriplanarChannelPacked_Shader"
{
	Properties
	{
		_BaseColor("BaseColor", 2D) = "white" {}
		[Normal]_Normal("Normal", 2D) = "bump" {}
		_AO("AO", 2D) = "bump" {}
		_Tiling("Tiling", Float) = 1
		_ColorMultiply("ColorMultiply", Color) = (1,1,1,0)
		_TilingAo("TilingAo", Float) = 0
		_TilingNrml("TilingNrml", Float) = 0
		_roughness("roughness", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _Normal;
		uniform float _TilingNrml;
		uniform sampler2D _BaseColor;
		uniform float _Tiling;
		uniform float4 _ColorMultiply;
		uniform float _roughness;
		uniform sampler2D _AO;
		uniform float _TilingAo;


		inline float3 TriplanarSamplingSNF( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float tilling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= projNormal.x + projNormal.y + projNormal.z;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = ( tex2D( topTexMap, tilling * worldPos.zy * float2( nsign.x, 1.0 ) ) );
			yNorm = ( tex2D( topTexMap, tilling * worldPos.xz * float2( nsign.y, 1.0 ) ) );
			zNorm = ( tex2D( topTexMap, tilling * worldPos.xy * float2( -nsign.z, 1.0 ) ) );
			xNorm.xyz = half3( UnpackNormal( xNorm ).xy * float2( nsign.x, 1.0 ) + worldNormal.zy, worldNormal.x ).zyx;
			yNorm.xyz = half3( UnpackNormal( yNorm ).xy * float2( nsign.y, 1.0 ) + worldNormal.xz, worldNormal.y ).xzy;
			zNorm.xyz = half3( UnpackNormal( zNorm ).xy * float2( -nsign.z, 1.0 ) + worldNormal.xy, worldNormal.z ).xyz;
			return normalize( xNorm.xyz * projNormal.x + yNorm.xyz * projNormal.y + zNorm.xyz * projNormal.z );
		}


		inline float4 TriplanarSamplingSF( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float tilling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= projNormal.x + projNormal.y + projNormal.z;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = ( tex2D( topTexMap, tilling * worldPos.zy * float2( nsign.x, 1.0 ) ) );
			yNorm = ( tex2D( topTexMap, tilling * worldPos.xz * float2( nsign.y, 1.0 ) ) );
			zNorm = ( tex2D( topTexMap, tilling * worldPos.xy * float2( -nsign.z, 1.0 ) ) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 localTangent = mul( unity_WorldToObject, float4( ase_worldTangent, 0 ) );
			float3 localBitangent = mul( unity_WorldToObject, float4( ase_worldBitangent, 0 ) );
			float3 localNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float3x3 objectToTangent = float3x3(localTangent, localBitangent, localNormal);
			float3 localPos = mul( unity_WorldToObject, float4( ase_worldPos, 1 ) );
			float3 triplanar50 = TriplanarSamplingSNF( _Normal, localPos, localNormal, 1.0, _TilingNrml, 1.0, 0 );
			float3 tanTriplanarNormal50 = mul( objectToTangent, triplanar50 );
			o.Normal = tanTriplanarNormal50;
			float4 triplanar44 = TriplanarSamplingSF( _BaseColor, localPos, localNormal, 1.0, _Tiling, 1.0, 0 );
			float3 temp_output_52_0 = (triplanar44).xyz;
			float temp_output_2_0_g1 = _ColorMultiply.r;
			float temp_output_3_0_g1 = ( 1.0 - temp_output_2_0_g1 );
			float3 appendResult7_g1 = (float3(temp_output_3_0_g1 , temp_output_3_0_g1 , temp_output_3_0_g1));
			o.Albedo = ( ( temp_output_52_0 * temp_output_2_0_g1 ) + appendResult7_g1 );
			o.Smoothness = ( ( 1.0 - triplanar44.w ) * _roughness );
			float4 triplanar51 = TriplanarSamplingSF( _AO, ase_worldPos, ase_worldNormal, 1.0, _TilingAo, 1.0, 0 );
			o.Occlusion = triplanar51.x;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
-1529.6;84.8;1523;636;1887.308;456.8041;2.043925;True;True
Node;AmplifyShaderEditor.RangedFloatNode;56;-1352,-154;Float;False;Property;_Tiling;Tiling;3;0;Create;True;0;0;False;0;1;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;45;-1288.422,-462.3536;Float;True;Property;_BaseColor;BaseColor;0;0;Create;True;0;0;False;0;None;fda6352a99d6ac94db2d8da5300dde1c;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TriplanarNode;44;-614.6832,-409.8385;Float;True;Spherical;Object;False;Top Texture 0;_TopTexture0;white;-1;None;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Triplanar Sampler;False;9;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;8;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;52;-158.6416,-309.2832;Float;False;True;True;True;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;48;-924.7693,103.6074;Float;True;Property;_AO;AO;2;0;Create;True;0;0;False;0;None;3ba2a2909ee14984489d07661fb753e0;False;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ColorNode;58;-472.197,-183.1024;Float;False;Property;_ColorMultiply;ColorMultiply;4;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;64;-96.73578,147.8315;Float;False;Property;_roughness;roughness;7;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-1369.321,71.23592;Float;False;Property;_TilingAo;TilingAo;5;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-1345.981,-26.21655;Float;False;Property;_TilingNrml;TilingNrml;6;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;47;-911.6172,-134.7715;Float;True;Property;_Normal;Normal;1;1;[Normal];Create;True;0;0;False;0;None;1037f4d504e9c8140b7faba3e2555c94;True;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.OneMinusNode;53;-31.56281,-37.54004;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;62;119.8691,-194.8889;Float;False;214.8;182.2;Scene 1:  Schrijfkamer Johann en zweder kasteel Culemborg  luide trompetstoot luid de aanval van jan van buren in  Luid geschreeuw van buiten, Verward kijken ze in het rond.   Ze overleggen de val van Culemborg en hun vervolgstappen  luid geschreeuw verandert in luid gejuig.  Visvrouwen komen bebloed binnen de stukken lichaam van Jan van buren dragend in hun blauwe schorten. De 2 staan verbaast te kijken terwijl hun wordt uitgelegd wat er gebeurd is   Verbazing verandert in vreugde.   Scene 2:  Vanaf de dijk beschiet de bisschop van Diepholt op het kasteel met een kanon en katapulten.   In het opperhof Johan en zweder staan in paniek mensen aan te sturen, om hun heen worden stukken kasteel kapot geschoten gruis en stenen vallen van de muren.   “Screenshake intensiveert”   Er worden ook dode dieren/ mensen naar binnen geschoten.  Een van de stallen begint te branden. Er wordt een waterslinger gevormd naar de putten.  Emmers worden overgeheveld naar de brandende locatie.   Iemand(de speler? Johan of zweder?)  Gaat de stallen in om de paarden te redden De paarden worden naar de binnenplaats geleid.  Van buiten het kasteel klinkt gejuich.              Scene 3:  Grote zaal 1428 18 maart:   Johan van Culemborg verkondigd de overwinning over utrecht.   Zijn open brief aan het volk wordt door hem voorgelezen.  Gevangen genomen utrechters staan in het midden van de grote zaal waar ze uitgejouwd worden door omstanders. Waarvan een aantal visvrouwen met blauwe schorten.  Zijn broer Zweder staat naast hem met zijn bischopsstaf.   De festiviteiten worden geopend.   Scene 4:   Grote zaal:  Een aanval op de muur van het kasteel wordt geopend, De soldaten klimmen op de muren terwijl de verdedigers brandende olie van de muren afgieten.   De kanonnen openen vuur op de muren. Door het kasteel worden spullen veilig gesteld. Lontslot musketten worden afgeschoten door de vuur openingen in de muur.  Johan trekt kalm zijn wapenrusting aan.   Hij plant met Zweder het binnenbrengen van een nieuw kanon via 1 van de ondergrondse geheime gangen.    Scene 5:   Kelder kasteel:   Achter een haard in de kelder bevind zich een geheime deur, Johan begeleid een aantal soldaten die een kanonsloop en andere onderdelen dragen.   Ze proberen zo voorzichtig mogelijk dit kanon via de kelder de trap op te krijgen.   Er wordt nog een boodschapper met een hoeveelheid geld teruggestuurd om het laatste gedeelte van de betaling te maken.  ;1;57;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;77.26422,61.83148;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;65;128.733,-273.879;Float;False;Lerp White To;-1;;1;047d7c189c36a62438973bad9d37b1c2;0;2;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TriplanarNode;50;-480.7474,135.555;Float;True;Spherical;Object;True;Top Texture 1;_TopTexture1;white;-1;None;Mid Texture 1;_MidTexture1;white;-1;None;Bot Texture 1;_BotTexture1;white;-1;None;Triplanar Sampler;False;9;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;8;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TriplanarNode;51;-360.9058,401.5105;Float;True;Spherical;World;False;Top Texture 2;_TopTexture2;white;-1;None;Mid Texture 2;_MidTexture2;white;-1;None;Bot Texture 2;_BotTexture2;white;-1;None;Triplanar Sampler;False;9;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;8;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;169.8691,-144.8889;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;275.026,104.2135;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;TriplanarChannelPacked_Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;44;0;45;0
WireConnection;44;3;56;0
WireConnection;52;0;44;0
WireConnection;53;0;44;4
WireConnection;63;0;53;0
WireConnection;63;1;64;0
WireConnection;65;1;52;0
WireConnection;65;2;58;0
WireConnection;50;0;47;0
WireConnection;50;3;60;0
WireConnection;51;0;48;0
WireConnection;51;3;61;0
WireConnection;57;0;52;0
WireConnection;57;1;58;0
WireConnection;0;0;65;0
WireConnection;0;1;50;0
WireConnection;0;4;63;0
WireConnection;0;5;51;0
ASEEND*/
//CHKSM=7B4B8512168388CA27CF9A2E653629217A9B1A7A