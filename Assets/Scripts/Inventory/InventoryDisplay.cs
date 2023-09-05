using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private bool viewingInventory = false;

    [SerializeField]
    private InventoryMainLists inventoryToDisplay;

    public void DisplayInventory(GameObject inventoryUI)
    {
        if (inventoryUI.activeInHierarchy)
        {
            viewingInventory = false;
            inventoryUI.SetActive(false);
        }
        else
        {
            viewingInventory = true;
            Debug.Log("Objetos en inventario: " + inventoryToDisplay.mainInventoryList.Count);
            inventoryUI.SetActive(true);
        }
    }
}