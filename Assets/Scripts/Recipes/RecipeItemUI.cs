//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 25/03/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeItemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI recipeNameTxt;

    [SerializeField]
    private Image recipeImg;

    private SO_Recipe recipe;

    public void SetRecipe(SO_Recipe newRecipe)
    {
        recipe = newRecipe;
    }

    public void DisplayRecipe()
    {
        recipeNameTxt.text = recipe.recipeName;

        recipeImg.sprite = recipe.resultItem.itemSprite;
    }

    public SO_Recipe GetRecipe() => recipe;
}