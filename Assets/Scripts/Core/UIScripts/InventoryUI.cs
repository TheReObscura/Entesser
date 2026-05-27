using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Parents")]
    public Transform inventoryParent;

    public Transform activeParent; 

    public Transform hotbarParent; 

    public Transform equipmentParent;

    [Header("Prefab")]
    public InventorySlotUI slotPrefab;

    public InventorySlotUI quickPotionSlot;

    InventorySlotUI[] inventorySlots;

    InventorySlotUI[] activeSlots;

    InventorySlotUI[] hotbarSlots;

    InventorySlotUI[] equipmentSlots;

    const int INVENTORY_SIZE = 20;
    const int HOTBAR_SIZE = 5;

    void Start()
    {
        CreateUI();

        UpdateUI();
    }

    void CreateUI()
    {
        inventorySlots =
    CreateSlots(
        inventoryParent,
        INVENTORY_SIZE,
        InventorySlotUI.SlotType.Inventory
    );

        hotbarSlots =
            CreateSlots(
                hotbarParent,
                HOTBAR_SIZE,
                InventorySlotUI.SlotType.Hotbar
            );
        activeSlots =
            CreateSlots(
                activeParent,
                HOTBAR_SIZE,
                InventorySlotUI.SlotType.Hotbar
            );

        equipmentSlots =
            CreateSlots(
                equipmentParent,
                3,
                InventorySlotUI.SlotType.Equipment
            );
    }

    InventorySlotUI[] CreateSlots(
     Transform parent,
     int count,
     InventorySlotUI.SlotType type
 )
    {
        InventorySlotUI[] slots =
            new InventorySlotUI[count];

        for (int i = 0; i < count; i++)
        {
            var slot =
                Instantiate(
                    slotPrefab,
                    parent
                );

            slot.index = i;
            slot.type = type;

            slots[i] = slot;
        }

        return slots;
    }

    public void UpdateUI()
    {
        if (
        inventorySlots == null
        ||
        hotbarSlots == null
    )
        {
            Debug.Log(
                "UI not ready"
            );

            return;
        }

        var m =
            InventoryManager.Instance;

        if (m == null)
            return;

        // обычный инвентарь.
        for (int i = 0;i < INVENTORY_SIZE; i++)
        {
            inventorySlots[i].UpdateSlot(m.inventory[i]);
        }

        // active внутри окна E.
        for ( int i = 0; i < HOTBAR_SIZE; i++)
        {
            activeSlots[i].UpdateSlot(m.inventory[20 + i]);
        }

        // HUD.
        for (int i = 0;i < HOTBAR_SIZE; i++)
        {
            hotbarSlots[i].UpdateSlot(m.inventory[20 + i]);

            hotbarSlots[i].SetHighlight(i == m.selectedHotbarSlot);
        }

        // экипировка.
        for (int i = 0;i < 3;i++)
        {
            equipmentSlots[i].UpdateSlot(m.equipment[i]);
        }

        quickPotionSlot?.UpdateSlot(m.quickPotion);
    }
}