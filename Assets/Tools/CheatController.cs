using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatController : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInventory;

    [SerializeField]
    private List<SO_Item> items;

    [SerializeField]
    private GameObject controlsPanel;

    [SerializeField]
    private GameObject recipePanel;

    [SerializeField]
    private GameObject craftingDisplay;

    private void Update()
    {
    }

    public void AddItems()
    {
        foreach (SO_Item item in items)
        {
            playerInventory.AddItem(item);
        }
    }

    public void ShowControls()
    {
        if (controlsPanel.activeInHierarchy)
        {
            controlsPanel.SetActive(false);
        }
        else
        {
            controlsPanel.SetActive(true);
        }
    }

    public void ShowRecipe()
    {
        if (recipePanel.activeInHierarchy)
        {
            recipePanel.SetActive(false);
        }
        else
        {
            recipePanel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        craftingDisplay.SetActive(false);
    }
}