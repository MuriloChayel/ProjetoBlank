using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_inventory : MonoBehaviour
{
    public static UI_inventory Instance { get; private set; }

    public Transform container;
    public Transform slot;
    public Image img;
    public List<Transform> icons;

    public void AddItemToCanvas(Item item)
    {
        Transform icon = Instantiate(slot, container.transform, false).transform;
        icon.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        icon.gameObject.AddComponent<iconTypes>();
        icon.gameObject.GetComponent<iconTypes>().type = item.type;
        icons.Add(icon);
    }
    //CORRIGINDO
    public void UseItemFromCanvas(ItemClass.ItemType type)
    {
        switch (type)
        {
            case ItemClass.ItemType.chave:
                for(int a = 0; a < icons.Count; a++)
                {
                    WalkInList(a);
                }
                break;
            case ItemClass.ItemType.envelope:

                break;
            default:
                return;
        }
    }
    public void WalkInList(int index)
    {
        if (icons[index].GetComponent<iconTypes>().type == ItemClass.ItemType.chave)
        {
            Destroy(icons[index].gameObject);
            icons.RemoveAt(index);
        }
    }
}
