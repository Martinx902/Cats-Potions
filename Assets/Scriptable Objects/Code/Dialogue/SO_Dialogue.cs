//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 19/02/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = ("Dialogue/New Dialogue"))]
public class SO_Dialogue : ScriptableObject
{
    [SerializeField]
    public string npcName;

    [SerializeField]
    public Sprite npcImage;

    [SerializeField, TextArea(0, 5)]
    public string[] sentences;
}