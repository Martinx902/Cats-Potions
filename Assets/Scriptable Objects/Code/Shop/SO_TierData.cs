using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Shop/Tier Data"))]
public class SO_TierData : ScriptableObject
{
    public float actualExpends;

    public int actualTier;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}