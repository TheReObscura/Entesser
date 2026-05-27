using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public enum ItemType
    {
        Material,
        Consumable,
        Weapon,
        Quest,
        SpellBook,
        Amulet, 
        Hands,
        Boots,
        Potions
    }

    [CreateAssetMenu(fileName = "Item",menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;

        public Sprite icon;

        public ItemType type;

        [Min(1)]
        public int maxStack = 1;

        public bool usable;
        [Header("Save")]
        public string id;
        public virtual void Use()
        {

        }
    }

    [CreateAssetMenu(fileName = "Potion",menuName = "Inventory/Potion")]
    public class PotionData : ItemData
    {
        public int heal;
        public override void Use()
        {
            var player =
                FindFirstObjectByType<Player.Player>();

            player.stats.Heal(
                heal
            );
            Debug.Log("THAT BITCH HEALED");
        }
    }
}
