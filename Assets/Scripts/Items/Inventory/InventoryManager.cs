using Assets.Scripts.Items;
using UnityEngine;
using static InventorySlotUI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory")]
    public InventorySlot[] inventory =
        new InventorySlot[25];

    [Header("Equipment")]
    public InventorySlot[] equipment =
        new InventorySlot[3];

    public InventorySlot quickPotion;

    [Header("External")]
    public InventoryUI ui;
    public HandItemView handView;

    [Header("Hotbar")]
    public int selectedHotbarSlot;

    const int HOTBAR_START = 20;
    const int HOTBAR_SIZE = 5;

    private void Awake()
    {
        Instance = this;

        Init(inventory);
        Init(equipment);

        quickPotion = new InventorySlot();
    }
    void Start()
    {
        RefreshHand();
    }
    void Init(InventorySlot[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }
    public InventorySlot GetHotbarSlot(int index)
    {
        return inventory[HOTBAR_START + index];
    }
    public InventorySlot GetSelectedSlot()
    {
        return GetHotbarSlot(selectedHotbarSlot);
    }
    public void ChangeHotbarSlot(float direction)
    {
        selectedHotbarSlot += direction > 0 ? 1 : -1;

        if (selectedHotbarSlot >= HOTBAR_SIZE)
            selectedHotbarSlot = 0;

        if (selectedHotbarSlot < 0)
            selectedHotbarSlot =
                HOTBAR_SIZE - 1;

        RefreshHand();
        RefreshUI();
    }
    public void RefreshHand()
    {
        if (handView == null)
            return;

        handView.SetItem(GetSelectedSlot().item);
    }
    public void RefreshUI()
    {
        if (ui == null)
            return;

        ui.UpdateUI();

        RefreshHand();
    }
    public void SwapSlots(InventorySlotUI.SlotType fromType, int fromIndex, InventorySlotUI.SlotType toType, int toIndex)
    {
        InventorySlot from = GetSlot(fromType, fromIndex);
        InventorySlot to = GetSlot(toType, toIndex);

        InventorySlot temp = new InventorySlot();

        temp.item = to.item;
        temp.amount = to.amount;

        to.item = from.item;
        to.amount = from.amount;

        from.item = temp.item;
        from.amount = temp.amount;

        Debug.Log(
            $"SWAP {fromIndex} ↔ {toIndex}\n" +
            $"FROM: {from.item?.name}\n" +
            $"TO: {to.item?.name}"
        );

        RefreshUI();
    }
    InventorySlot GetSlot(InventorySlotUI.SlotType type, int index)
    {
        switch (type)
        {
            case InventorySlotUI.SlotType.Inventory:
                return inventory[index];

            case InventorySlotUI.SlotType.Hotbar:
                return GetHotbarSlot(index);

            default:
                return null;
        }
    }
    public bool AddItem(ItemData item,int amount = 1)
    {
        foreach (var slot in inventory)
        {
            if (slot.item == item && slot.amount < item.maxStack)
            {
                slot.amount += amount;

                RefreshUI();

                return true;
            }
        }

        foreach (var slot in inventory)
        {
            if (slot.IsEmpty())
            {
                slot.item = item;
                slot.amount = amount;

                RefreshUI();

                return true;
            }
        }

        return false;
    }
    public void UseSlot(int index)
    {
        InventorySlot slot =
            GetHotbarSlot(index);

        if (
            slot == null ||
            slot.item == null
        )
            return;

        slot.item.Use();

        slot.amount--;

        if (
            slot.amount <= 0
        )
        {
            slot.Clear();
        }

        RefreshUI();
    }
}