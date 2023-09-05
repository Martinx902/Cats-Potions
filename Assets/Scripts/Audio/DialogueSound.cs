using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private float basePitch = 1f;
    [SerializeField]
    private float pitchVariation = 0.1f;
    private float speed = 1f;
    [SerializeField]
    private float timerSpeed = 0.5f;
    [SerializeField]
    private float volume = 1f;

    private AudioSource audioSource;

    float timer = 0f;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerSpeed / speed)
        {
            PlaySound();
            timer = 0;
        }
    }

    void PlaySound()
    {
        audioSource.pitch = Random.Range(basePitch - pitchVariation, basePitch + pitchVariation);
        audioSource.PlayOneShot(audioClip);
    }
}
