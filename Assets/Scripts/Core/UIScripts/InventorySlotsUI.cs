using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerDownHandler
{
    [Header("UI")]
    public GameObject highlight;
    public Image icon;
    public TextMeshProUGUI amount;

    [Header("Data link")]
    public int index;
    public SlotType type;

    public static InventorySlotUI draggedItem;

    private InventorySlot boundSlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        // просто для дебага, можно убрать
        // Debug.Log($"PointerDown: {name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (boundSlot == null || boundSlot.item == null)
            return;

        draggedItem = this;

        DragIconController.Instance.Show(icon.sprite);

        DragIconController.Instance.SetPosition(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG");

        Debug.Log($"Instance: {DragIconController.Instance}");

        if (DragIconController.Instance == null)
        {
            Debug.LogError("DRAG CONTROLLER NULL");

            return;
        }

        DragIconController.Instance
            .SetPosition(
                eventData.position
            );
    }

    public void OnEndDrag(
     PointerEventData eventData
 )
    {
        DragIconController.Instance.Hide();

        draggedItem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem == null)
            return;

        if (draggedItem == this)
            return;

        InventoryManager.Instance.SwapSlots(draggedItem.type, draggedItem.index, type, index);
        Debug.Log($"DROP {draggedItem.index} → {index}");
        DragIconController.Instance.Hide();

        draggedItem = null;
    }

    public void SetHighlight(bool value)
    {
        if (highlight != null)
            highlight.SetActive(value);
    }

    public void UpdateSlot(InventorySlot slot)
    {
        boundSlot = slot;

        if (slot == null || slot.item == null)
        {
            icon.enabled = false;

            if (amount != null)
                amount.gameObject.SetActive(false);

            return;
        }

        icon.enabled = true;
        icon.sprite = slot.item.icon;

        if (amount != null)
        {
            amount.gameObject.SetActive(slot.amount > 1);
            amount.text = slot.amount.ToString();
        }
        amount.text = slot.item.name;
    }

    public enum SlotType
    {
        Inventory,
        Hotbar,
        Equipment
    }
}