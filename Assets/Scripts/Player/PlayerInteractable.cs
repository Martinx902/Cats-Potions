//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 20/03/2023

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InteractableDetector))]
public class PlayerInteractable : MonoBehaviour
{
    #region Interact Checks

    [Space(10)]
    [Header("Interact Checks")]
    [Space(15)]
    [SerializeField]
    private bool canInteract = true;

    [Space(15)]
    [SerializeField]
    private KeyCode interactKey;

    [SerializeField]
    private float interactionTime = 0.2f;

    #endregion Interact Checks

    private Animator anim;

    private InteractableDetector interactableDetector;

    private void Start()
    {
        anim = transform.GetComponentInChildren<Animator>(true);
        interactableDetector = GetComponent<InteractableDetector>();
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            CheckInteractables();
        }
    }

    /// <summary>
    /// Gets the nearest interactable object and triggers their interact method
    /// </summary>
    private void CheckInteractables()
    {
        var nearestObject = interactableDetector.GetNearestInteractableObject();

        if (nearestObject == null)
            return;

        var interactable = nearestObject.GetComponent<IInteractable>();

        if (interactable == null) return;

        InteractAnim();

        interactable.Interact();
    }

    public void InteractAnim()
    {
        anim.SetTrigger("interact");
    }

    public void CanInteract(bool interact)
    {
        canInteract = interact;
    }
}