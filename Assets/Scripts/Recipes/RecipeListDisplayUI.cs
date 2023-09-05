//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 25/03/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListDisplayUI : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform instatiateRecipePosition;

    [SerializeField]
    private GameObject recipePrefab;

    [SerializeField]
    private SO_RecipeBook recipeBook;

    private List<SO_Recipe> newRecipes = new List<SO_Recipe>();

    #endregion Variables

    /// <summary>
    /// Displays Recipe List on the UI
    /// </summary>
    public void DisplayList()
    {
        //Check to see if we have new recipes on the list
        if (recipeBook.recipeBook.Count == newRecipes.Count)
            return;

        //If there are, just instatiate them in the list
        for (int i = newRecipes.Count; i < recipeBook.recipeBook.Count; i++)
        {
            GameObject newRecipe = Instantiate(recipePrefab, instatiateRecipePosition);

            newRecipe.SetActive(true);

            RecipeItemUI newRecipeUI = newRecipe.GetComponent<RecipeItemUI>();

            newRecipeUI.SetRecipe(recipeBook.recipeBook[i]);

            newRecipeUI.DisplayRecipe();

            newRecipes.Add(recipeBook.recipeBook[i]);
        }
    }

    private void OnEnable()
    {
        DisplayList();
    }
}