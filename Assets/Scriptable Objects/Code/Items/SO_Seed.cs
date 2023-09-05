//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 12/11/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/New Seed"))]
public class SO_Seed : SO_Item
{
    //GameObject Prefab list of the plant growth states
    [SerializeField]
    public List<GameObject> plantStages = new List<GameObject>();

    //Time it takes to grow
    [SerializeField]
    public int daysToChange;

    //SO_Plant to collect once this grows
    [SerializeField]
    public SO_Item itemToCollect;

    //public int actualDays;

    //public int seedIndex;

    //public bool isReadyToCollect;

    //public void ResetData()
    //{
    //    actualDays = 0;
    //    seedIndex = 0;
    //    isReadyToCollect = false;
    //}
}