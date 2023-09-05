//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 20/03/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    #region Interaction Variables

    [Space(10)]
    [Header("Interact Checks")]
    [Space(15)]
    [SerializeField]
    [Tooltip("Position from which the interaction radius will expand")]
    private Transform interactCheckPosition;

    [Space(15)]
    [SerializeField, Range(0f, 7f)]
    private float interactCheckRadius = 1f;

    [Space(15)]
    [SerializeField]
    [Tooltip("Notification Pop-Up of the Player")]
    private GameObject interactionNotification;

    [Space(15)]
    [SerializeField]
    [Tooltip("Layer of the Interactables")]
    private LayerMask interactMask;

    #endregion Interaction Variables

    private GameObject nearestObject;

    private INotificable notificable;

    private void Update()
    {
        nearestObject = FindNearestInteractableObject();

        if (nearestObject != null)
            Notify();
        else
            EndNotify();
    }

    /// <summary>
    /// Triggers the notify method on the interactable object in question
    /// </summary>
    private void Notify()
    {
        notificable = nearestObject.GetComponent<INotificable>();

        interactionNotification.SetActive(true);

        if (notificable == null)
            return;

        notificable.Notify();
    }

    /// <summary>
    /// Untriggers the notify method on the interactable object in question
    /// </summary>
    private void EndNotify()
    {
        interactionNotification.SetActive(false);

        if (notificable == null)
            return;

        notificable.EndNotify();
    }

    public GameObject GetNearestInteractableObject() => nearestObject;

    /// <summary>
    /// Gets the closes interactable object within a certain radius
    /// </summary>
    /// <returns></returns>
    private GameObject FindNearestInteractableObject()
    {
        GameObject interactable;

        Collider[] interactableColliders;

        Vector3 distance;

        Vector3 tempDistance;

        //Creates a check sphere to see wich interactable objects are on it

        interactableColliders = Physics.OverlapSphere(interactCheckPosition.position, interactCheckRadius, interactMask);

        if (interactableColliders.Length == 0)
            return null;
        else if (interactableColliders.Length == 1)
            return interactableColliders[0].gameObject;
        else
        {
            //Asign the first value to start comparing to the others.
            interactable = interactableColliders[0].gameObject;
        }

        distance = interactableColliders[0].transform.position - transform.position;

        foreach (Collider collider in interactableColliders)
        {
            //Then with those objects we make a basic sorting algorithm
            // where it returns the closest one to the player

            tempDistance = collider.transform.position - transform.position;

            if (tempDistance.magnitude <= distance.magnitude)
            {
                interactable = collider.gameObject;
                distance = tempDistance;
            }
        }

        return interactable;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(interactCheckPosition.position, interactCheckRadius);
    }
}