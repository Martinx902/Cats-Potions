//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 25/03/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndividualRecipeDisplayUI : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private TextMeshProUGUI recipeDescriptionTxt;

    [SerializeField]
    private Image recipeImg;

    [SerializeField]
    private List<Image> itemToCraft = new List<Image>();

    private SO_Recipe recipeToDisplay;

    #endregion Variables

    private void Start()
    {
        recipeToDisplay = null;
    }

    public void SetRecipeToDisplay(RecipeItemUI newRecipeToDisplay)
    {
        recipeToDisplay = newRecipeToDisplay.GetRecipe();

        DisplayIndividualRecipe();
    }

    /// <summary>
    ///  Displays an individual recipe on the UI Right Panel
    /// </summary>
    public void DisplayIndividualRecipe()
    {
        recipeDescriptionTxt.text = recipeToDisplay.recipeDescription;

        recipeImg.sprite = recipeToDisplay.resultItem.itemSprite;

        for (int i = 0; i < itemToCraft.Count; i++)
        {
            itemToCraft[i].sprite = recipeToDisplay.items[i].itemSprite;
        }
    }
}