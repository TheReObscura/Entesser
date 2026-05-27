using Assets.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventorySlotUI;

public class CraftUI : MonoBehaviour
{
    public static CraftUI Instance;

    [Header("Parents")]
    public Transform recipeParent;
    public Transform craftParent;

    [Header("Prefabs")]
    public RecipeUI recipePrefab;
    public InventorySlotUI slotPrefab;

    [Header("Data")]
    public CraftRecipe[] recipes;
    public ItemData resultPreview;
    private InventorySlotUI[] craftSlots;
    public Image resultIcon;
    private CraftRecipe currentRecipe;
    private void Awake()
    {
        Instance = this;
        CreateCraftSlots();
        CreateRecipeList();
    }

    void CreateCraftSlots()
    {
        craftSlots = new InventorySlotUI[8];

        for (int i = 0; i < craftSlots.Length; i++)
        {
            craftSlots[i] =
                Instantiate(slotPrefab, craftParent);

            craftSlots[i].index = i;
            craftSlots[i].type = SlotType.Craft;

            craftSlots[i].SetHighlight(false);
        }
    }

    void CreateRecipeList()
    {
        if (recipes == null)
        {
            Debug.LogWarning("No recipes assigned");
            return;
        }

        foreach (var r in recipes)
        {
            RecipeUI ui =
                Instantiate(recipePrefab, recipeParent);

            ui.Init(r);
        }
    }
    public void CheckRecipe()
    {
        currentRecipe = null;

        foreach (var recipe in recipes)
        {
            if (Matches(recipe))
            {
                currentRecipe = recipe;

                resultIcon.enabled = true;
                resultIcon.sprite = recipe.result.icon;

                return;
            }
        }

        resultIcon.enabled = false;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < craftSlots.Length; i++)
        {
            
            var slot = InventoryManager.Instance.GetSlot(SlotType.Craft, i);
            craftSlots[i].UpdateSlot(slot);
        }

        CheckRecipe();
    }
    bool Matches(CraftRecipe recipe)
    {
        List<InventorySlot> usedSlots =
            new List<InventorySlot>();

        foreach (var ingredient in recipe.ingredients)
        {
            int remaining = ingredient.amount;

            foreach (var slot in InventoryManager.Instance.craft)
            {
                if (slot.item != ingredient.item)
                    continue;

                remaining -= slot.amount;

                if (remaining <= 0)
                    break;
            }

            if (remaining > 0)
                return false;
        }

        return true;
    }

    public void Craft()
    {
        if (currentRecipe == null)
            return;

        foreach (var ingredient in currentRecipe.ingredients)
        {
            int remaining = ingredient.amount;

            foreach (var slot in InventoryManager.Instance.craft)
            {
                if (slot.item != ingredient.item)
                    continue;

                int take =
                    Mathf.Min(slot.amount, remaining);

                slot.amount -= take;
                remaining -= take;

                if (slot.amount <= 0)
                    slot.Clear();

                if (remaining <= 0)
                    break;
            }
        }

        InventoryManager.Instance.AddItem(
            currentRecipe.result,
            currentRecipe.resultAmount
        );

        InventoryManager.Instance.RefreshUI();

        CheckRecipe();
    }

}