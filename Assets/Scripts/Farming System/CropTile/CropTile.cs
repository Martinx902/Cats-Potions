//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 20/04/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IPlantable), typeof(IWaterable))]
public class CropTile : MonoBehaviour, IInteractable
{
    #region Variables

    [SerializeField]
    private SO_CropTile cropTileData;

    [SerializeField]
    private InventoryMainLists playerInventory;

    private IPlantable plantable;
    private IWaterable waterableSystem;

    private bool isOcuppied = false;

    [Space(15)]
    public UnityEvent OnPlant;

    public UnityEvent OnWater;
    public UnityEvent OnCollect;

    [Header("Collectable Sequence")]
    [Space(15)]
    [SerializeField]
    private CollectSequence plantCollectableSO;

    #endregion Variables

    private void Awake()
    {
        if (!cropTileData.isUnlocked)
        {
            gameObject.SetActive(false);
        }

        plantable = GetComponent<IPlantable>();
        waterableSystem = GetComponent<IWaterable>();

        LoadData();
    }

    #region Events Handlers and Data Savers/Loaders

    private void OnEnable()
    {
        GameManager.nextDayDelegate += NextDay;
    }

    private void LoadData()
    {
        if (cropTileData.seed == null)
            return;

        isOcuppied = cropTileData.isOccupied;

        if (cropTileData.isIrrigated)
            waterableSystem.LoadStateData(cropTileData.isIrrigated);

        plantable.LoadData(cropTileData);
    }

    private void OnDisable()
    {
        GameManager.nextDayDelegate -= NextDay;
    }

    public void SaveData()
    {
        if (plantable.GetSeed() == null)
            return;

        cropTileData.SaveData(plantable.GetSeed(), isOcuppied, waterableSystem.CheckWaterState());

        plantable.SaveData(cropTileData);

        Debug.Log("CropTile:" + this.name + "\n Is irrigated: " + cropTileData.isIrrigated + "\n Seed:" + cropTileData.seed + "\n Is Occupied:" + cropTileData.isOccupied);
    }

    #endregion Events Handlers and Data Savers/Loaders

    private void NextDay()
    {
        if (plantable == null || waterableSystem == null)
            return;

        plantable.Grow();
        waterableSystem.ResetTile();

        SaveData();
    }

    public void Interact()
    {
        //Updates CropTile State
        CheckState();

        //Decides depending on the state what to do
        if (!isOcuppied)
        {
            //Check to see if the player has any seeds to plant
            if (playerInventory.CheckForSeeds())
                //If not ocuppied, the player can plant
                OnPlant.Invoke();
            else
                PopUpUI.instance.Show(PopUpType.NoSeeds);
        }
        else
        {
            //If ocuppied, then check if the plant is ready to be collected or irrigated
            if (plantable.HandleState())
            {
                //COLLECT THE PLANT
                if (playerInventory.IsFull())
                {
                    PopUpUI.instance.Show(PopUpType.InventoryFull);
                }
                else
                {
                    OnCollect.Invoke();

                    //Reset the data from the save
                    cropTileData.ResetData();

                    isOcuppied = false;
                }
            }
            //If is ocuppied but not watered
            else if (!waterableSystem.CheckWaterState())
            {
                waterableSystem.WaterPlant();
                OnWater.Invoke();
            }
            else
            {
                //Everything okay, just let it grow
                Debug.Log("Ocuppied, Growing seed... " + plantable.GetSeed());
            }
        }

        SaveData();
    }

    private void CheckState()
    {
        //The State of being ocuppied or not depends whether the scripts has or not a seed attached to it
        if (plantable.GetSeed() == null)
            isOcuppied = false;
        else
            isOcuppied = true;
    }

    public void InstantiateSeed(SO_Seed seedReceived)
    {
        if (seedReceived == null)
            return;

        plantable.Plant(seedReceived, this.transform);
        isOcuppied = true;

        SaveData();
    }
}