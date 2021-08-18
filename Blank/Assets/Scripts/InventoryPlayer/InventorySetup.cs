using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySetup : MonoBehaviour
{
    public static InventorySetup Instance { get; private set; }
    public UI_inventory uI_Inventory;
    
    [SerializeField] public List<Item> itemList;

    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("itens"))
        {
            Item item = collision.GetComponent<Item>();
            AddItem(item);
        }
    }
    public void AddItem(Item item)
    {
        SetItemProperties(item);
        //print("tipo do item adquirido" + item.type);
        uI_Inventory.AddItemToCanvas(item);//newItem.sprite);
    }
    public void SetItemProperties(Item itemObj)
    {
        itemList.Add(itemObj);
    }
    public bool ReturnIfHaveThisItem(ItemClass.ItemType type)
    {
        for(int a = 0; a < itemList.Count; a++)
        {
            if (itemList[a].type == type)
                return true;
        }
        return false;
    }
    //CORRIGINDO
    public void UseKey()
    {
        for (int a = 0; a < itemList.Count; a++)
        {
            if (itemList[a].type == ItemClass.ItemType.chave)
            {
                uI_Inventory.UseItemFromCanvas(ItemClass.ItemType.chave);
                itemList.RemoveAt(a);
            }
        }
    }
    //CORRIGINDO
    public void UseCommunItem(int indexOfCommunItem, bool canUseThisItem)
    {
        if(canUseThisItem)
            itemList.RemoveAt(indexOfCommunItem);
    }
}
