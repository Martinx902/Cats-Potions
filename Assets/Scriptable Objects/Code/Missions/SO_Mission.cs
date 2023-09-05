//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 06/07/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Missions/New Mission")]
public class SO_Mission : ScriptableObject
{
    public bool isActive = false;

    public bool isCompleted = false;

    public List<SO_Item> itemsToComplete;

    public List<SO_Item> rewards;

    public UnityEvent onMissionComplete;

    public int missionIndex = 0;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}