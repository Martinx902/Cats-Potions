//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 27/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleIrrigation : MonoBehaviour, IWaterable
{
    #region Inspector Variables

    [SerializeField]
    private Material irrigatedMat;

    [SerializeField]
    private Material unIrrigatedMat;

    [SerializeField]
    private MeshRenderer meshRenderer;

    #endregion Inspector Variables

    #region Private Variables

    private bool isIrrigated = false;
    private IParticlePlayer particlePlayer;

    #endregion Private Variables

    private void Awake()
    {
        particlePlayer = GetComponent<IParticlePlayer>();
    }

    public void LoadStateData(bool _isIrrigated)
    {
        if (_isIrrigated)
        {
            isIrrigated = true;
            meshRenderer.material = irrigatedMat;
        }
    }

    public void WaterPlant()
    {
        isIrrigated = true;
        meshRenderer.material = irrigatedMat;

        if (particlePlayer != null)
            particlePlayer.PlayParticles();
    }

    //Checks the actual state of tile
    public bool CheckWaterState() => isIrrigated;

    public void ResetTile()
    {
        if (isIrrigated)
        {
            //Resets the materials
            meshRenderer.material = unIrrigatedMat;

            isIrrigated = false;
        }
    }
}