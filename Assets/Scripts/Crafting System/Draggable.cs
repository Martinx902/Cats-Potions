//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 29/11/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentAfterDrag;

    [SerializeField]
    private Image itemImg;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemImg.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Vector3.Lerp(transform.position, Input.mousePosition, 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BackToOrigin();
        AudioManager.instance.PlayClip(SoundsFX.SFX_Craft);
        itemImg.raycastTarget = true;
    }

    public void BackToOrigin()
    {
        transform.SetParent(parentAfterDrag);
        itemImg.raycastTarget = true;
    }
}