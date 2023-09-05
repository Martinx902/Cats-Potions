using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDialogueController : MonoBehaviour, ITalkableComposite
{
    [SerializeField]
    private DialogueCustomEvent OnStartDialogue;

    private SO_Dialogue actualDialogue;

    public void ChangeDialogue(bool isGeneric)
    {
    }

    public void TriggerDialogue()
    {
        OnStartDialogue.Invoke(actualDialogue);
    }

    public void SelectDialogue(SO_Dialogue dialogueToShow)
    {
        actualDialogue = dialogueToShow;
    }
}