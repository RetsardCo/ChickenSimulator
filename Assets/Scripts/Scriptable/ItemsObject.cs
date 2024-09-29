using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Object/Item")]
public class ItemsObject:  ScriptableObject
{
    [field: SerializeField]
    public bool isStackable {  get; private set; }

    public int ID => GetInstanceID();

    [field: SerializeField]
    public string itemName {  get; private set; }


    [field: SerializeField]
    [field: TextArea]
    public string itemDescription { get; private set; }


    [field: SerializeField]
    public int itemPrice { get; private set; }


    [field: SerializeField]
    public int itemAmount { get; private set; }


    [field: SerializeField]
    public Sprite itemSprite { get; private set; }



    public ItemsObject(ItemsObject item)
    {
        this.itemName = item.itemName;
        this.itemSprite = item.itemSprite;
        this.itemAmount = item.itemAmount;
        this.isStackable = item.isStackable;
    }

}

