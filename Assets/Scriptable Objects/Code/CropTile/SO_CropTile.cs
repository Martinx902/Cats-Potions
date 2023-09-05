using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Crop Tile / DataHolder"))]
[ExecuteAlways]
public class SO_CropTile : ScriptableObject
{
    public SO_Seed seed;

    public bool isOccupied;

    public bool isIrrigated;

    public int actualDays;

    public int seedIndex;

    public bool isReadyToCollect;

    public bool isUnlocked = true;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void UnlockCroptile()
    {
        isUnlocked = true;
    }

    public void SaveData(SO_Seed seed, bool isOccupied, bool isIrrigated)
    {
        this.seed = seed;
        this.isOccupied = isOccupied;
        this.isIrrigated = isIrrigated;
    }

    public void SavePlantData(int actualDays, int seedIndex, bool isReadyToCollect)
    {
        this.actualDays = actualDays;
        this.seedIndex = seedIndex;
        this.isReadyToCollect = isReadyToCollect;
    }

    public void ResetData()
    {
        seed = null;
        isOccupied = false;
        isIrrigated = false;
        actualDays = 0;
        seedIndex = 0;
        isReadyToCollect = false;

        Debug.Log("Crop Tile data reseted");
    }
}