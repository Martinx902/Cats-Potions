using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private float minPitch = 0.8f;
    [SerializeField]
    private float maxPitch = 1.2f;
    private float speed = 1f;
    [SerializeField]
    private float timerSpeed = 0.5f;
    [SerializeField]
    private float walkSpeed = 1.5f;
    [SerializeField]
    private float runSpeed = 1.6f;
    private Vector3 input;
    [SerializeField]
    private float volume = 1f;

    private AudioSource audioSource;
    private bool isWalking = false;
    private bool isRunning = false;

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
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Quick input check
        if (input.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (isWalking)
        {
            if((Input.GetKey(KeyCode.LeftShift)))
            {
                isRunning = true;
                speed = runSpeed;
            }
            else
            {
                isRunning = false;
                speed = walkSpeed;
            }
        }

        if (isWalking)
        {
            timer += Time.deltaTime;

            if (timer >= timerSpeed / speed)
            {
                PlaySound();
                timer = 0;
            }
        }
    }

    void PlaySound()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(audioClip);
    }
}
