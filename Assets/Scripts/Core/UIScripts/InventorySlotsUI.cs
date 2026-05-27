using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlotUI :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler
{
    [Header("UI")]
    public GameObject highlight;
    public Image icon;
    public TextMeshProUGUI amount;

    [Header("Data link")]
    public int index;
    public SlotType type;

    public static InventorySlotUI draggedItem;
    public static int draggedAmount;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slot = InventoryManager.Instance.GetSlot(type, index);

        if (slot == null || slot.item == null)
            return;

        draggedItem = this;

        bool shift =
            Keyboard.current != null &&
            Keyboard.current.leftShiftKey.isPressed;

        draggedAmount =
            shift
                ? Mathf.Max(1, slot.amount / 2)
                : slot.amount;

        if (DragIconController.Instance != null)
            DragIconController.Instance.Show(slot.item.icon);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragIconController.Instance == null)
            return;

        DragIconController.Instance.SetPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragIconController.Instance != null)
            DragIconController.Instance.Hide();

        draggedItem = null;
        draggedAmount = 0;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem == null)
            return;

        InventorySlot from =
            InventoryManager.Instance.GetSlot(
                draggedItem.type,
                draggedItem.index
            );

        InventorySlot to =
            InventoryManager.Instance.GetSlot(
                type,
                index
            );

        InventoryManager.Instance.TransferItem(
            from,
            to,
            draggedAmount
        );

        draggedItem = null;
        draggedAmount = 0;
    }
    public void SetHighlight(bool value)
    {
        if (highlight != null)
            highlight.SetActive(value);
    }

    public void UpdateSlot(InventorySlot slot)
    {
        if (slot == null || slot.item == null)
        {
            if (icon != null)
                icon.enabled = false;

            if (amount != null)
                amount.gameObject.SetActive(false);

            return;
        }

        if (icon != null)
        {
            icon.enabled = true;
            icon.sprite = slot.item.icon;
        }

        if (amount != null)
        {
            amount.gameObject.SetActive(slot.amount > 1);
            amount.text = slot.amount.ToString();
        }
    }

    public enum SlotType
    {
        Inventory,
        Hotbar,
        Equipment,
        Chest,
        Craft
    }
}