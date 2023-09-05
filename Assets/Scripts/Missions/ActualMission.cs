using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualMission : MonoBehaviour
{
    [SerializeField]
    private SO_Mission missionDataHolder;

    private SO_Mission actualMission;

    private void Start()
    {
        actualMission = missionDataHolder;
    }

    /// <summary>
    /// Set the actual mission of the player and active it
    /// </summary>
    /// <param name="newMission"></param>
    public void SetActualMission(SO_Mission newMission)
    {
        PopUpUI.instance.Show(PopUpType.NewMission);

        this.actualMission = newMission;

        actualMission.isActive = true;

        missionDataHolder.itemsToComplete = actualMission.itemsToComplete;
        missionDataHolder.isActive = actualMission.isActive;
        missionDataHolder.isCompleted = actualMission.isCompleted;
        missionDataHolder.onMissionComplete = actualMission.onMissionComplete;
    }

    public SO_Mission ReturnActualMission() => actualMission;
}