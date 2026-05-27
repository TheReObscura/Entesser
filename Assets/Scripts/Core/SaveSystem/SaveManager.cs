using Assets.Scripts.Items;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

    public static class SaveTransfer
    {
        public static SaveData pendingLoad;
    }
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        public Player player;
        public PlayerLevelSystem levelSystem;
        public SaveData pendingLoad;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            if (SaveTransfer.pendingLoad != null)
            {
                ApplyData(SaveTransfer.pendingLoad);
                SaveTransfer.pendingLoad = null;
            }
        }
        public void SaveGame(int slotId)
        {

            if (player == null || levelSystem == null)
            {
                Debug.LogError("[SaveManager] Missing references!");
                return;
            }

            SaveData data = new SaveData();

            data.stats = player.stats;
            data.level = levelSystem.level;
            data.xp = levelSystem.currentXP;
            var inv =  InventoryManager.Instance;

            data.inventory = new InventorySlotSave[ inv.inventory.Length];

            for (int i = 0; i < inv.inventory.Length;i++)
            {
                data.inventory[i] =
                    new InventorySlotSave
                    {
                        itemId = inv.inventory[i].item != null ? inv.inventory[i].item.id : "",

                        amount = inv.inventory[i].amount
                    };
            }
            Chest[] all = FindObjectsByType<Chest>(FindObjectsSortMode.None);

            foreach ( var chest in all)
            {
                data.chests.Add(chest.GetSaveData());
            }
            data.selectedHotbarSlot = inv.selectedHotbarSlot;
            data.header = CreateHeader();

            SaveSystem.Save(slotId, data);
        }

        public void LoadGame(int slotId)
        {
            SaveData data = SaveSystem.Load(slotId);

            if (data == null)
                return;

            StartCoroutine(LoadSceneAndApply(data));
        }
        private IEnumerator LoadSceneAndApply(SaveData data)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");

            yield return null;

            player = FindFirstObjectByType<Player>();
            levelSystem = FindFirstObjectByType<PlayerLevelSystem>();

            ApplyData(data);
        }
        private void ApplyData(SaveData data)
        {
            player.stats = data.stats;

            levelSystem.level = data.level;

            levelSystem.currentXP = data.xp;

            player.stats.Init();

            var inv = InventoryManager.Instance;

            // игрок
            if (data.inventory != null)
            {
                for (int i = 0; i < inv.inventory.Length;i++)
                {
                    if (i >= data.inventory.Length)
                        break;

                    var slot = data.inventory[i];

                    if (string.IsNullOrEmpty(slot.itemId))
                    {
                        inv.inventory[i].Clear();

                        continue;
                    }

                    inv.inventory[i].item = ItemDatabase.Instance.GetItem(slot.itemId);

                    inv.inventory[i].amount = slot.amount;
                }

                inv.selectedHotbarSlot = data.selectedHotbarSlot;
            }

            // сундуки
            if (data.chests != null)
            {
                Chest[] chests = FindObjectsByType<Chest>(FindObjectsSortMode.None);

                foreach (var chestData in data.chests)
                {
                    foreach (var chest in chests)
                    {
                        if (chest.chestId != chestData.chestId)
                            continue;

                        chest.LoadData(chestData);

                        break;
                    }
                }
            }

            StartCoroutine(
                DelayedUIRefresh()
            );

            Debug.Log(
                "[SaveManager] Game loaded"
            );
        }
        IEnumerator DelayedUIRefresh()
        {
            yield return null;

            InventoryManager.Instance.RefreshUI();
        }
        private SaveSlotHeader CreateHeader()
        {
            return new SaveSlotHeader
            {
                slotName = "Slot",
                level = levelSystem.level,
                lastSaveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };
        }


    }

