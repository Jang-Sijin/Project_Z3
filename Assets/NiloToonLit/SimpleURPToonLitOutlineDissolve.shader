// For more information, visit -> https://github.com/ColinLeung-NiloCat/UnityURPToonLitShaderExample

/*
This shader is a simple and short example showing you how to write your first URP custom toon lit shader with "minimum" shader code.
You can use this shader as a starting point, add/edit code to develop your own custom toon lit shader for URP14(Unity2022.3) or above.

Usually, just by editing "SimpleURPToonLitOutlineExample_LightingEquation.hlsl" alone can control most of the visual result.

This shader includes 5 passes:
0.UniversalForwardOnly  pass    (this pass will always render to the _CameraColorAttachment* & _CameraDepthAttachment*)
1.Outline               pass    (this pass will always render to the _CameraColorAttachment* & _CameraDepthAttachment*)
2.ShadowCaster          pass    (only for URP's shadow caster rendering, render to the _MainLightShadowmapTexture* and _AdditionalLightsShadowmapTexture*. This pass won't render at all if your character don't cast shadow)
3.DepthOnly             pass    (only for URP's _CameraDepthTexture's rendering. This pass won't render at all if your project don't render URP's offscreen depth prepass)
4.DepthNormalsOnly      pass    (only for URP's _CameraDepthTexture + _CameraNormalsTexture's rendering. This pass won't render at all if your project don't render URP's offscreen depth+normal prepass)

- Because most of the time, you use this toon lit shader for unique dynamic characters, so all lightmap related code are removed for simplicity.
- For batching, we only rely on SRP batching, which is the most practical batching method in URP for rendering lots of unique animated SkinnedMeshRenderer characters using the same shader

Most of the properties will try to follow URP Lit shader's naming convention,
so switching your URP lit material's shader to this toon lit shader will preserve most of the original properties if defined in this shader.
For URP Lit shader's naming convention, see URP's Lit.shader.

In this shader, sometimes we choose "conditional move (a?b:c)" or "static uniform branching (if(_Uniform))" over "shader_feature & multi_compile" for some of the toggleable features, 
because:
    - we want to avoid this shader's build time takes too long (2^n)
    - we want to avoid shader size and memory usage becomes too large easily (2^n), 2GB memory iOS mobile will crash easily if you use too much multi_compile
    - we want to avoid rendering spike/hiccup when a new shader variant was seen by the camera first time ("create GPU program" in profiler)
    - we want to avoid increasing ShaderVariantCollection's keyword combination complexity
    - we want to avoid breaking SRP batching because SRP batching is per shader variant batching, not per shader

    All modern GPU(include the latest high-end mobile devices) can handle "static uniform branching" with "almost" no performance cost (if register pressure is not the bottleneck).
    Usually, there exist 4 cases of branching, here we sorted them by cost, from lowest cost to highest cost,
    and you usually only need to worry about the "case 4" only!

    case 1 - compile time constant if():
        // absolutely 0 performance cost for any platform, unity's shader compiler will treat the false side of if() as dead code and remove it completely
        // shader compiler is very good at dead code removal
        #define SHOULD_RUN_FANCY_CODE 0
        if(SHOULD_RUN_FANCY_CODE) {...}

    case 2 - static uniform branching if():
        // reasonable low performance cost (except OpenGLES2, OpenGLES2 doesn't have branching and will always run both paths and discard the false path)
        // since OpenGLES2 is not important anymore in 2024, we will use static uniform branching if() when suitable
        CBUFFER_START(UnityPerMaterial)
            float _ShouldRunFancyCode; // usually controlled by a [Toggle] in material inspector, or material.SetFloat(1 or 0) in C#
        CBUFFER_END
        if(_ShouldRunFancyCode) {...}

    case 3 - dynamic branching if() without divergence inside a wavefront/warp: 
        bool shouldRunFancyCode = (some shader calculation); // all pixels inside a wavefront/warp(imagine it is a group of 8x8 pixels) all goes into the same path, then no divergence.
        if(shouldRunFancyCode) {...}

    case 4 - dynamic branching if() WITH divergence inside a wavefront/warp: 
        // this is the only case that will make GPU really slow! You will want to avoid it as much as possible
        bool shouldRunFancyCode = (some shader calculation); // pixels inside a wavefront/warp(imagine it is a group of 8x8 pixels) goes into different paths, even it is 63 vs 1 within a 8x8 thread group, it is still divergence!
        if(shouldRunFancyCode) {...} 

    If you want to understand the difference between case 1-4,
    Here are some extra resources about the cost of if() / branching / divergence in shader:
    - https://stackoverflow.com/questions/37827216/do-conditional-statements-slow-down-shaders
    - https://stackoverflow.com/questions/5340237/how-much-performance-do-conditionals-and-unused-samplers-textures-add-to-sm2-3-p/5362006#5362006
    - https://twitter.com/bgolus/status/1235351597816795136
    - https://twitter.com/bgolus/status/1235254923819802626?s=20
    - https://www.shadertoy.com/view/wlsGDl?fbclid=IwAR1ByDhQBck8VO0AMPS5XpbtBPSzSN9Mh8clW4itRgDIpy5ROcXW1Iyf86g

    [TLDR] 
    Just remember(even for mobile platform): 
    - if() itself is not evil, you CAN use it if you know there is no divergence inside a wavefront/warp, still, it is not free on mobile.
    - "a ? b : c" is just a conditional move(movc / cmov) in assembly code, don't worry using it if you have calculated b and c already
    - Don't try to optimize if() or "a ? b : c" by replacing them by lerp(b,c,step())..., because "a ? b : c" is always faster if you have calculated b and c already
    - branching is not evil, still it is not free. Sometimes we can use branching to help GPU run faster if the skipped task is heavy!
    - but, divergence is evil! If you want to use if(condition){...}else{...}, make sure the "condition" is the same within as many groups of 8x8 pixels as possible

    [Note from the developer (1)]
    Using shader permutation(multi_compile/shader_feature) is still the fastest way to skip shader calculation,
    because once the code doesn't exist, it will enable many compiler optimizations. 
    If you need the best GPU performance, and you can accept long build time and huge memory usage, you can use multi_compile/shader_feature more, especially for features with texture read.

    NiloToonURP's character shader will always prefer shader permutation if it can skip any texture read, 
    because the GPU hardware has very strong ALU(pure calculation) power growth since 2015 (including mobile), 
    but relatively weak growth in memory bandwidth(usually means buffer/texture read).
    (https://community.arm.com/developer/tools-software/graphics/b/blog/posts/moving-mobile-graphics#siggraph2015)

    And when GPU is waiting for receiving texture fetch, it won't become idle, 
    GPU will still continue any available ALU work(latency hiding) until there is 100% nothing to calculate anymore, 
    also bandwidth is the biggest source of heat generation (especially on mobile without active cooling = easier overheat/thermal throttling). 
    So we should try our best to keep memory bandwidth usage low (since more ALU is ok, but more texture read is not ok),
    the easiest way is to remove texture read using shader permutation.

    But if the code is ALU only(pure calculation), and calculation is simple on both paths on the if & else side, NiloToonURP will prefer "a ? b : c". 
    The rest will be static uniform branching (usually means heavy ALU only code inside an if()).

    [Note from the developer (2)]
    If you are working on a game project, not a generic tool like NiloToonURP, you will always want to pack 4data (occlusion/specular/smoothness/any mask.....) into 1 RGBA texture(for fragment shader), 
    and pack 4data (outlineWidth/ZOffset/face area mask....) into another RGBA texture(for vertex shader), to reduce the number of texture read without changing visual result(if we ignore texture compression).
    But since NiloToonURP is a generic tool that is used by different person/team/company, 
    we know it is VERY important for all users to be able to apply NiloToon shader to any model easily/fast/without effort,
    and we know that it is almost not practical if we force regular user to pack their texture into a special format just for NiloToon shader,
    so we decided we will keep every texture separated, even it is VERY slow compared to the packed texture method.
    That is a sad decision in terms of performance, but a good decision for ease of use. 
    If user don't need the best performance, this decision is actually a plus to them since it is much more flexible when using this shader.  

    [About multi_compile or shader_feature's _vertex and _fragment suffixes]
    In unity 2020.3, unity added _vertex, _fragment suffixes to multi_compile and shader_feature
    https://docs.unity3d.com/2020.3/Documentation/Manual/SL-MultipleProgramVariants.html (Using stage-specific keyword directives)

    The only disadvantage of NOT using _vertex and _fragment suffixes is only compilation time, not build size/memory usage:
    https://docs.unity3d.com/2020.3/Documentation/Manual/SL-MultipleProgramVariants.html (Stage-specific keyword directives)
    "Unity identifies and removes duplicates afterwards, so this redundant work does not affect build sizes or runtime performance; 
    however, if you have a lot of stages and/or variants, the time wasted during shader compilation can be significant."

---------------------------------------------------------------------------
More information about mobile GPU optimization can be found here, most of the best practice can apply both GPU(Mali & Adreno):
https://developer.arm.com/solutions/graphics-and-gaming/arm-mali-gpu-training

[Shader build time and memory]
https://blog.unity.com/engine-platform/2021-lts-improvements-to-shader-build-times-and-memory-usage

[Support SinglePassInstancing]
https://docs.unity3d.com/2022.2/Documentation/Manual/SinglePassInstancing.html

[Conditionals can affect #pragma directives]
preprocessor conditionals can be used to influence, which #pragma directives are selected.
https://forum.unity.com/threads/new-shader-preprocessor.790328/
https://docs.unity3d.com/Manual/shader-variant-stripping.html
Example code:
{
    #ifdef SHADER_API_DESKTOP
        #pragma multi_compile _ RED GREEN BLUE WHITE
    #else
       #pragma shader_feature RED GREEN BLUE WHITE
    #endif
}
{
    #if SHADER_API_DESKTOP
        #pragma geometry ForwardPassGeometry
    #endif
}
{
    #if SHADER_API_DESKTOP
        #pragma vertex DesktopVert
    #else
        #pragma vertex MobileVert
    #endif
}
{
    #if SHADER_API_DESKTOP
       #pragma multi_compile SHADOWS_LOW SHADOWS_HIGH
       #pragma multi_compile REFLECTIONS_LOW REFLECTIONS_HIGH
       #pragma multi_compile CAUSTICS_LOW CAUSTICS_HIGH
    #elif SHADER_API_MOBILE
       #pragma multi_compile QUALITY_LOW QUALITY_HIGH
       #pragma shader_feature CAUSTICS // Uses shader_feature, so Unity strips variants that use CAUSTICS if there are no Materials that use the keyword at build time.
    #endif
}
But this will not work (Keywords coming from pragmas (shader_feature, multi_compile and variations) will not affect other pragmas.):
{
    #pragma shader_feature WIREFRAME_MODE_ON
 
    #ifdef WIREFRAME_MODE_ON
        #pragma geometry ForwardPassGeometry
    #endif
}

[Write .shader and .hlsl using an IDE]
Rider is the best IDE for writing shader in Unity, there should be no other better tool than Rider.
If you never used Rider to write hlsl before, we highly recommend trying it for a month for free.
https://www.jetbrains.com/rider/

[hlsl code is inactive in Rider]  
You may encounter an issue that some hlsl code is inactive within the #if #endif section, so Rider's "auto complete" and "systax highlightd" is not active, 
to solve this problem, please switch the context using the "Unity Shader Context picker in the status bar" UI at the bottom right of Rider

For details, see this Rider document about "Unity Shader Context picker in the status bar":
https://github.com/JetBrains/resharper-unity/wiki/Switching-code-analysis-context-for-hlsl-cginc-files-in-Rider
*/ 
Shader "SimpleURPToonLitDissolve(With Outline)"
{
     Properties
    {
        [Header(High Level Setting)]
        [ToggleUI]_IsFace("Is Face? (face/eye/mouth)", Float) = 0

        [Header(Base Color)]
        [MainTexture]_BaseMap("Base Map", 2D) = "white" {}
        [HDR][MainColor]_BaseColor("Base Color", Color) = (1,1,1,1)

        [Header(Alpha Clipping)]
        [Toggle(_UseAlphaClipping)]_UseAlphaClipping("Enable?", Float) = 0
        _Cutoff("Cutoff", Range(0.0, 1.0)) = 0.5

        [Header(Dissolve Effect)]
        _Noise("Noise Texture", 2D) = "white" {}
        _DissolveCutoff("Dissolve Amount", Range(0,1)) = 0.0
        _DissolveEdgeSize("Dissolve Edge Size", Range(0,1)) = 0.2
        _NoiseStrength("Noise Strength", Range(0,1)) = 0.4
        _DisplaceAmount("Displace Amount", Range(0,1)) = 0.4
        [HDR]_EdgeColor1("Dissolve Edge Color", Color) = (1,1,1,1)

        [Header(Outline)]
        _OutlineWidth("Width", Range(0,4)) = 1
        _OutlineColor("Color", Color) = (0.5,0.5,0.5,1)
    }
    
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }
        
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        // Texture Samplers
        TEXTURE2D(_BaseMap);        SAMPLER(sampler_BaseMap);
        TEXTURE2D(_Noise);          SAMPLER(sampler_Noise);
        
        // Properties
        float4 _BaseColor;
        float _Cutoff;
        float _DissolveCutoff;
        float _DissolveEdgeSize;
        float _NoiseStrength;
        float _DisplaceAmount;
        float4 _EdgeColor1;

        // Vertex Attributes
        struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float2 uv : TEXCOORD0;
        };

        // Varyings
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 positionWS : TEXCOORD1;
            float3 normalWS : TEXCOORD2;
        };

        // Vertex Shader with Dissolve Effect
        Varyings VertexShaderWork(Attributes input)
        {
            Varyings output;
            output.positionWS = TransformObjectToWorld(input.positionOS);
            output.uv = input.uv;
            output.normalWS = TransformObjectToWorldNormal(input.normalOS);
            
            // Noise-based Displacement
            float noiseValue = SAMPLE_TEXTURE2D_LOD(_Noise, sampler_Noise, output.positionWS.xy * 0.5, 0).r;
            float mask = smoothstep(_DissolveCutoff, _DissolveCutoff - 0.3, 1 - noiseValue);
            output.positionWS.xyz += output.normalWS * noiseValue * mask * _DissolveCutoff * _DisplaceAmount;

            output.positionCS = TransformWorldToHClip(output.positionWS);
            return output;
        }
        
        // Fragment Shader
        float4 ShadeFinalColor(Varyings input) : SV_Target
        {
            // Base Color
            float4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * _BaseColor;
            
            // Dissolve Effect
            float noiseValue = SAMPLE_TEXTURE2D(_Noise, sampler_Noise, input.uv).r;
            float dissolveEdge = smoothstep(_DissolveCutoff, _DissolveCutoff - _DissolveEdgeSize, 1 - noiseValue);
            float4 edgeHighlight = _EdgeColor1 * (1 - dissolveEdge);
        
            // Alpha Clipping
            clip(dissolveEdge - _DissolveCutoff);
        
            // Final Color
            float4 finalColor = baseColor;
            finalColor.rgb += edgeHighlight.rgb;
            return finalColor;
        }
        ENDHLSL

        // Forward Lit Pass
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForwardOnly" }

            Blend One Zero
            ZWrite On
            Cull Off
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 3.0
            #pragma vertex VertexShaderWork
            #pragma fragment ShadeFinalColor

            ENDHLSL
        }

        // Outline Pass
        Pass
        {
            Name "Outline"
            Tags {}

            Blend One Zero
            ZWrite On
            Cull Front
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 3.0
            #define ToonShaderIsOutline
            #pragma vertex VertexShaderWork
            #pragma fragment ShadeFinalColor

            ENDHLSL
        }

        // Shadow Caster Pass
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull Off

            HLSLPROGRAM
            #pragma target 3.0
            #pragma vertex VertexShaderWork
            #pragma fragment ShadeFinalColor
            #define ToonShaderApplyShadowBiasFix
            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}