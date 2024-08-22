using System;
using System.Collections;
using System.Collections.Generic;
using Infra;
using Infra.Patterns;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class VFXManager : TrueSingleton<VFXManager>
{
    // Random level shared
    [SerializeField, Tooltip("Add shader for level filter")]
    private GameObject levelFilter;
    private Volume _colorShader;
    private Bloom _shaderBloom;
    
    // Hit effect
    [SerializeField, Tooltip("Add shader for hit effect")]
    private GameObject hitEffect;
    private Volume _hitVolume;
    private Bloom _hitBloom;
    
    public bool IsHitEffectActive { get;  private set; }

    protected override void Awake()
    {
        base.Awake();
        
        _colorShader = levelFilter.GetComponent<Volume>();
        _colorShader.profile.TryGet(out _shaderBloom);
        _hitVolume = hitEffect.GetComponent<Volume>();
        _hitVolume.profile.TryGet<Bloom>(out _hitBloom);
    }

    private void Start()
    {
        ApplyRandomShader();
    }

    public IEnumerator GetHitLightEffect()
    {
        IsHitEffectActive = true;
        _hitVolume.enabled = true;
        _hitBloom.intensity.value = 8f;
        _hitBloom.scatter.value = 0.400f;
        yield return new WaitForSeconds(0.2f);
        _hitBloom.scatter.value = 0.125f;
        _hitBloom.intensity.value = 0.05f;
        yield return new WaitForSeconds(0.2f);
        IsHitEffectActive = false;
        _hitVolume.enabled = false;
    }
    
    private void ApplyRandomShader()
    {
        _colorShader.enabled = true;
        _colorShader.weight = 0.5f;
        _hitBloom.intensity.value = 2f;
        _shaderBloom.tint.value = Random.ColorHSV();
    }
}
