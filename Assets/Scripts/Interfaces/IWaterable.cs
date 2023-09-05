//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 27/11/2022

using UnityEngine;

public interface IWaterable
{
    public abstract void WaterPlant();

    public void LoadStateData(bool isIrrigated);

    public bool CheckWaterState();

    public void ResetTile();
}