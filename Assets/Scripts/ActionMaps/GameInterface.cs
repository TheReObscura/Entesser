using UnityEngine;

public class GameInterface : MonoBehaviour
{
    InterfaceInput interfaceInput;
    public static GameInterface instance { get; private set; }

    private void Awake()
    {
        instance = this;
        interfaceInput = new InterfaceInput();
        interfaceInput.Enable();
    }
    public void EnableUIInput() => interfaceInput.Enable();
    public void DisableUIInput() => interfaceInput.Disable();

    void Update()
    {
        var context = GetCurrentContext();

        if (interfaceInput.Interface.Inventory.WasPressedThisFrame())
            UIManager.instance.ToggleWindow(WindowType.Inventory);

        if (interfaceInput.Interface.Menu.WasPressedThisFrame())
        {
            if (UIManager.instance.IsAnyWindowOpen())
                UIManager.instance.CloseAll();
            else
                UIManager.instance.OpenWindow(WindowType.Menu);
        }

        if (interfaceInput.Interface.Characteristics.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Characteristics, context);

        if (interfaceInput.Interface.Crafting.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Crafting, context);

        if (interfaceInput.Interface.Map.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Map, context);

        if (interfaceInput.Interface.Directory.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Directory, context);

        if (interfaceInput.Interface.Diary.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Diary, context);

        if (interfaceInput.Interface.Achievements.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Achievements, context);

        if (interfaceInput.Interface.Spells.WasPressedThisFrame())
            UIManager.instance.TryToggleWindow(WindowType.Spells, context);
    }

    ContextItem GetCurrentContext()
    {
        if (Fire.instance != null && Fire.instance.GetActive())
            return ContextItem.Fire;

        if (Book.instance != null && Book.instance.GetActive())
            return ContextItem.Book;

        if (Sphere.instance != null && Sphere.instance.GetActive())
            return ContextItem.Sphere;

        return ContextItem.None;
    }
}