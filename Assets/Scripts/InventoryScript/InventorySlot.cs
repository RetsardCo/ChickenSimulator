using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemsObject inventoryItem {  get; private set; }
    public int quantity { get; private set; } = 0;
    public bool isSelected { get; private set; } = false;

    public ItemsObject InventoryItem = null;


    public void SetItem(ItemsObject newObject)
    {
        inventoryItem = newObject;
        AddQuantity (inventoryItem.itemAmount);
    }

    public int AddQuantity(int addQuantity)
    {
    
        quantity += addQuantity;
        
        return quantity;

    }

    public void DeductQuantity(int deductQuantity)
    {
        quantity -= deductQuantity;
        Debug.Log($"{inventoryItem.itemName} : {quantity}");
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }
}
