//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 16/04/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDisplayUI : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject missionItem;

    [SerializeField]
    private Transform itemsPosition;

    [SerializeField]
    private SO_Mission missionDataHolder;

    private List<GameObject> actualItems = new List<GameObject>();

    private SO_Mission actualMission;

    private static bool newMission = false;

    #endregion Variables

    private void Start()
    {
        actualMission = missionDataHolder;
        DisplayMissionItems();
    }

    public void SetActualMission(SO_Mission actualMission)
    {
        if (this.actualMission != actualMission || actualMission != null)
        {
            this.actualMission = actualMission;
            newMission = true;
        }
    }

    /// <summary>
    /// Display the actual mission items on the UI
    /// </summary>
    public void DisplayMissionItems()
    {
        if (actualMission != null && !missionDataHolder.isCompleted)
        {
            //Destroy the previous mission items

            foreach (GameObject item in actualItems)
            {
                item.SetActive(false);
                Destroy(item);
            }
            actualItems.Clear();

            //Instatiate and Display many ItemUI as the items has the actual mission to complete.
            //(Not the best way to handle this, could use some optimisation) TODO??

            for (int i = 0; i < actualMission.itemsToComplete.Count; i++)
            {
                GameObject newItem = Instantiate(missionItem, itemsPosition);

                newItem.transform.SetParent(itemsPosition, false);

                actualItems.Add(newItem);

                ItemUI itemUI = newItem.GetComponent<ItemUI>();

                if (itemUI == null)
                {
                    Debug.Log("Error encontrando los items de la mision a mostrar en la UI");
                    return;
                }

                itemUI.item = actualMission.itemsToComplete[i];

                itemUI.Display();
            }

            newMission = false;
        }
    }

    private void OnEnable()
    {
        DisplayMissionItems();
    }
}