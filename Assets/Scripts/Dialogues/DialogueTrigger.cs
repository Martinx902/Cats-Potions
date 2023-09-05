//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 23/03/2022

using UnityEngine;

public class DialogueTrigger : MonoBehaviour, ITalkable, IInteractable
{
    [SerializeField]
    private SO_CompositeDialogue dialogues;

    [SerializeField]
    private DialogueCustomEvent OnStartDialogue;

    public void TriggerDialogue()
    {
        if (dialogues == null) return;

        //If you have finished the dialogues or you cant see the next one, then just repeat the last one
        if (dialogues.dialogueIndex < dialogues.dialogueList.Count && dialogues.nextDialogue)
        {
            OnStartDialogue.Invoke(dialogues.dialogueList[dialogues.dialogueIndex]);

            dialogues.dialogueIndex++;
        }
        else
        {
            dialogues.dialogueIndex = dialogues.dialogueList.Count - 1;

            if (dialogues.dialogueIndex < 0)
            {
                dialogues.dialogueIndex = 0;
            }

            OnStartDialogue.Invoke(dialogues.dialogueList[dialogues.dialogueIndex]);
        }
    }

    public void ChangeDialogue(bool change)
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        TriggerDialogue();
    }
}