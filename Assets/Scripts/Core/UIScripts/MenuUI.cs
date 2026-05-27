using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core.UIScripts
{
    class MenuUI : MonoBehaviour
    {
        public GameObject menuPanel;
        public GameObject settingsPanel;
        public GameObject savePanel;

        public void OpenSettings()
        {
            settingsPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        public void CloseSettings()
        {
            settingsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }

        public void OpenSave()
        {
            savePanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        public void CloseSave()
        {
            savePanel.SetActive(false);
            menuPanel.SetActive(true);
        }
        public void LeaveGame()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public void SaveGame()
        {
        }
        public void DeleteSave()
        {
        }
    }
}
