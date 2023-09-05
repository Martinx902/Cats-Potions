//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 03/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoxInteractionHandler : MonoBehaviour, IInteractable
{
    public UnityEvent onFoxInteraction;

    //Handles the menu of the interactions with the fox NPC
    public void Interact()
    {
        onFoxInteraction.Invoke();
    }
}