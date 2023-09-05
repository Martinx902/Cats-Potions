using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlantable
{
    public abstract void Plant(SO_Seed seedToPlant, Transform plantPosition);

    public abstract void Grow();

    public abstract bool HandleState();

    public void SaveData(SO_CropTile cropTileData);

    public void LoadData(SO_CropTile cropTileData);

    public abstract SO_Seed GetSeed();
}