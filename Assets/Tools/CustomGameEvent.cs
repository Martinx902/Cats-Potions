using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueCustomEvent : UnityEvent<SO_Dialogue>
{
}

[System.Serializable]
public class ItemCustomEvent : UnityEvent<SO_Item>
{
}