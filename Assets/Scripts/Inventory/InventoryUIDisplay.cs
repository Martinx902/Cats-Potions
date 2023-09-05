//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 29/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIDisplay : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private ItemDisplayUI itemDisplay;

    [SerializeField]
    private List<ItemUI> inventorySlots;

    [SerializeField]
    private List<ItemUI> seedSlots;

    [SerializeField]
    private List<ItemUI> craftingSlots;

    private List<SO_Item> inventoryList;

    #endregion Variables

    /// <summary>
    /// Displays the player inventory on the UI with the SO Main List of the player
    /// </summary>
    /// <param name="listToDisplay"></param>
    public void DisplayInventory(InventoryMainLists listToDisplay)
    {
        if (listToDisplay != null)
        {
            inventoryList = listToDisplay.mainInventoryList;

            foreach (ItemUI item in inventorySlots)
            {
                item.CleanItem();
            }

            if (itemDisplay != null)
                itemDisplay.ReciveList(listToDisplay.mainInventoryList);

            for (int i = 0; i < inventoryList.Count; i++)
            {
                inventorySlots[i].item = inventoryList[i];

                inventorySlots[i].SetIndex(i);

                inventorySlots[i].Display();
            }
        }
    }

    /// <summary>
    /// Gets all the seed of the player inventory and then displays them on screen when selecting a tile to plant on
    /// </summary>
    /// <param name="listToDisplay"></param>
    public void DisplaySeedInventory(InventoryMainLists listToDisplay)
    {
        if (listToDisplay != null)
        {
            inventoryList = listToDisplay.mainInventoryList;

            int index = 0;

            //Clean the previous items
            foreach (ItemUI item in seedSlots)
            {
                item.CleanItem();
            }

            //Sorts and selects the seeds item from the inventory and adds them to the UI slots
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i] is SO_Seed)
                {
                    seedSlots[index].item = inventoryList[i];

                    seedSlots[index].Display();

                    index++;
                }
            }
        }
    }

    public void DisplayCraftingInventory(InventoryMainLists listToDisplay)
    {
        if (listToDisplay != null)
        {
            inventoryList = listToDisplay.mainInventoryList;

            int index = 0;

            foreach (ItemUI item in craftingSlots)
            {
                item.CleanItem();
                item.gameObject.SetActive(false);
            }

            //Sorts and selects the craftable items from the inventory and adds them to the UI slots
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i] is SO_CraftableItem)
                {
                    craftingSlots[index].item = inventoryList[i];

                    craftingSlots[index].Display();

                    craftingSlots[index].gameObject.SetActive(true);

                    index++;
                }
            }
        }
    }
}