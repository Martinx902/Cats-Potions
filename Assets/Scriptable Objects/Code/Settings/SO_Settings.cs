using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Settings"))]
public class SO_Settings : ScriptableObject
{
    public float volume;

    public int qualityLevel = 2;

    public bool isFullScreen = true;

    public int resolutionIndex;
}