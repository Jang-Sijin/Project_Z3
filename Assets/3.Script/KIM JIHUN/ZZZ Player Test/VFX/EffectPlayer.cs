using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> effects;
    [SerializeField] private List<GameObject> particles;

    public void PlayEffect(int effectIndex)
    {
        VisualEffect particleSystem = effects[effectIndex].GetComponentInChildren<VisualEffect>();
        particleSystem.Play();
    }

    public void StopEffect(int effectIndex)
    {
        VisualEffect particleSystem = effects[effectIndex].GetComponentInChildren<VisualEffect>();
        particleSystem.Stop();
    }

    public void PlayParticle(int particleIndex)
    {
        particles[particleIndex].GetComponent<ParticleSystem>().Play();
    }

    public void StopParticle(int particleIndex)
    {
        particles[particleIndex].GetComponent<ParticleSystem>().Stop();
    }
}