using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core.SaveSystem
{
    public class SaveSlotsMenu : MonoBehaviour
    {
        public static SaveSlotsMenu instance;

        public SaveSlotUI[] slots;

        private int selectedSlot = -1;

        private void Awake()
        {
            instance = this;
        }
        public void Update()
        {
            RefreshAll();
        }

        public void SetSelectedSlot(int slotId)
        {
            selectedSlot = slotId;

            foreach (var slot in slots)
                slot.SetHighlight(slot.slotId == slotId);
        }

        public void Save()
        {
            Debug.Log("Selected slot: " + selectedSlot);
            if (selectedSlot < 0)
            {
                Debug.LogWarning("No slot selected!");
                return;
            }

            if (SaveManager.instance == null)
            {
                Debug.LogError("SaveManager is missing!");
                return;
            }
            SaveManager.instance.SaveGame(selectedSlot);
            RefreshAll();
        }

        public void Load()
        {
            if (selectedSlot < 0)
            {
                Debug.LogWarning("No slot selected");
                return;
            }

            SaveData data = SaveSystem.Load(selectedSlot);

            if (data == null)
                return;

            SaveTransfer.pendingLoad = data;

            SceneManager.LoadScene("GameScene");
        }
        public void Delete()
        {
            if (selectedSlot < 0) return;

            SaveSystem.Delete(selectedSlot);
            RefreshAll();
        }

        public void RefreshAll()
        {
            foreach (var slot in slots)
                slot.Refresh();
        }
    }
}
