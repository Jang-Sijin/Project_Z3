using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VfxPlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> effects;


    public void PlayVFX(int effectIndex)
    {
        ParticleSystem particleSystem = effects[effectIndex].GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    public void StopVFX(int effectIndex)
    {
        ParticleSystem particleSystem = effects[effectIndex].GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }
}