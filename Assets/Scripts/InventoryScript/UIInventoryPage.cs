using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private MouseFollower mouseFollower;

        private float addWidth = 150f;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentlyDragItemIndex = -1;

        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        public void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
        }
        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
                contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x + addWidth, contentPanel.sizeDelta.y);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemPointerEnter += handleItemHover;
                uiItem.OnItemPointerExit += handleExitItemHover;
            }
        }

        public void UpdateDescription(int itemIdex)
        {
            DeselectAllItems();
            listOfUIItems[itemIdex].Select();
        }

        public void UpdateData(int itemIdex, Sprite sprite, int itemQuantity, string itemName, string itemDescription)
        {
            if (listOfUIItems.Count > itemIdex)
            {
                listOfUIItems[itemIdex].SetData(sprite, itemQuantity, itemName, itemDescription);
            }
        }

        #region Event Method

        private void handleItemHover(UIInventoryItem inventoryItemUI)
        {
            inventoryItemUI.itemDescription.SetActive(true);

        }
        private void handleExitItemHover(UIInventoryItem inventoryItemUI)
        {
            inventoryItemUI.itemDescription.SetActive(false);
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDragItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDragItemIndex = -1;
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDragItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity, string itemName, string itemDescription)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity, itemName, itemDescription);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
            //print(inventoryItemUI.name);
            listOfUIItems[index].Select();
            print(index);
            //inventoryItemUI.itemDescription.SetActive(true);
        }

        #endregion

        public void additem()
        {
            if (listOfUIItems.Count >= 6) return;
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x + addWidth, contentPanel.sizeDelta.y);
            listOfUIItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemPointerEnter += handleItemHover;
            uiItem.OnItemPointerExit += handleExitItemHover;
        }

        private void panelAddWidth()
        {
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x + addWidth * listOfUIItems.Count, contentPanel.sizeDelta.y);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
            /*listOfUIItems[0].SetData(sprite, quantity, itemName, itemDescription);
            listOfUIItems[1].SetData(sprite2, quantity, itemName, itemDescription);*/
        }

        public void ResetSelection()
        {
            print("deselected");
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}