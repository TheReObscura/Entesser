using Assets.Scripts.Items;
using UnityEngine;
using UnityEngine.UI;

public class HandItemView : MonoBehaviour
{
    [Header("UI icon in HUD or world")]
    public SpriteRenderer icon;

    public void SetItem(ItemData item)
    {
        if (item == null)
        {
            icon.enabled = false;
            return;
        }

        icon.enabled = true;

        if (item.icon != null)
            icon.sprite = item.icon;
    }
}