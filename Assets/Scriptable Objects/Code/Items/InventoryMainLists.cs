//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 21/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Inventory/Main List"))]
public class InventoryMainLists : ScriptableObject
{
    [SerializeField]
    public List<SO_Item> mainInventoryList;

    public int inventoryCapacity = 6;

    public bool CheckForItem(SO_Item item)
    {
        if (item == null)
            return false;

        if (mainInventoryList.Contains(item))
            return true;
        else
            return false;
    }

    public bool CheckForSeeds()
    {
        foreach (SO_Item item in mainInventoryList)
        {
            if (item is SO_Seed)
            {
                return true;
            }
        }

        return false;
    }

    public void Add(SO_Item item)
    {
        mainInventoryList.Add(item);
    }

    public void Remove(SO_Item item)
    {
        if (mainInventoryList.Contains(item))
        {
            mainInventoryList.Remove(item);
        }
    }

    public bool IsFull()
    {
        if (mainInventoryList.Count >= inventoryCapacity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}