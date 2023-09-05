using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UIManager : MonoBehaviour
{
    private List<Animator> buttonAnimators;

    public static S_UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        buttonAnimators = new List<Animator>();
    }

    public void AddAnimator(Animator anim)
    {
        buttonAnimators.Add(anim);
    }

    public void ClearSelected()
    {
        for (int i = 0; i < buttonAnimators.Count; i++)
        {
            buttonAnimators[i].SetBool("b_Selected", false);
        }
    }
}