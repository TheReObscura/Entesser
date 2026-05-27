using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject loadPanel;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }


    // ЗАГРУЗИТЬ
    public void OpenLoad()
    {
        mainMenuPanel.SetActive(false);
        loadPanel.SetActive(true);
    }

    public void CloseLoad()
    {
        loadPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    // НАСТРОЙКИ
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // ВЫЙТИ
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
