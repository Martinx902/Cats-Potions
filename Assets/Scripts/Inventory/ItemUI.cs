//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 27/11/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [HideInInspector]
    public SO_Item item;

    public int index { get; private set; }

    [SerializeField]
    private Image itemImg;

    [SerializeField]
    private Sprite emptyImg;

    // Start is called before the first frame update
    private void Start()
    {
        if (item)
        {
            Display();
        }
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    /// <summary>
    /// Displays the item name on the text button
    /// </summary>
    public void Display()
    {
        if (item)
        {
            //itemName.text = item.itemName;
            itemImg.sprite = item.itemSprite;
        }
    }

    public void CleanItem()
    {
        if (item)
        {
            item = null;
            //itemName.text = string.Empty;
            itemImg.sprite = emptyImg;
        }
    }

    public SO_Item GetItem() => item;
}