Shader "MrPMorris/Universe sub section"
{
    Properties
    {
        _MainTex ("-", 2D) = ""{}
        _CenterOfUniverse("Center of universe", VECTOR) = (0, 0, 0)
        _UniverseRadius("Radius of the universe", float) = 2
        _FeatheringRadius("Additional size to feather out edges of universe", float) = 1
        _ChromaKey("Color in scene to render transparent", COLOR) = (0, 1, 0, 0.618)
        _ClipBelowGround("1 = don't render below universe center, other = render it", INT) = 1
        _BackgroundTex("Background texture", 2D) = ""{}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    sampler2D _BackgroundTex;
    half3 _CenterOfUniverse;
    float _UniverseRadius;
    float _FeatheringRadius;
    half4 _ChromaKey;
    int _ClipBelowGround;

    sampler2D_float _CameraDepthTexture;
    float4x4 _InverseView;

    fixed4 frag (v2f_img i) : SV_Target
    {
        const float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
        const float2 p13_31 = float2(unity_CameraProjection._13, unity_CameraProjection._23);
        const float isOrtho = unity_OrthoParams.w;
        const float near = _ProjectionParams.y;
        const float far = _ProjectionParams.z;

        half2 upsideDownUV = half2(i.uv.x, i.uv.y);
#if UNITY_UV_STARTS_AT_TOP
        upsideDownUV.y = 1 - upsideDownUV.y;
#endif

        float d = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, upsideDownUV);
#if defined(UNITY_REVERSED_Z)
        d = 1 - d;
#endif
        float zOrtho = lerp(near, far, d);
        float zPers = near * far / lerp(far, near, d);
        float vz = lerp(zPers, zOrtho, isOrtho);

        float3 vpos = float3((upsideDownUV * 2 - 1 - p13_31) / p11_22 * lerp(vz, 1, isOrtho), -vz);
        float4 wpos = mul(_InverseView, float4(vpos, 1));

        half4 source = tex2D(_MainTex, i.uv);
        bool forceBackground =
            (_ClipBelowGround == 1 && wpos.y < _CenterOfUniverse.y)
            || (source.r == _ChromaKey.r && source.g == _ChromaKey.g && source.b == _ChromaKey.b && source.a == _ChromaKey.a);

        if (forceBackground)
            return tex2D(_BackgroundTex, upsideDownUV);

        float dist = distance(float4(_CenterOfUniverse, 0), wpos);
        if (dist <= _UniverseRadius)
            return source;

        half4 backgroundPixel = tex2D(_BackgroundTex, upsideDownUV);
        if (dist <= _UniverseRadius + _FeatheringRadius)
        {
            float featherDistance = dist - _UniverseRadius;
            float opacity = (featherDistance / _FeatheringRadius);
            return lerp(source, backgroundPixel, opacity);

        }
        return backgroundPixel;
    }

    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}