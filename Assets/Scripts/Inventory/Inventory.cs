//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 09/12/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private InventoryMainLists mainList;

    public void AddFromScript(Collectable item)
    {
        if (!InventoryFull())
        {
            PopUpUI.instance.Show(PopUpType.InventoryNewItem, item.CollectItem().itemSprite);
            mainList.Add(item.CollectItem());
        }
    }

    public void AddItem(SO_Item item)
    {
        if (!InventoryFull())
        {
            PopUpUI.instance.Show(PopUpType.InventoryNewItem, item.itemSprite);
            mainList.Add(item);
        }
    }

    public void Remove(SO_Item itemToRemove)
    {
        if (mainList.mainInventoryList.Contains(itemToRemove))
        {
            mainList.Remove(itemToRemove);
            PopUpUI.instance.Show(PopUpType.InventoryRemoveItem, itemToRemove.itemSprite);
            Debug.Log("Item removed: " + itemToRemove);
        }
        else
            Debug.Log("No se ha encontrado el item para eliminar: " + itemToRemove);
    }

    public bool InventoryFull()
    {
        if (mainList.mainInventoryList.Count >= mainList.inventoryCapacity)
        {
            //Displays message that says that inventory is full
            Debug.Log("Inventory full");
            PopUpUI.instance.Show(PopUpType.InventoryFull);
            return true;
        }
        else
        {
            return false;
        }
    }
}