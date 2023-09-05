//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 23/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionController : MonoBehaviour, IInteractable
{
    #region Inspector Variables

    [SerializeField]
    private SO_Mission actualMissionHolder;

    [SerializeField]
    private List<SO_MissionHolder> missionList = new List<SO_MissionHolder>();

    [SerializeField]
    private InventoryMainLists playerInventoryMainList;

    public UnityEvent<SO_Mission> onNewMission;

    #endregion Inspector Variables

    #region Private Variables

    private ITalkableComposite mainQuestLine;

    private ITalkable genericResponses;

    private SO_Mission actualMission;

    private int actualMissionIndex = 0;

    private int itemCount = 0;

    private bool[] tempItemCount = new bool[3];

    #endregion Private Variables

    private void Start()
    {
        actualMissionIndex = actualMissionHolder.missionIndex;

        if (missionList == null)
            Debug.Log("No missions asigned to the bear yet to give");

        actualMission = missionList[actualMissionIndex].mission;

        mainQuestLine = GetComponent<ITalkableComposite>();

        genericResponses = GetComponent<ITalkable>();
    }

    private void CheckForMission()
    {
        tempItemCount = new bool[actualMission.itemsToComplete.Count];
        itemCount = 0;

        ResetTempList();

        //Check to see if this is their first mission
        if (actualMission.isActive)
        {
            if (actualMissionIndex >= missionList.Count)
                GenericResponse();

            if (actualMission.isActive)
            {
                //Por cada item del inventario
                for (int i = 0; i < playerInventoryMainList.mainInventoryList.Count; i++)
                {
                    for (int y = 0; y < actualMission.itemsToComplete.Count; y++)
                    {
                        //We've got a hit
                        if (playerInventoryMainList.mainInventoryList[i] == actualMission.itemsToComplete[y] && !tempItemCount[y])
                        {
                            itemCount++;
                            tempItemCount[y] = false;
                            break;
                        }
                    }
                }

                if (itemCount >= actualMission.itemsToComplete.Count)
                {
                    //If they have all the objects, perfect, completed
                    MissionComplete();
                }
                else
                {
                    GenericResponse();
                }

                itemCount = 0;
                ResetTempList();
            }
        }
        else
        {
            //Give the player a new mission
            ShowQuestDialogue(missionList[actualMissionIndex].questDialogue);
            onNewMission.Invoke(actualMission);
        }
    }

    private void GenericResponse()
    {
        genericResponses.TriggerDialogue();
    }

    private void ShowQuestDialogue(SO_Dialogue dialogueToShow)
    {
        mainQuestLine.SelectDialogue(dialogueToShow);
        mainQuestLine.TriggerDialogue();
    }

    private void MissionComplete()
    {
        if (actualMission.rewards.Count > 0)
        {
            //Give the rewards to the player
            foreach (SO_Item item in actualMission.rewards)
            {
                playerInventoryMainList.Add(item);
            }
        }

        //Remove the items from the player inventory
        foreach (SO_Item item in actualMission.itemsToComplete)
        {
            playerInventoryMainList.Remove(item);
        }

        //Thanks the player
        ShowQuestDialogue(missionList[actualMissionIndex].gratitudeDialogue);

        //Complete the mission
        actualMission.isCompleted = true;

        //We activate the completed event in case something needs to be triggered
        actualMission.onMissionComplete.Invoke();

        //Update the actual mission
        actualMissionIndex++;

        //Audio feedback
        AudioManager.instance.PlayClip(SoundsFX.SFX_MissionComplete);

        //UI Feedback
        PopUpUI.instance.Show(PopUpType.MissionCompleted);

        //No more missions, so just generic responses
        if (actualMissionIndex >= missionList.Count)
        {
            Debug.Log("No more missions");
            return;
        }

        //Advance to the next mission on the list
        actualMission = missionList[actualMissionIndex].mission;
    }

    public void Interact()
    {
        CheckForMission();
    }

    public void ResetTempList()
    {
        for (int i = 0; i < tempItemCount.Length; i++)
        {
            tempItemCount[i] = false;
        }
    }
}