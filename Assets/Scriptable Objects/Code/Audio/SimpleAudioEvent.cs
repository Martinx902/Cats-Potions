//Martin Pérez Villabrille
//Cat & Potions 
//Last Modification 07/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Audio Event/Simple Audio Event")]
public class SimpleAudioEvent : AudioEvent
{
    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    [Range(0f, 2f)]
    private float volume = 0.5f;

    [SerializeField]
    [Range(0f, 2f)]
    private float pitch = 0.5f;

    public override void Play(AudioSource source)
    {
        if (clips.Length == 0)
            return;

        source.clip = clips[Random.Range(0,clips.Length)];
        source.volume = volume;
        source.pitch = pitch;   
        source.Play();
    }
}
