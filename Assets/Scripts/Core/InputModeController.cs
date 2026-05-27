using UnityEngine;

public class InputModeController : MonoBehaviour
{
    public static InputModeController instance;

    public enum GameMode
    {
        Gameplay,
        UI
    }

    public GameMode mode = GameMode.Gameplay;

    private void Awake()
    {
        instance = this;
    }

    public void SetGameplayMode()
    {
        mode = GameMode.Gameplay;

        GameInput.instance.EnableGameplayInput();    
    }

    public void SetUIMode()
    {
        mode = GameMode.UI;

        GameInput.instance.DisableGameplayInput();    
    }

    public bool IsGameplay() => mode == GameMode.Gameplay;
    public bool IsUI() => mode == GameMode.UI;
}