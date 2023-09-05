//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 19/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class SellSystem : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private List<UIShopItem> productSlots = new List<UIShopItem>();

    [SerializeField]
    private TextMeshProUGUI priceTxt;

    private List<SO_Item> itemsToSell = new List<SO_Item>();

    private int priceOfItemsToSell = 0;

    private BankAccount playerBankAccount;

    private Inventory playerInventory;

    #endregion Variables

    private void Awake()
    {
        //Get references and debug errors
        playerBankAccount = FindObjectOfType<BankAccount>();
        playerInventory = FindObjectOfType<Inventory>();

        if (playerBankAccount == null || playerInventory == null)
        {
            Debug.LogError("Player bank account or inventory not found on Sell System");
            return;
        }
    }

    //Add items to sell
    public void AddItemToSell(ItemUI item)
    {
        if (item.item == null)
            return;

        if (itemsToSell.Count >= 6)
            return;

        itemsToSell.Add(item.item);

        //Add the item to the corresponding shop slot
        UIShopItem shopSlot = productSlots.First(t => t.ReturnItem() == null);

        //Set the item on the shop slot
        shopSlot.SetItem(item.item);
        shopSlot.DisplayItem();

        //Update the Price UI
        UpdatePrice(item.item.itemPrice);

        //Remove from inventory
        playerInventory.Remove(item.item);

        //Clean the item from the other inventory
        item.CleanItem();
        item.Display();
    }

    //Remove item to sell
    public void RemoveItemToSell(SO_Item item)
    {
        itemsToSell.Remove(item);

        UpdatePrice(-item.itemPrice);
    }

    /// <summary>
    /// Updates price UI
    /// </summary>
    /// <param name="itemPrice"></param>
    private void UpdatePrice(int itemPrice)
    {
        priceOfItemsToSell += itemPrice;
        priceTxt.text = priceOfItemsToSell.ToString();
    }

    /// <summary>
    /// Remove the items from the list and adds the money to the player
    /// </summary>
    public void Sell()
    {
        //Add the founds to the player bank account
        playerBankAccount.AddFunds(priceOfItemsToSell);

        AudioManager.instance.PlayClip(SoundsFX.SFX_Coins);

        //Remove items from the player inventory
        foreach (SO_Item item in itemsToSell)
        {
            playerInventory.Remove(item);
        }

        //Recharge UI
        itemsToSell.Clear();

        priceOfItemsToSell = 0;

        UpdatePrice(priceOfItemsToSell);

        //Remove the items from the product slot
        foreach (UIShopItem item in productSlots)
        {
            item.UnDisplayItem();
            item.ClearItem();
        }
    }

    /// <summary>
    /// Remove all the items to sell
    /// </summary>
    public void Undo()
    {
        if (itemsToSell.Count <= 0)
            return;

        //Empty the product slots
        foreach (UIShopItem slot in productSlots)
        {
            if (slot.ReturnItem() != null)
            {
                playerInventory.AddItem(slot.ReturnItem());
                slot.UnDisplayItem();
                slot.ClearItem();
            }
        }

        //Clear the list of items to sell
        itemsToSell.Clear();

        //Reset the UI price display
        priceOfItemsToSell = 0;
        UpdatePrice(priceOfItemsToSell);
    }

    /// <summary>
    /// Remove an item to sell from the sell window
    /// </summary>
    /// <param name="itemToUndo"></param>
    public void UndoItem(UIShopItem itemToUndo)
    {
        if (itemToUndo.ReturnItem() == null)
            return;

        SO_Item item = itemToUndo.ReturnItem();

        //Get the slot where the item is being held
        UIShopItem itemSlot = productSlots.FirstOrDefault(slot => slot == itemToUndo);

        //Undisplay it
        itemSlot.UnDisplayItem();
        itemSlot.ClearItem();

        //Remove it from the list
        itemsToSell.Remove(item);

        //Add it to the inventory
        playerInventory.AddItem(item);

        //Reset the UI price display
        UpdatePrice(-item.itemPrice);
    }
}