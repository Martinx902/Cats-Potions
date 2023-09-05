//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 04/05/2023

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BedInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject sleepCat;

    [SerializeField]
    private SO_GlobalTime globalTime;

    [SerializeField]
    private float allowedBedTime = 20;

    [SerializeField]
    private LevelLoader levelLoader;

    [SerializeField]
    private ParticleSystem sleepParticles;

    [SerializeField, Range(4f, 10f)]
    private float waitDelay;

    public UnityEvent onBedTime;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private float transitionTime;

    [SerializeField]
    private GameObject image;

    private void Awake()
    {
        image.SetActive(true);
    }

    public void Interact()
    {
        //Check if it's more than the allowed bed time

        if (globalTime.currentTime.Hour < allowedBedTime)
        {
            PopUpUI.instance.Show(PopUpType.NotTimeToGoToBedYet);
        }
        else
        {
            //Ask the player if they really want to go to sleep
            onBedTime.Invoke();
        }
    }

    public void ResetDay()
    {
        //Cinematic
        StartCoroutine(SleepTransition());

        //Coroutine to wait for the cinematic to end and then reset the day
        StartCoroutine(GoToBed());
    }

    private IEnumerator SleepTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        player.SetActive(false);
        transition.SetTrigger("End");
        sleepCat.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
    }

    private IEnumerator GoToBed()
    {
        //Zzz Particles
        sleepParticles.Play();

        yield return new WaitForSeconds(waitDelay);

        globalTime.ResetDayTime();

        levelLoader.LoadNextLevel(Scenes.HouseSecondFloor);
    }
}