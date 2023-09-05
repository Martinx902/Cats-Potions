using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Recipe/Recipe Book"))]
public class SO_RecipeBook : ScriptableObject
{
    //Saves the unlocked recipes by the player

    public List<SO_Recipe> recipeBook;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}