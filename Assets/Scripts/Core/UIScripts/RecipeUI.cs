using Assets.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image icon;

    private CraftRecipe recipe;

    public void Init(CraftRecipe r)
    {
        recipe = r;

        if (title != null)
            title.text = r.name;

        if (icon != null)
            icon.sprite = r.icon;
    }
}
