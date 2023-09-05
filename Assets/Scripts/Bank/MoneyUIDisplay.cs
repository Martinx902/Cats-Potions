using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUIDisplay : MonoBehaviour
{
    [SerializeField]
    private SO_Bank playersMoney;

    [SerializeField]
    private TextMeshProUGUI moneyTxt;

    // Update is called once per frame
    private void Update()
    {
        moneyTxt.text = playersMoney.money.ToString();
    }
}