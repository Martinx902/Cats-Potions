using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [Header("Collectable Item")]
    [Space(15)]
    [SerializeField]
    protected SO_Item item;

    public abstract SO_Item CollectItem();
}
