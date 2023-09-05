using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakePotionTrigger : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject throwPotionPanel;

    [SerializeField]
    private GameObject comeBackPanel;

    [SerializeField]
    private InventoryMainLists playerInventory;

    [SerializeField]
    private SO_Item magicPotion;

    [SerializeField]
    private ChangeSceneTrigger endCinematic;

    public void Interact()
    {
        throwPotionPanel.SetActive(true);
    }

    public void ThrowItem(bool yesOrNo)
    {
        if (yesOrNo)
        {
            if (playerInventory.mainInventoryList.Contains(magicPotion))
            {
                endCinematic.Trigger();
            }
            else
            {
                throwPotionPanel.SetActive(false);
                comeBackPanel.SetActive(true);
            }
        }
        else
        {
            throwPotionPanel.SetActive(false);
        }
    }
}