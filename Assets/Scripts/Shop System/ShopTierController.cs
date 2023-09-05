//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 25/04/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopTierController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float nextTierAugment = 0.6f;

    [SerializeField]
    private float actualTierCost = 50f;

    [SerializeField]
    private SO_TierData tierData;

    private float actualExpends;

    private int actualTier;

    [SerializeField]
    private TextMeshProUGUI nextTierTxt;

    public UnityEvent OnTierUnlock;

    #endregion Variables

    private void Start()
    {
        LoadData();
        UpdateActualTier();
    }

    private void LoadData()
    {
        actualTier = tierData.actualTier;
        actualExpends = tierData.actualExpends;
    }

    public void SaveData()
    {
        tierData.actualTier = actualTier;
        tierData.actualExpends = actualExpends;
    }

    public void OpenNextTier()
    {
        actualTier++;
        tierData.actualTier = actualTier;

        //Update the tier on the shop, reset the expends and recharge the UI
        if (actualTier <= 2)
        {
            actualTierCost = actualTierCost + actualTierCost * nextTierAugment;

            actualExpends = 0;

            UpdateActualTier();

            OnTierUnlock.Invoke();
        }
        else
        {
            UpdateActualTier();
        }
    }

    public int GetActualTier() => actualTier;

    /// <summary>
    /// Adds the cost of the item to the expends needed to unlock the next tier of the shop
    /// </summary>
    /// <param name="item"></param>
    public void AddExpends(SO_Item item)
    {
        float expends = item.itemPrice;

        //Add the expenses to the count, and check if it can advance to the next level
        actualExpends += expends;

        if (actualExpends >= actualTierCost)
        {
            OpenNextTier();
        }
        else
        {
            UpdateActualTier();
        }
    }

    private void UpdateActualTier()
    {
        //Update to the next tier if they haven't been unlocked all of them
        if (actualTier >= 2)
        {
            nextTierTxt.text = "All unlocked";
        }
        else
        {
            nextTierTxt.text = (actualTierCost - actualExpends).ToString();
            AudioManager.instance.PlayClip(SoundsFX.SFX_TierUpgrade);
        }
    }
}