using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable : IInteractable
{
    SO_Item GetItem();
}
