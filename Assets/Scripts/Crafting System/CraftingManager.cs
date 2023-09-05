// Martin Pérez Villabrille
//Cat & Potions
//Last Modification 25/04/2023

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CraftingManager : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private ItemUI resultSlot;

    [SerializeField]
    private SO_Item genericItem;

    [SerializeField]
    private List<SO_Recipe> RecipeList;

    [SerializeField]
    private Inventory playerInventory;

    [SerializeField]
    private RecipeBook recipeBook;

    #endregion Inspector Variables

    #region Private Variables

    private List<SO_Item> itemList = new List<SO_Item>();

    [SerializeField]
    private List<ItemUI> itemUIList = new List<ItemUI>();

    private bool[] tempCraftingItems = new bool[3];

    private SO_Recipe recipeCompleted;

    private SO_Item resultItem;
    private int itemCount = 0;
    private bool isEqual;
    private bool isFull;

    #endregion Private Variables

    //TODO Clean this code and way of checking for recipes

    public void Craft()
    {
        int itemCount = 0;
        ResetTempList();

        //Checks to see if all the slots have items

        foreach (ItemUI tempItem in itemUIList)
        {
            if (tempItem == null || tempItem.item == null)
            {
                PopUpUI.instance.Show(PopUpType.IntroduceAllItems);
                return;
            }

            itemList.Add(tempItem.item);
            itemCount++;
        }

        //Removes them from inventory and cleans the UI

        if (itemCount >= itemUIList.Count - 1)
        {
            foreach (ItemUI item in itemUIList)
            {
                playerInventory.Remove(item.item);
                item.CleanItem();
                item.Display();
                item.gameObject.SetActive(false);
            }
        }

        itemCount = 0;

        //Look for a recipe that contains the ingredients the player added to the crafting station
        for (int i = 0; i < RecipeList.Count; i++)
        {
            //Very unoptimized way of doing this, but just checks to see if the 3 items match the ones on the recipe ingredient list
            // by adding a counter, if they all match then we have a result, if not check the next recipe.

            //Nice thing to do perhaps is implementing linq here to do the searches, could make this more readable and debuggable.
            for (int x = 0; x < itemList.Count; x++)
            {
                for (int y = 0; y < RecipeList[i].items.Count; y++)
                {
                    if (itemList[x] == RecipeList[i].items[y] && !tempCraftingItems[y])
                    {
                        //Es el mismo objeto y entonces registramos su posición y sumamos un valor
                        itemCount++;
                        tempCraftingItems[y] = true;
                        break;
                    }
                }
            }

            //If we completed all the items the recipe needs, then we have completed a new recipe
            if (itemCount >= RecipeList[i].items.Count)
            {
                Debug.Log(itemCount);
                Debug.Log("Recipe completed: " + RecipeList[i]);

                isEqual = true;

                resultItem = RecipeList[i].resultItem;
                recipeCompleted = RecipeList[i];

                itemCount = 0;
                ResetTempList();

                break;
            }
            else
            {
                isEqual = false;
                Debug.Log("No hemos crafteando el objeto");
            }

            itemCount = 0;
            ResetTempList();
        }

        //If they all match then we got a recipe done.
        if (isEqual)
        {
            resultSlot.gameObject.SetActive(true);
            resultSlot.item = resultItem;
            resultSlot.Display();
            AudioManager.instance.PlayClip(SoundsFX.SFX_Craft);

            if (!recipeCompleted.isUnlocked)
                recipeBook.AddRecipeToList(recipeCompleted);
        }
        else if (!isEqual)
        {
            resultSlot.gameObject.SetActive(true);
            resultSlot.item = genericItem;
            resultSlot.Display();
        }

        itemCount = 0;
        itemList.Clear();
    }

    public void ResetTempList()
    {
        for (int i = 0; i < tempCraftingItems.Length; i++)
        {
            tempCraftingItems[i] = false;
        }
    }

    public void CollectItem()
    {
        //Give the player the result item he obtained crafting
        if (resultSlot.item != null)
        {
            playerInventory.AddItem(resultSlot.item);
            resultSlot.item = null;
            resultSlot.gameObject.SetActive(false);
            AudioManager.instance.PlayClip(SoundsFX.SFX_Craft);
        }
    }

    public void SetLayOut(GameObject layOut)
    {
        //Set the crafting station layout and get a reference to the itemUI on them,
        //that way we can have modular layouts depending on the station
        for (int i = 0; i < layOut.transform.childCount; i++)
        {
            GameObject craftingSlot = layOut.transform.GetChild(i).gameObject;

            ItemUI tempItem = craftingSlot.transform.GetComponentInChildren<ItemUI>(true);

            if (tempItem != null)
            {
                itemUIList.Add(tempItem);
            }
        }
    }
}