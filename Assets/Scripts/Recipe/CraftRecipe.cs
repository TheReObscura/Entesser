using Assets.Scripts.Items;
using UnityEngine;

[System.Serializable]
public class RecipeIngredient
{
    public ItemData item;
    public int amount;
}
[CreateAssetMenu(fileName = "Recipe", menuName = "Craft/Recipe")]
public class CraftRecipe : ScriptableObject
{
    public string recipeName;

    public RecipeIngredient[] ingredients;

    public ItemData result;
    public int resultAmount = 1;

    public Sprite icon;
}