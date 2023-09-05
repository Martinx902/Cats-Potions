using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Shop/Daily Items Data"))]
public class SO_ShopDailyItems : ScriptableObject
{
    public List<UIShopList> dailyItemsList;

    public bool debug;

    private void Awake()
    {
        if (debug)
        {
            CleanList();
        }
    }

    public void CleanList()
    {
        foreach (UIShopList item in dailyItemsList)
        {
            foreach (UIShopItem shopItem in item.uiShopItems)
            {
                shopItem.ClearItem();
            }
        }
    }

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}