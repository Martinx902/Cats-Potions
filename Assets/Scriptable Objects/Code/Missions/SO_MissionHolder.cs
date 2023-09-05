using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/New MissionHolder")]
public class SO_MissionHolder : ScriptableObject
{
    public SO_Mission mission;

    public SO_Dialogue questDialogue;

    public SO_Dialogue gratitudeDialogue;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}