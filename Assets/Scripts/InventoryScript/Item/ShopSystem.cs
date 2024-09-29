using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditorInternal.Profiling.Memory.Experimental;


public class ShopSystem : MonoBehaviour
{
    //Scirpt Reference
    private InventorySystem inventory;

    //Reference
    [SerializeField] private ItemsObject[] itemShops;
    [SerializeField] private GameObject itemContent;
    [SerializeField] private TextMeshProUGUI textPopUp;
    [SerializeField] private GameObject PopDescription;
    
    //prefab
    [SerializeField] private GameObject itemHolder;

    //data
    private GameObject instantiateItemDescript;

    private void Awake()
    {
        inventory = FindAnyObjectByType<InventorySystem>();
    }
    private void Start()
    {
  

        foreach (var item in itemShops)
        {
            GameObject newItem = Instantiate(itemHolder, itemContent.transform);

            SetupItemUI (newItem, item);
            
        }
    }

    //SETTING UP THE UI
    #region Set Up UI

    private void SetupItemUI(GameObject itemUI, ItemsObject item)
    {
        Button itemButton = itemUI.GetComponentInChildren<Button>();
        Image itemSprite = itemUI.GetComponentInChildren<Image>();
        Text itemNameText = itemUI.GetComponentInChildren<Text>();
        TextMeshProUGUI buttonText = itemButton.GetComponentInChildren<TextMeshProUGUI>();

        if (itemSprite != null)
        {
            itemSprite.sprite = item.itemSprite;
            OnHoverItem(itemSprite.gameObject, item);
        }

        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;
        }

        if (buttonText != null)
        {
            buttonText.text = $"{item.itemPrice}";
        }
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => OnItemClick(item));
        }

    }

    private void OnItemClick(ItemsObject item)
    {
        StartCoroutine(PopUptext(item));
    }

    #endregion

    //ON HOVER FUNCTION
    #region On Hover

    private void OnHoverItem(GameObject imageItem,  ItemsObject item)
    {
        EventTrigger eventTrigger = imageItem.AddComponent<EventTrigger>(); // Adding event Trigger in the item holder


        //Adding the event type (On Entry)
        EventTrigger.Entry onPointEntry = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter
        };
        onPointEntry.callback.AddListener((eventData) => OnHoverEnter(imageItem, item));

        //Adding the event type (On Exit)
        EventTrigger.Entry onPointExit = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerExit
        };
        onPointExit.callback.AddListener((eventData) => OnHoverExit());


        eventTrigger.triggers.Add(onPointEntry);
        eventTrigger.triggers.Add(onPointExit);

    }


    private void OnHoverEnter(GameObject imageItem, ItemsObject item)
    {
        instantiateItemDescript = Instantiate(PopDescription, imageItem.transform);
        TextMeshProUGUI textDescript = instantiateItemDescript.GetComponentInChildren<TextMeshProUGUI>();
        textDescript.text = item.itemDescription;
    }

    private void OnHoverExit()
    {
        if (instantiateItemDescript != null)
        {
            Destroy(instantiateItemDescript);
            instantiateItemDescript = null;
        }
    }   


    #endregion


    private IEnumerator PopUptext (ItemsObject item)
    {
        textPopUp.text = $"Purchased {item.itemName}";
        
        textPopUp.gameObject.SetActive(true);
        ValidationPurchase(item);
        yield return new WaitForSeconds(2);
        textPopUp.gameObject.SetActive(false);

    }

    private void ValidationPurchase(ItemsObject item)
    {
        if (inventory.HaveSlot())
        {
            inventory.AddItem(item);
        }
        else
        {
            textPopUp.text = $"Inventory full, failed to purchase";
        }
    }
}
