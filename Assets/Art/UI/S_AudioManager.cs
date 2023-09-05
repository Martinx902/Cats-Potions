using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class S_AudioManager : MonoBehaviour
{
    public static S_AudioManager instance { get; private set; }

    private AudioSource hoverAS;
    private AudioSource pressedAS;
    private AudioSource music;
    public AudioClip buttonHoverClip;
    public AudioClip buttonPressedClip;
    public AudioClip musicClip;

    [SerializeField]
    private float hoverVolume = 1;

    [SerializeField]
    private float pressedVolume = 1;

    [SerializeField]
    private float minPitch = 0.8f;

    [SerializeField]
    private float maxPitch = 1.2f;

    [SerializeField]
    private bool playMusic = true;

    private void Awake()
    {
        hoverAS = this.AddComponent<AudioSource>();
        pressedAS = this.AddComponent<AudioSource>();
        music = GetComponent<AudioSource>();

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        hoverAS.clip = buttonHoverClip;
        pressedAS.clip = buttonPressedClip;
        hoverAS.volume = hoverVolume;
        pressedAS.volume = pressedVolume;
        music.clip = musicClip;
        music.loop = true;

        if (playMusic)
            music.Play();
    }

    public void PlayHover()
    {
        hoverAS.pitch = Random.Range(minPitch, maxPitch);
        hoverAS.Play();
    }

    public void PlayPressed()
    {
        pressedAS.pitch = Random.Range(minPitch, maxPitch);
        pressedAS.Play();
    }
}