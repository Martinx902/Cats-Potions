//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 17/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    //Banco del jugador
    private BankAccount playerBankAccount;

    //Inventario del jugador
    public ItemCustomEvent OnPurchase;

    //Dialogo del zorro
    private ITalkable shopDialogueTrigger;

    private bool canBuy = false;

    //Monedas del jugador UI
    [SerializeField]
    private TextMeshProUGUI currentPlayerFundsUI;

    [SerializeField]
    private Inventory playerInventory;

    private void Awake()
    {
        shopDialogueTrigger = GetComponent<ITalkable>();
        playerBankAccount = FindObjectOfType<BankAccount>();
    }

    private void Start()
    {
        //Display of the first day
        DisplayCurrentBankFunds();
    }

    public void ManagePurchase(UIShopItem itemToPurchase)
    {
        //Check if the item exits
        if (itemToPurchase == null)
            return;

        //Check if it hasn't been bought yet this day
        if (itemToPurchase.hasBeenBought)
        {
            Debug.Log("Item ya comprado");
            canBuy = false;
            return;
        }

        //Check to see if the player has money to buy the item
        if (playerBankAccount.CheckForFunds(itemToPurchase.ReturnItem().itemPrice) && !playerInventory.InventoryFull())
        {
            //Audio Feedback
            AudioManager.instance.PlayClip(SoundsFX.SFX_CashRegister);

            //Buy the item
            itemToPurchase.ItemBought();

            //Notify the user
            canBuy = true;

            //Send the event
            OnPurchase.Invoke(itemToPurchase.ReturnItem());

            playerBankAccount.AddFunds(-itemToPurchase.ReturnItem().itemPrice);
            playerInventory.AddItem(itemToPurchase.ReturnItem());

            //Update the UI
            DisplayCurrentBankFunds();
        }
        else
        {
            Debug.Log("No tienes suficiente dinero");
            canBuy = false;
        }

        //Send message feedback to the dialogue manager depending on the output
        shopDialogueTrigger.ChangeDialogue(canBuy);
        shopDialogueTrigger.TriggerDialogue();
    }

    public void DisplayCurrentBankFunds()
    {
        currentPlayerFundsUI.text = playerBankAccount.GetAccountBalance().ToString();
    }
}