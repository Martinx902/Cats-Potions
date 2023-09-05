//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 13/05/2023

using UnityEngine;

[CreateAssetMenu(menuName = ("Advice / New Advice"))]
public class SO_Advice : ScriptableObject
{
    public string adviceTitle;

    [TextArea(1, 5)]
    public string advice;

    public float weight;
}