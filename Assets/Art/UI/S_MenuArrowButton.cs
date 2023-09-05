using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MenuArrowButton : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetParameter(bool value)
    {
        if(animator != null)
        {
            animator.SetBool("b_Inventory", value);
        }
    }
}
