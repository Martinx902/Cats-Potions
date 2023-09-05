//Martin Pérez Villabrille
//Cat & Potions 
//Last Modification 29/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingStation : MonoBehaviour, IInteractable
{
    public UnityEvent onCraft;

    public void Interact()
    {
        onCraft.Invoke();
    }
}
