using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdviceController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SO_AdviceList adviceList;

    [SerializeField]
    private SO_DailyAdvice dailyAdviceHolder;

    public UnityEvent onAdviceInteraction;

    public void Interact()
    {
        onAdviceInteraction.Invoke();
        Debug.Log("interacting");
    }

    private void GetDailyAdvice()
    {
        //Get a random index number

        SO_Advice temp;
        float randomSurvivalPercentage = 0;
        float adviceSurvivalPercentage = 0;
        int tries = 0;

        do
        {
            int randomAdviceIndex = Random.Range(0, adviceList.adviceList.Count);

            temp = adviceList.adviceList[randomAdviceIndex];

            randomSurvivalPercentage = Random.Range(0, 5f);

            adviceSurvivalPercentage = Random.Range(0.1f, 1f) * temp.weight;

            tries++;
        }
        while (adviceSurvivalPercentage < randomSurvivalPercentage || tries <= 3);

        dailyAdviceHolder.actualAdvice = temp;

        //Checks to see if the actual advice is not the same as the last one

        if (dailyAdviceHolder.lastAdvice == null)
            dailyAdviceHolder.lastAdvice = dailyAdviceHolder.actualAdvice;
        else
        {
            if (dailyAdviceHolder.actualAdvice == dailyAdviceHolder.lastAdvice)
            {
                GetDailyAdvice();
            }

            dailyAdviceHolder.lastAdvice = dailyAdviceHolder.actualAdvice;
        }

        Debug.Log(dailyAdviceHolder.actualAdvice);
    }

    private void OnEnable()
    {
        GameManager.nextDayDelegate += GetDailyAdvice;
    }

    private void OnDisable()
    {
        GameManager.nextDayDelegate -= GetDailyAdvice;
    }
}