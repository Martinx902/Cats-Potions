// Martin Pérez Villabrille
//Cat & Potions
//Last Modification 29/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingInventorySlot : MonoBehaviour, IDropHandler
{
    private Draggable draggableComponent;

    private ItemUI droppedItem;
    private ItemUI actualItem;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 || !transform.GetChild(0).gameObject.activeInHierarchy)
        {
            GameObject droppedObject = eventData.pointerDrag;

            draggableComponent = droppedObject.GetComponent<Draggable>();

            droppedItem = droppedObject.GetComponent<ItemUI>();

            actualItem = GetComponentInChildren<ItemUI>(true);

            actualItem.item = droppedItem.item;

            draggableComponent.BackToOrigin();
            droppedObject.SetActive(false);
            droppedItem.CleanItem();

            AudioManager.instance.PlayClip(SoundsFX.SFX_Craft);

            actualItem.gameObject.SetActive(true);
            actualItem.Display();
        }
    }
}