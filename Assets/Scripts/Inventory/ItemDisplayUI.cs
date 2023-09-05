//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 03/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using static UnityEditor.Progress;

public class ItemDisplayUI : MonoBehaviour
{
    #region Variables

    //Item to display
    private SO_Item itemToDisplay;

    private int currentIndex = 0;

    private List<SO_Item> items = new List<SO_Item>();

    //UI resources to display, texts, descriptions, img...
    [SerializeField]
    private TextMeshProUGUI itemNameUI;

    [SerializeField]
    private Image itemImageUI;

    [SerializeField]
    private TextMeshProUGUI itemDescriptionUI;

    #endregion Variables

    //Set the item to display depending of its index so later it can update the index to navigate with the arrows
    public void SetItem(ItemUI itemUI)
    {
        if (itemUI.item != null)
        {
            currentIndex = itemUI.index;

            itemToDisplay = items[currentIndex];

            if (itemToDisplay != null)
            {
                DisplayItem();
            }
        }
    }

    //Update the index depending on the direction of the arrow the player choose
    public void ChangeIndex(int direction)
    {
        if (itemToDisplay == null)
            return;

        currentIndex += direction;

        if (currentIndex >= items.Count)
        {
            currentIndex = 0;
        }
        else if (currentIndex < 0)
        {
            currentIndex = items.Count - 1;
        }

        itemToDisplay = items[currentIndex];

        DisplayItem();
    }

    public void DisplayItem()
    {
        if (itemToDisplay != null)
        {
            itemNameUI.text = itemToDisplay.itemName;

            itemImageUI.sprite = itemToDisplay.itemSprite;

            itemDescriptionUI.text = itemToDisplay.itemDescription;
        }
    }

    public void ReciveList(List<SO_Item> _items)
    {
        items = _items;
    }
}