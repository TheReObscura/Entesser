using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class ItemDatabase :
        MonoBehaviour
    {
        public static ItemDatabase Instance;

        public List<ItemData> items;

        void Awake()
        {
            Instance = this;
        }

        public ItemData GetItem(
            string id
        )
        {
            foreach (
                var item
                in items
            )
            {
                if (
                    item.id ==
                    id
                )
                    return item;
            }

            return null;
        }
    }
}