using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
namespace Assets.Scripts.Core.SaveSystem
{
    public class SaveSlotUI : MonoBehaviour
    {
        public int slotId;

        public GameObject highlight;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI dateText;

        public void OnClick()
        {
            SaveSlotsMenu.instance.SetSelectedSlot(slotId);
        }

        public void SetHighlight(bool value)
        {
            if (highlight)
                highlight.SetActive(value);
        }

        public void Refresh()
        {
            if (SaveSystem.HasSave(slotId))
            {
                SaveData data = SaveSystem.Load(slotId);

                titleText.text = data.header.slotName;
                levelText.text = "Level " + data.header.level;
                dateText.text = data.header.lastSaveDate;
            }
            else
            {
                titleText.text = "Empty Slot";
                levelText.text = "";
                dateText.text = "";
            }
        }
    }

}
