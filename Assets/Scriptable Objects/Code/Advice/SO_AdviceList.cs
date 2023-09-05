//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 13/05/2023

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Advice / New Advice List"))]
public class SO_AdviceList : ScriptableObject
{
    public List<SO_Advice> adviceList;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}