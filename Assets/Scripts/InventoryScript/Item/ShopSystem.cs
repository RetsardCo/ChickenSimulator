using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    /*[SerializeField]
    private Item item;*/
    public void itemClicked()
    {
        Item item = GetComponent<Item>();
        print("dagdag");
        print(item.name);
        //inventoryData.AddItem(item.InventoryItem, item.Quantity);
        /*if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                return;
            item.Quantity = reminder;
        }*/
    }


}
