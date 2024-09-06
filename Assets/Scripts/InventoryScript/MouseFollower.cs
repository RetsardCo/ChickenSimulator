using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private UIInventoryItem item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity, string itemName, string itemDescription)
    {
        item.SetData(sprite, quantity, itemName, itemDescription);
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
            );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        print($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
