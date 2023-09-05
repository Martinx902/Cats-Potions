using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class S_ButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator anim;

    [SerializeField]
    private bool canBeSelected = true;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (S_UIManager.instance != null)
        {
            if (canBeSelected == true)
            {
                S_UIManager.instance.AddAnimator(anim);
            }
        }
        else
        {
            Debug.LogError("UI Manager not found");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("b_Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("b_Hover", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("b_Pressed", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetBool("b_Pressed", false);
    }

    public void SelectDeselectButton()
    {
        if (canBeSelected)
        {
            if (anim.GetBool("b_Selected") == false)
            {
                S_UIManager.instance.ClearSelected();
                anim.SetBool("b_Selected", true);
            }
            else
            {
                anim.SetBool("b_Selected", false);
            }
        }
    }
}