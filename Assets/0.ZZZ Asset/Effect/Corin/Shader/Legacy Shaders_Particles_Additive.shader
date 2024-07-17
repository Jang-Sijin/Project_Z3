//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Legacy Shaders/Particles/Additive" {
Properties {
_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
_MainTex ("Particle Texture", 2D) = "white" { }
_InvFade ("Soft Particles Factor", Range(0.01, 3)) = 1
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
  Blend SrcAlpha One, SrcAlpha One
  ColorMask RGB 0
  ZWrite Off
  Cull Off
  GpuProgramID 27317
Program "vp" {
SubProgram "d3d11 " {
"// hash: 89592c9690bf2f6
cbuffer cb2 : register(b2)
{
  float4 cb2[21];
}

cbuffer cb1 : register(b1)
{
  float4 cb1[4];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[4];
}




// 3Dmigoto declarations
#define cmp -


void main(
  float4 v0 : POSITION0,
  float4 v1 : COLOR0,
  float2 v2 : TEXCOORD0,
  out float4 o0 : SV_POSITION0,
  out float4 o1 : COLOR0,
  out float2 o2 : TEXCOORD0)
{
  float4 r0,r1;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.xyzw = cb1[1].xyzw * v0.yyyy;
  r0.xyzw = cb1[0].xyzw * v0.xxxx + r0.xyzw;
  r0.xyzw = cb1[2].xyzw * v0.zzzz + r0.xyzw;
  r0.xyzw = cb1[3].xyzw + r0.xyzw;
  r1.xyzw = cb2[18].xyzw * r0.yyyy;
  r1.xyzw = cb2[17].xyzw * r0.xxxx + r1.xyzw;
  r1.xyzw = cb2[19].xyzw * r0.zzzz + r1.xyzw;
  o0.xyzw = cb2[20].xyzw * r0.wwww + r1.xyzw;
  o1.xyzw = v1.xyzw;
  o2.xy = v2.xy * cb0[3].xy + cb0[3].zw;
  return;
}"
}
SubProgram "d3d11 " {
Keywords { "SOFTPARTICLES_ON" }
"// hash: c64abd0856fff17b
cbuffer cb3 : register(b3)
{
  float4 cb3[21];
}

cbuffer cb2 : register(b2)
{
  float4 cb2[4];
}

cbuffer cb1 : register(b1)
{
  float4 cb1[6];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[4];
}




// 3Dmigoto declarations
#define cmp -


void main(
  float4 v0 : POSITION0,
  float4 v1 : COLOR0,
  float2 v2 : TEXCOORD0,
  out float4 o0 : SV_POSITION0,
  out float4 o1 : COLOR0,
  out float4 o2 : TEXCOORD0,
  out float4 o3 : TEXCOORD2)
{
  float4 r0,r1;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.xyzw = cb2[1].xyzw * v0.yyyy;
  r0.xyzw = cb2[0].xyzw * v0.xxxx + r0.xyzw;
  r0.xyzw = cb2[2].xyzw * v0.zzzz + r0.xyzw;
  r0.xyzw = cb2[3].xyzw + r0.xyzw;
  r1.xyzw = cb3[18].xyzw * r0.yyyy;
  r1.xyzw = cb3[17].xyzw * r0.xxxx + r1.xyzw;
  r1.xyzw = cb3[19].xyzw * r0.zzzz + r1.xyzw;
  r1.xyzw = cb3[20].xyzw * r0.wwww + r1.xyzw;
  o0.xyzw = r1.xyzw;
  o1.xyzw = v1.xyzw;
  o2.xy = v2.xy * cb0[3].xy + cb0[3].zw;
  r0.y = cb3[10].z * r0.y;
  r0.x = cb3[9].z * r0.x + r0.y;
  r0.x = cb3[11].z * r0.z + r0.x;
  r0.x = cb3[12].z * r0.w + r0.x;
  o3.z = -r0.x;
  r0.x = cb1[5].x * r1.y;
  r0.w = 0.5 * r0.x;
  r0.xz = float2(0.5,0.5) * r1.xw;
  o3.w = r1.w;
  o3.xy = r0.xw + r0.zz;
  return;
}"
}
}
Program "fp" {
SubProgram "d3d11 " {
"// hash: bc16f92fe723879d
Texture2D<float4> t0 : register(t0);

SamplerState s0_s : register(s0);

cbuffer cb0 : register(b0)
{
  float4 cb0[3];
}




// 3Dmigoto declarations
#define cmp -


void main(
  float4 v0 : SV_POSITION0,
  float4 v1 : COLOR0,
  float2 v2 : TEXCOORD0,
  out float4 o0 : SV_Target0)
{
  float4 r0,r1;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.xyzw = v1.xyzw + v1.xyzw;
  r0.xyzw = cb0[2].xyzw * r0.xyzw;
  r1.xyzw = t0.Sample(s0_s, v2.xy).xyzw;
  r0.xyzw = r1.xyzw * r0.xyzw;
  o0.w = saturate(r0.w);
  o0.xyz = r0.xyz;
  return;
}"
}
SubProgram "d3d11 " {
Keywords { "SOFTPARTICLES_ON" }
"// hash: a97487d1d5df3a2e
Texture2D<float4> t1 : register(t1);

Texture2D<float4> t0 : register(t0);

SamplerState s1_s : register(s1);

SamplerState s0_s : register(s0);

cbuffer cb1 : register(b1)
{
  float4 cb1[8];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[5];
}




// 3Dmigoto declarations
#define cmp -


void main(
  float4 v0 : SV_POSITION0,
  float4 v1 : COLOR0,
  float4 v2 : TEXCOORD0,
  float4 v3 : TEXCOORD2,
  out float4 o0 : SV_Target0)
{
  float4 r0,r1;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.xy = v3.xy / v3.ww;
  r0.xyzw = t0.Sample(s1_s, r0.xy).xyzw;
  r0.x = cb1[7].z * r0.x + cb1[7].w;
  r0.x = 1 / r0.x;
  r0.x = -v3.z + r0.x;
  r0.x = saturate(cb0[4].x * r0.x);
  r0.w = v1.w * r0.x;
  r0.xyz = v1.xyz;
  r0.xyzw = r0.xyzw + r0.xyzw;
  r0.xyzw = cb0[2].xyzw * r0.xyzw;
  r1.xyzw = t1.Sample(s0_s, v2.xy).xyzw;
  r0.xyzw = r1.xyzw * r0.xyzw;
  o0.w = saturate(r0.w);
  o0.xyz = r0.xyz;
  return;
}"
}
}
}
}
}