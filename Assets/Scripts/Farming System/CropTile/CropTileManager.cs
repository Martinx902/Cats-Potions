//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 20/04/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTileManager : MonoBehaviour
{
    [SerializeField]
    private List<CropTile> tiles;

    [SerializeField]
    private Inventory playerInventory;

    private CropTile tileBeingUsed;

    /// <summary>
    /// Saves a reference to the tile that is being interacted with in order to plant a seed there
    /// </summary>
    /// <param name="tile"></param>
    public void SelectTile(CropTile tile)
    {
        tileBeingUsed = tile;
    }

    /// <summary>
    /// Selects the tile that is being saved and plants a seed there, given by the UI
    /// </summary>
    /// <param name="seedReceived"></param>
    public void PlantOnTile(ItemUI seedReceived)
    {
        AudioManager.instance.PlayClip(SoundsFX.SFX_Harvest);

        tileBeingUsed.InstantiateSeed((SO_Seed)seedReceived.GetItem());

        playerInventory.Remove(seedReceived.GetItem());
    }
}