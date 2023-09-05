//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 04/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField]
    private List<UIShopList> UIShopItems = new List<UIShopList>();

    //Scriptable object with the list of daily items
    [SerializeField]
    private SO_ShopDailyItems dailyItems;

    //Daily item dictionary sorted by tier
    private Dictionary<int, TierItems> tempItems = new Dictionary<int, TierItems>();

    [SerializeField]
    private SO_TierData tierData;

    private int actualTier = 0;

    public void SetDictionary(Dictionary<int, TierItems> tempItems)
    {
        this.tempItems = tempItems;

        Debug.Log("Nuevos items recibidos a la tienda");

        DistributeItems();
    }

    //Distribute the items from the lists on the UI items, sorting by tier
    public void DistributeItems()
    {
        for (int i = 0; i <= UIShopItems.Count - 1; i++)
        {
            //Check with the shopTierController that we can render the next tier of objects

            for (int y = 0; y <= 2; y++)
            {
                TierItems newTierItem = tempItems[i];

                UIShopItem shopItem = UIShopItems[i].uiShopItems[y];

                shopItem.SetItem(newTierItem.myItems[y]);
            }
        }
    }

    public void UnlockTier()
    {
        actualTier++;
        DisplayItems();
    }

    /// <summary>
    /// Display the items saved on the lists to the UI on screen
    /// </summary>
    public void DisplayItems()
    {
        for (int i = 0; i <= UIShopItems.Count - 1; i++)
        {
            if (actualTier >= i)
            {
                Debug.Log("Displaying tier: " + actualTier);

                for (int y = 0; y <= 2; y++)
                {
                    UIShopItem item = UIShopItems[i].uiShopItems[y];

                    if (item.hasBeenBought)
                    {
                        item.UnDisplayItem();
                    }
                    else
                    {
                        item.DisplayItem();
                    }
                }
            }
        }
    }

    #region Load/Save Data

    public void LoadData()
    {
        for (int i = 0; i <= UIShopItems.Count - 1; i++)
        {
            for (int y = 0; y <= 2; y++)
            {
                UIShopItem shopItem = UIShopItems[i].uiShopItems[y];

                shopItem.SetItem(dailyItems.dailyItemsList[i].uiShopItems[y].ReturnItem());
                shopItem.hasBeenBought = dailyItems.dailyItemsList[i].uiShopItems[y].hasBeenBought;
            }
        }

        actualTier = tierData.actualTier;

        Debug.Log("Shop data loaded");
    }

    public void SaveData()
    {
        for (int i = 0; i <= UIShopItems.Count - 1; i++)
        {
            for (int y = 0; y <= 2; y++)
            {
                UIShopItem shopItem = UIShopItems[i].uiShopItems[y];

                dailyItems.dailyItemsList[i].uiShopItems[y].SetItem(shopItem.ReturnItem());
                dailyItems.dailyItemsList[i].uiShopItems[y].hasBeenBought = shopItem.hasBeenBought;

                Debug.Log("Item saved: " + dailyItems.dailyItemsList[i].uiShopItems[y].ReturnItem() + " and has been bought:" + dailyItems.dailyItemsList[i].uiShopItems[y].hasBeenBought);
            }
        }

        Debug.Log("Shop data saved");
    }

    #endregion Load/Save Data
}

[System.Serializable]
public class UIShopList
{
    public List<UIShopItem> uiShopItems = new List<UIShopItem>();
}

[System.Serializable]
public class PassDictionary : UnityEvent<Dictionary<int, TierItems>>
{
}