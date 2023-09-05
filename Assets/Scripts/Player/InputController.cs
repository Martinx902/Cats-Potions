//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 03/03/2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private KeyCode inventoryKey = KeyCode.I;

    [Space(15)]
    [SerializeField]
    private KeyCode interactKey = KeyCode.E;

    [SerializeField]
    private KeyCode recipeBookKey = KeyCode.G;

    [SerializeField]
    private KeyCode pauseMenu = KeyCode.Escape;

    [Space(15)]
    public UnityEvent onInteract;

    [Space(15)]
    public UnityEvent onInventoryDisplay;

    [Space(15)]
    public UnityEvent onRecipeBookDisplay;

    [Space(15)]
    public UnityEvent onPause;

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            onInventoryDisplay.Invoke();
        }

        if (Input.GetKeyDown(interactKey))
        {
            onInteract.Invoke();
        }

        if (Input.GetKeyDown(recipeBookKey))
        {
            onRecipeBookDisplay.Invoke();
        }

        if (Input.GetKeyDown(pauseMenu))
        {
            onPause.Invoke();
        }
    }

    public void DisplayInventory()
    {
        onInventoryDisplay.Invoke();
    }

    public void DisplayRecipeBook()
    {
        onRecipeBookDisplay.Invoke();
    }
}