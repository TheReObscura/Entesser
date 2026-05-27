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
            if (icon != null)
                icon.enabled = false;

            return;
        }

        if (icon != null)
        {
            icon.enabled = true;
            icon.sprite = item.icon;
        }
    }
}