// Martin Pérez Villabrille
//Cat & Potions
//Last Modification 24/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    [SerializeField]
    private SO_RecipeBook recipeList;

    public void AddRecipeToList(SO_Recipe newRecipe)
    {
        if (!recipeList.recipeBook.Contains(newRecipe))
        {
            PopUpUI.instance.Show(PopUpType.NewRecipe, newRecipe.resultItem.itemSprite);
            recipeList.recipeBook.Add(newRecipe);
        }
    }
}