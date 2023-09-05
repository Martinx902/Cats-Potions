//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 21/02/2022

using UnityEngine;

[CreateAssetMenu(menuName = ("Items/New Simple Item"))]
public class SO_Item : ScriptableObject
{
    [SerializeField]
    public string itemName = "Item";

    [SerializeField]
    public int itemTier = 1;

    [SerializeField]
    [Multiline(6)]
    public string itemDescription = "This is an unfinished Item";

    [SerializeField]
    public string itemType = "Normal Item";

    [SerializeField]
    public int itemPrice = 10;

    [SerializeField]
    public Sprite itemSprite = null;
}