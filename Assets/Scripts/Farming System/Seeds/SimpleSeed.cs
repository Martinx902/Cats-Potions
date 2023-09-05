//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 27/11/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSeed : Collectable, IPlantable
{
    #region Variables

    private IWaterable waterableSystem;

    public SO_Seed seed { get; private set; }

    private Transform plantPosition;
    private GameObject plantObject;
    private List<GameObject> plants = new List<GameObject>();

    private bool isReadyToCollect;
    private int actualDays;
    private int seedIndex;

    #endregion Variables

    private void Awake()
    {
        waterableSystem = GetComponent<IWaterable>();
    }

    #region IPlantable Functions

    /// <summary>
    /// Keeps the data and instantiates a seed on the Tile
    /// </summary>
    /// <param name="seedToPlant"></param>
    /// <param name="plantPosition"></param>
    public void Plant(SO_Seed seedToPlant, Transform plantPosition)
    {
        seed = seedToPlant;

        this.plantPosition = plantPosition;

        InstatiateSeed();
    }

    /// <summary>
    /// Instatiates all the stages of the plant on the crop tile and enables just the first one
    /// </summary>
    public void InstatiateSeed()
    {
        if (seed == null)
            return;

        if (plantPosition == null)
            plantPosition = this.transform;

        int index = 0;

        //This way is easier for the performance so we just have to enable/disable later when changing stages

        foreach (GameObject plant in seed.plantStages)
        {
            plantObject = Instantiate(seed.plantStages[index], new Vector3(plantPosition.position.x, plantPosition.position.y + 0.15f, plantPosition.position.z), Quaternion.identity);

            plantObject.transform.parent = plantPosition.transform;

            //Add the objects to a list so we can manage them later

            plants.Add(plantObject);

            if (index != seedIndex)
            {
                plantObject.SetActive(false);
            }

            index++;
        }
    }

    /// <summary>
    /// Manages the grow state of the seed and if it's ready to be collected
    /// </summary>
    public void Grow()
    {
        if (seed == null)
            return;

        if (isReadyToCollect)
            return;

        //Updates the days that the plant has been without growing

        actualDays++;

        //If it's time to grow and it has been watered that day, then grow.

        if (actualDays >= seed.daysToChange && waterableSystem.CheckWaterState())
        {
            UpdatePlantState();
        }
    }

    private void UpdatePlantState()
    {
        //Update the model

        plants[seedIndex].SetActive(false);

        seedIndex++;

        plants[seedIndex].SetActive(true);

        actualDays = 0;

        //Check if it's ready to be collected depending on the stage it is on

        if (seedIndex >= seed.plantStages.Count - 1)
        {
            Debug.Log("Lista para recoger!!");
            isReadyToCollect = true;
        }
    }

    #endregion IPlantable Functions

    public void SaveData(SO_CropTile cropTile)
    {
        if (seed == null)
            return;

        cropTile.SavePlantData(actualDays, seedIndex, isReadyToCollect);
    }

    public void LoadData(SO_CropTile cropTile)
    {
        if (cropTile.seed == null)
            return;

        seed = cropTile.seed;
        actualDays = cropTile.actualDays;
        seedIndex = cropTile.seedIndex;
        isReadyToCollect = cropTile.isReadyToCollect;

        InstatiateSeed();
    }

    #region State Handlers

    /// <summary>
    /// Resets all the script and visuals of the plant
    /// </summary>
    public void DestroyPlants()
    {
        for (int i = plants.Count - 1; i >= 0; i--)
        {
            GameObject plant = plants[i];

            plants.Remove(plant);

            Destroy(plant);
        }

        isReadyToCollect = false;
        seed = null;
        actualDays = 0;
        seedIndex = 0;
    }

    public bool HandleState()
    {
        if (isReadyToCollect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion State Handlers

    #region Return Things

    public SO_Seed GetSeed()
    {
        return seed;
    }

    public override SO_Item CollectItem()
    {
        return seed.itemToCollect;
    }

    #endregion Return Things
}