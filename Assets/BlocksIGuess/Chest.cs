using Assets.Scripts.Items;
using UnityEngine;

public class Chest :
    MonoBehaviour,
    IInteractable
{
    public InventorySlot[] slots = new InventorySlot[15];
    public string chestId;
    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public bool Interact()
    {
        InventoryUI.Instance.OpenChest(this);

        return true;
    }
    public void LoadData(ChestSaveData data)
    {
        for (int i = 0; i < slots.Length;i++)
        {
            if (i >=data.slots.Length)
                break;

            var slot = data.slots[i];

            if (string.IsNullOrEmpty(slot.itemId))
            {
                slots[i].Clear();

                continue;
            }

            slots[i].item = ItemDatabase.Instance.GetItem(slot.itemId);

            slots[i].amount = slot.amount;
        }
    }
    public ChestSaveData GetSaveData()
    {
        ChestSaveData data = new ChestSaveData();

        data.chestId = chestId;

        data.slots = new InventorySlotSave[slots.Length];

        for (int i = 0;i < slots.Length; i++)
        {
            data.slots[i] = new InventorySlotSave
                {
                    itemId =slots[i].item?.id,amount =slots[i].amount
                };
        }

        return data;
    }

}