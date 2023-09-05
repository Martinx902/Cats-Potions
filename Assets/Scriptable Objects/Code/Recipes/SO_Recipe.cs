//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 24/03/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Recipe/New Simple Recipe"))]
public class SO_Recipe : ScriptableObject
{
    [SerializeField]
    public string recipeName = "Recipe Name";

    [SerializeField]
    [Multiline(5)]
    public string recipeDescription = "Recipe Description";

    [SerializeField]
    [Tooltip("Items needed to make the recipe")]
    public List<SO_Item> items = new List<SO_Item>();

    [SerializeField]
    public SO_Item resultItem = null;

    public bool isUnlocked = false;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}