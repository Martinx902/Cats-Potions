//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 10/03/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private SO_Dialogue currentDialogue;

    private PlayerInteractable playerInteractable;
    private CharacterController playerMovement;

    [Space(15)]
    [Header("Sound")]
    [Space(15)]
    // Juan
    [SerializeField]
    private GameObject dialogueSound;

    #region Inspector Variables

    [Header("UI Components")]
    [Space(15)]
    [SerializeField]
    private GameObject dialoguePanelUI;

    [SerializeField]
    private TextMeshProUGUI npcNameTxt;

    [SerializeField]
    private Image npcImage;

    [SerializeField]
    private TextMeshProUGUI sentenceTxt;

    [Space(15)]
    [Header("Characters Animator")]
    [Space(15)]
    [SerializeField]
    private CharacterAnimator characterAnim;

    #endregion Inspector Variables

    private void Start()
    {
        playerInteractable = FindObjectOfType<PlayerInteractable>();
        playerMovement = FindObjectOfType<CharacterController>();

        sentences = new Queue<string>();
    }

    public void StartDialogue(SO_Dialogue dialogue)
    {
        if (dialogue == null) return;

        ManagePlayerInteractions(false);

        characterAnim.Animate(true);

        currentDialogue = dialogue;

        DisplayDialogueUI();

        sentences.Clear();

        //Add the sentences to a queue
        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //Display the first sentence
        DisplayNextSentence();
    }

    private void DisplayDialogueUI()
    {
        if (dialoguePanelUI == null)
            Debug.LogError("Error en el dialogue manager, no has conectado el display dialogue UI mongol");

        dialoguePanelUI.SetActive(true);
        npcNameTxt.text = currentDialogue.npcName;
        npcImage.sprite = currentDialogue.npcImage;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Add the last sentence to dequeue
        string currentSentence = sentences.Dequeue();

        //Stop all the previous sentences display
        StopAllCoroutines();

        //Start the dequeu animation for the next sentence
        StartCoroutine(TypeSentence(currentSentence));
        dialogueSound.SetActive(true);
    }

    /// <summary>
    /// Animate the sentence to write
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private IEnumerator TypeSentence(string sentence)
    {
        sentenceTxt.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            sentenceTxt.text += letter;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        dialogueSound.SetActive(false);

        if (sentences.Count == 0)
        {
            ManagePlayerInteractions(true);
        }
    }

    private void EndDialogue()
    {
        ManagePlayerInteractions(true);
        characterAnim.Animate(false);
        dialoguePanelUI.SetActive(false);
    }

    private void ManagePlayerInteractions(bool interaction)
    {
        playerMovement.CatTalking(!interaction);
        playerMovement.CanMove(interaction);
        playerInteractable.CanInteract(interaction);
    }
}