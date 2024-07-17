using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VfxPlayer : MonoBehaviour
{
    private VisualEffect vfx;
    [SerializeField] private List<VisualEffectAsset> vfxAsset;
    [SerializeField] private GameObject referenceBone;

    private void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    public void PlayVFX(int vfxIndex)
    {
        this.transform.position = referenceBone.transform.position;
        this.transform.rotation = referenceBone.transform.rotation;
        vfx.visualEffectAsset = vfxAsset[vfxIndex];
        vfx.Play();
    }
}
