//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 17/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueTrigger : MonoBehaviour, ITalkable
{
    [SerializeField]
    private SO_Dialogue thanksDialogue;

    [SerializeField]
    private SO_Dialogue badDialogue;

    [SerializeField]
    private DialogueCustomEvent OnStartDialogue;

    private bool ending = false;

    public void TriggerDialogue()
    {
        if (ending)
        {
            OnStartDialogue.Invoke(thanksDialogue);
        }
        else
        {
            OnStartDialogue.Invoke(badDialogue);
        }
    }

    public void ChangeDialogue(bool ending)
    {
        this.ending = ending;
    }
}