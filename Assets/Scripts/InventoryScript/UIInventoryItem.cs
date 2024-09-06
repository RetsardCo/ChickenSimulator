using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantityText;

        [SerializeField]
        private TMP_Text title;

        [SerializeField]
        private TMP_Text description;

        [SerializeField]
        private Image borderImage;

        public GameObject itemDescription;

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnItemPointerEnter, OnItemPointerExit;

        [SerializeField]
        private bool empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        #region Event Method

        public void ResetData()
        {
            itemDescription.SetActive(false);
            itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            borderImage.enabled = false;
            print("border disabled");
        }

        public void SetData(Sprite sprite, int quantity, string itemName, string itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityText.text = quantity + "";
            title.text = itemName;
            description.text = itemDescription;
            empty = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
            print("selected");
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemPointerEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemPointerExit?.Invoke(this);
        }

        #endregion
    }
}