using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.Overlays;
using UnityEngine;

namespace Assets.Scripts.Core.SaveSystem
{
    public static class SaveSystem
    {
        private static string GetPath(int slotId)
        {
            return Path.Combine(Application.persistentDataPath, $"save_{slotId}.json");
        }

        public static void Save(int slotId, SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetPath(slotId), json);

            Debug.Log($"[SaveSystem] Saved slot {slotId}");
        }

        public static SaveData Load(int slotId)
        {
            string path = GetPath(slotId);

            if (!File.Exists(path))
            {
                Debug.LogWarning($"[SaveSystem] No save file for slot {slotId}");
                return null;
            }

            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //Debug.Log($"[SaveSystem] Loaded slot {slotId}");
            return data;
        }

        public static bool HasSave(int slotId)
        {
            return File.Exists(GetPath(slotId));
        }

        public static void Delete(int slotId)
        {
            string path = GetPath(slotId);

            if (File.Exists(path))
                File.Delete(path);
        }
    }

    [Serializable]
    public class SaveData
    {
        public SaveSlotHeader header;

        public PlayerStats stats;
        public int level;
        public int xp;

        public InventorySlotSave[] inventory;

        public int selectedHotbarSlot;
    }

    [Serializable]
    public class SaveSlotHeader
    {
        public string slotName;
        public int level;
        public string lastSaveDate;
    }

    [Serializable]
    public class InventorySlotSave
    {
        public string itemId;

        public int amount;
    }
}
