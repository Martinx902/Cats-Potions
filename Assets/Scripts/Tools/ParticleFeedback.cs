using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFeedback : MonoBehaviour, IParticlePlayer
{
    [SerializeField]
    private GameObject particleSystemPrefab;

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private SoundsFX sound = SoundsFX.None;

    private ParticleSystem particleSystemComponent;

    private GameObject particleSystemObject;

    private void Awake()
    {
        particleSystemObject = Instantiate(particleSystemPrefab, spawnPos.position, Quaternion.identity);

        particleSystemComponent = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystemComponent.Stop();

        particleSystemObject.transform.parent = this.transform;
        particleSystemObject.transform.localScale = Vector3.one;
    }

    public void PlayParticles()
    {
        if (sound != SoundsFX.None)
        {
            AudioManager.instance.PlayClip(sound);
        }

        if (spawnPos == null)
            spawnPos = this.transform;

        particleSystemComponent.Play(spawnPos);
    }
}