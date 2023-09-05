//Martin Pérez Villabrille
//Cat & Potions 
//Last Modification 05/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class PlantCollectable : Collectable, IInteractable
{   
    #region Inspector Variables
    [Space(15)]

    [Header("Just for Description")]
    [Space(15)]
    [SerializeField]
    [Multiline(5)]
    private string description;

    [Header("SO Collectable Sequence Coroutine")]
    [Space(15)]
    [SerializeField]
    CollectSequence plantCollectableSO;

    //[Header("SO Item to Collect")]
    //[Space(15)]
    //[SerializeField]
    //SO_Item item;

    [Header("Unity Event response")]
    [Space(15)]
    public UnityEvent Response;

    #endregion

    public void Interact()
    {
        if (plantCollectableSO)
        {
            //Starts the SO coroutine
            StartCoroutine(plantCollectableSO.CollectPlantCoroutine(this));
        }
        //Unity Event Response
        Response.Invoke();
    }

    public override SO_Item CollectItem()
    {
        return item;
    }
}
