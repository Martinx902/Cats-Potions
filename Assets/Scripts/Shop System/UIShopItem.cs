//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 04/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShopItem : MonoBehaviour
{
    private SO_Item itemToDisplay;

    [HideInInspector]
    public bool hasBeenBought = false;

    [SerializeField]
    private Sprite occupiedImage;

    [SerializeField]
    private string occupiedText;

    [SerializeField]
    private Image itemImageUI;

    [SerializeField]
    private TextMeshProUGUI itemPriceUI;

    private Button button;

    private void GetButton()
    {
        button = GetComponent<Button>();

        Debug.Log("Button: " + button);

        if (button == null)
            button = GetComponentInChildren<Button>();
    }

    public void DisplayItem()
    {
        if (itemToDisplay != null)
        {
            itemImageUI.sprite = itemToDisplay.itemSprite;

            itemPriceUI.text = itemToDisplay.itemPrice.ToString();

            GetButton();

            button.interactable = true;
        }
    }

    public void UnDisplayItem()
    {
        if (itemToDisplay != null)
        {
            itemImageUI.sprite = occupiedImage;

            itemPriceUI.text = occupiedText;
        }
    }

    public void ItemBought()
    {
        hasBeenBought = true;
    }

    public void ClearItem()
    {
        itemToDisplay = null;
        hasBeenBought = false;
    }

    public void SetItem(SO_Item itemToReceive)
    {
        hasBeenBought = false;
        itemToDisplay = itemToReceive;
    }

    public SO_Item ReturnItem() => itemToDisplay;
}