//Martin Pérez Villabrille
//Cat & Potions 
//Last Modification 01/12/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Crafting Layout/New LayOut"))]
public class SO_CraftingLayout : ScriptableObject
{
    [SerializeField]
    public string layOutName = "null";

    [SerializeField]
    public GameObject layOut = null;


}
