using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    //For Storing items in inventory
    [Header("UI Storage")]
    public List<ItemsObject> items = new List<ItemsObject>();
    public Dictionary<ItemsObject, InventorySlot> inventory = new Dictionary<ItemsObject, InventorySlot>();

    private InventorySlot selecteditem = null;

    [Header("Storage Capacity")]
    [SerializeField] private int maxStorage = 8;
    private int inventorytNum = 0;

    //For UI
    [Header("UI Display")]
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private GameObject inventoryCtrn;
    private bool inventoryClose = false;

    private void Start()
    {
        inventoryClose = true;
        inventoryCtrn.SetActive(false);
        inventoryBtn.onClick.AddListener(() => inventoryPanelController());
    }

    #region Inventory System

    public bool HaveSlot()
    {
        return items.Count < 8;
    }

    public void AddItem(ItemsObject item)
    {
        
 
        if (inventory.TryGetValue(item, out InventorySlot inventorySlot))
        {
            
            inventorySlot.AddQuantity(item.itemAmount);
        }
        else
        {
            
            InventorySlot newItemSlot = new InventorySlot();
            items.Add(item);
            inventory.Add(item, newItemSlot);
            newItemSlot.SetItem(item);
        }
        
        
        UpdateUI(item);
    }

    #endregion

    #region UI inventory
    private void UpdateUI(ItemsObject item)
    {
       Transform existingSlot = parentTransform.Find(item.itemName);

        if (existingSlot != null)
        {
            TMP_Text quantityText = existingSlot.GetChild(2).GetComponent<TMP_Text>();
            quantityText.text = inventory[item].quantity.ToString();
        }
        else
        {
            GameObject newSlot = Instantiate (itemContainer, parentTransform);
            newSlot.name = item.itemName;

            Image itemIcon = newSlot.transform.GetChild(1).GetComponent<Image>();   
            TMP_Text quantityTxt = newSlot.transform.GetChild(2).GetComponent<TMP_Text>();

            itemIcon.sprite = item.itemSprite;
            quantityTxt.text = inventory[item].quantity.ToString();

            Button button = newSlot.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnItemClick(item));
        }
    }

    private void inventoryPanelController()
    {
        if (inventoryClose)
        {
            inventoryCtrn.SetActive(true);
            inventoryClose = false;
        }
        else if (!inventoryClose)
        {
            inventoryCtrn.SetActive(false);
            inventoryClose = true;
        }
    }
    #endregion

    #region selection of Item
    private void OnItemClick(ItemsObject item)
    {
        if (selecteditem != null)
        {
            selecteditem.SetSelected(false);
            UpdateItemUI(selecteditem.inventoryItem);
        }

        selecteditem = inventory[item];
        selecteditem.SetSelected(true);
        Debug.Log($"Item selected : {item.itemName}");
        UpdateItemUI(item);
    }

    private void UpdateItemUI(ItemsObject item)
    {
        Transform itemSlot = parentTransform.Find(item.itemName);
        Image Background = itemSlot.GetChild(0).GetComponent<Image>();

        if (inventory[item].isSelected)
        {
            Background.color = Color.green;
        }
        else
        {
            Background.color = Color.white;
        }
    }
    #endregion
}