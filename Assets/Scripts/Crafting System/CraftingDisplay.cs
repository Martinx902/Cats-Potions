// Martin Pérez Villabrille
//Cat & Potions
//Last Modification 18/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDisplay : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform instPosition;

    private GameObject prevLayout = null;

    private bool firstLayout = true;

    private CraftingManager craftingManager;

    #endregion Variables

    private void Start()
    {
        craftingManager = GetComponent<CraftingManager>();
    }

    /// <summary>
    /// Instatiates a crafting layout depending on the station parameters
    /// </summary>
    /// <param name="craftingLayout"></param>
    public void InstantiateLayOut(SO_CraftingLayout craftingLayout)
    {
        ////Instatiates the crafting layout of a station
        //if (prevLayout == null)
        //{
        //    prevLayout = craftingLayout.layOut;
        //}

        //If the layout is different from the last time then instatiate one
        if (craftingLayout.layOut != prevLayout || firstLayout)
        {
            //Create one and register it on the crafting manager to get their references in order to process the craft.
            prevLayout = craftingLayout.layOut;
            Instantiate(craftingLayout.layOut, instPosition);
            craftingManager.SetLayOut(prevLayout);
            firstLayout = false;
        }
    }
}