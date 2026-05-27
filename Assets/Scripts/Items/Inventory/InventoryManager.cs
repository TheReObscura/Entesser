using Assets.Scripts.Items;
using UnityEngine;
using static InventorySlotUI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Craft")]
    public InventorySlot[] craft =
    new InventorySlot[8];
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
        Init(craft);
        quickPotion = new InventorySlot();
    }

    private void Start()
    {
        RefreshUI();
    }

    void Init(InventorySlot[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i] = new InventorySlot();
    }



    public InventorySlot GetSlot(SlotType type, int index)
    {
        switch (type)
        {
            case SlotType.Inventory:
                if (index < 0 || index >= inventory.Length) return null;
                return inventory[index];

            case SlotType.Hotbar:
                if (index < 0 || index >= HOTBAR_SIZE) return null;
                return GetHotbarSlot(index);

            case SlotType.Chest:
                if (InventoryUI.Instance == null ||
                    InventoryUI.Instance.openedChest == null)
                    return null;

                if (index < 0 || index >= InventoryUI.Instance.openedChest.slots.Length)
                    return null;

                return InventoryUI.Instance.openedChest.slots[index];

            case SlotType.Craft:
                if (index < 0 || index >= craft.Length)
                    return null;

                return craft[index];
        }

        return null;
    }

    public InventorySlot GetHotbarSlot(int index)
    {
        return inventory[HOTBAR_START + index];
    }

    public InventorySlot GetSelectedSlot()
    {
        return GetHotbarSlot(selectedHotbarSlot);
    }

    public void TransferItem(InventorySlot from, InventorySlot to, int amount)
    {
        if (from == null || from.item == null)
            return;

        if (to == null)
            return;

        if (amount <= 0)
            return;

        if (to.item == null)
        {
            to.item = from.item;
            to.amount = amount;

            from.amount -= amount;

            if (from.amount <= 0)
                from.Clear();

            RefreshUI();
            return;
        }

        if (from.item == to.item)
        {
            int space = to.item.maxStack - to.amount;
            int add = Mathf.Min(space, amount);

            to.amount += add;
            from.amount -= add;

            if (from.amount <= 0)
                from.Clear();

            RefreshUI();
            return;
        }

        ItemData tempItem = to.item;
        int tempAmount = to.amount;

        to.item = from.item;
        to.amount = from.amount;

        from.item = tempItem;
        from.amount = tempAmount;

        RefreshUI();

    }


    public void Move(SlotType fromType, int fromIndex, SlotType toType, int toIndex, int amount)
    {
        InventorySlot from = GetSlot(fromType, fromIndex);
        InventorySlot to = GetSlot(toType, toIndex);

        TransferItem(from, to, amount);
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        for (int i = 0; i < inventory.Length && amount > 0; i++)
        {
            var slot = inventory[i];

            if (slot.item == item && slot.amount < item.maxStack)
            {
                int space = item.maxStack - slot.amount;
                int add = Mathf.Min(space, amount);

                slot.amount += add;
                amount -= add;
            }
        }

        for (int i = 0; i < inventory.Length && amount > 0; i++)
        {
            var slot = inventory[i];

            if (slot.item == null)
            {
                int add = Mathf.Min(item.maxStack, amount);

                slot.item = item;
                slot.amount = add;

                amount -= add;
            }
        }

        RefreshUI();
        return amount <= 0;
    }

    public void UseSlot(int index)
    {
        InventorySlot slot = GetHotbarSlot(index);

        if (slot == null || slot.item == null)
            return;

        slot.item.Use();

        slot.amount--;

        if (slot.amount <= 0)
            slot.Clear();

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (ui != null)
            ui.UpdateUI();


        CraftUI.Instance.UpdateUI();
        RefreshHand();
    }

    public void RefreshHand()
    {
        if (handView == null)
            return;

        handView.SetItem(GetSelectedSlot().item);
    }

    public void ChangeHotbarSlot(float direction)
    {
        selectedHotbarSlot += direction > 0 ? 1 : -1;

        if (selectedHotbarSlot >= HOTBAR_SIZE)
            selectedHotbarSlot = 0;

        if (selectedHotbarSlot < 0)
            selectedHotbarSlot = HOTBAR_SIZE - 1;

        RefreshHand();
        RefreshUI();
    }
}