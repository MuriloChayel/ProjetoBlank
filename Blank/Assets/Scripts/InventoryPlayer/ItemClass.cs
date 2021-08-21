using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemProperties")]
public class ItemClass : ScriptableObject
{
    public Sprite sprite;
    public enum ItemType
    {
        chave,
        envelope,
        pedecabra,
    };
    public ItemType type;
}
