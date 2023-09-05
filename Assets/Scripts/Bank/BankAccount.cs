//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 03/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccount : MonoBehaviour
{
    [SerializeField]
    private SO_Bank bankAccount;

    public int GetAccountBalance() => bankAccount.money;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
            AddFunds(200);
    }

    //Checks to see if the player has money to buy an item, if it has substract the money and accept the payment
    public void SubstractMoney(SO_Item itemPrice)
    {
        bankAccount.money -= itemPrice.itemPrice;

        if (bankAccount.money < 0)
        {
            bankAccount.money = 0;
        }
    }

    public bool CheckForFunds(int price)
    {
        if (bankAccount.money >= price)
        {
            return true;
        }
        else
            return false;
    }

    public void AddFunds(int bizum)
    {
        bankAccount.money += bizum;

        if (bankAccount.money < 0)
        {
            bankAccount.money = 0;
        }
    }
}