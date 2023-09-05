//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 14/03/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Warehouse : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private InventoryMainLists firstTierInventory;

    [SerializeField]
    private InventoryMainLists secondTierInventory;

    [SerializeField]
    private InventoryMainLists thirdTierInventory;

    #endregion Inspector Variables

    #region Private Variables

    private Dictionary<int, InventoryMainLists> inventories = new Dictionary<int, InventoryMainLists>();

    private Dictionary<int, TierItems> tempItems = new Dictionary<int, TierItems>();

    public PassDictionary passDictionary;

    #endregion Private Variables

    private void Awake()
    {
        //Add inventories to the list with a key
        inventories.Add(0, firstTierInventory);
        inventories.Add(1, secondTierInventory);
        inventories.Add(2, thirdTierInventory);
    }

    //Send the dictionary to everyone interested
    private void PassDictionary()
    {
        passDictionary.Invoke(tempItems);
    }

    //Add random items to the tempItems list of the daily options
    private void GenerateRandomInventories()
    {
        List<SO_Item> tempList;

        tempItems.Clear();

        for (int i = 0; i <= inventories.Count - 1; i++)
        {
            tempList = new List<SO_Item>();

            tempList.Clear();

            TierItems newTierItems = new TierItems(tempList);
            //Adds three random items from a pool to the list depending on the tier

            for (int y = 0; y <= 2; y++)
            {
                newTierItems.myItems.Add(SelectRandomItem(i));
            }

            tempItems.Add(i, newTierItems);
        }

        PassDictionary();
    }

    //Choose a random item from an inventory from the dictionary
    private SO_Item SelectRandomItem(int inventoryTier)
    {
        int randomIndex = Random.Range(0, firstTierInventory.mainInventoryList.Count - 1);

        InventoryMainLists tempInventory = inventories[inventoryTier];

        if (tempInventory.mainInventoryList.Count == 0)
        {
            Debug.Log("No hay items en los inventarios de la tienda. Añadirlos antes de continuar");
            return null;
        }

        SO_Item tempItem = tempInventory.mainInventoryList[randomIndex];

        return tempItem;
    }

    #region Events Handlers

    private void OnEnable()
    {
        GameManager.nextDayDelegate += GenerateRandomInventories;
    }

    private void OnDisable()
    {
        GameManager.nextDayDelegate -= GenerateRandomInventories;
    }

    #endregion Events Handlers
}

public class TierItems
{
    public List<SO_Item> myItems;

    public TierItems(List<SO_Item> myList)
    {
        myItems = new List<SO_Item>();

        myItems = myList;
    }
}