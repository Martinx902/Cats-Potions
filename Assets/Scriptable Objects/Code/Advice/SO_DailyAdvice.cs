using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Advice / Daily Advice"))]
public class SO_DailyAdvice : ScriptableObject
{
    public SO_Advice lastAdvice;

    public SO_Advice actualAdvice;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}