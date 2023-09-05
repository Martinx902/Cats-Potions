using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAdviceDisplay : MonoBehaviour
{
    [SerializeField]
    private SO_DailyAdvice dailyAdvice;

    [SerializeField]
    private TextMeshProUGUI adviceTitle;

    [SerializeField]
    private TextMeshProUGUI adviceBody;

    private void OnEnable()
    {
        if (dailyAdvice.actualAdvice == null)
            return;

        adviceBody.text = dailyAdvice.actualAdvice.advice;
        adviceTitle.text = dailyAdvice.actualAdvice.adviceTitle;
    }
}