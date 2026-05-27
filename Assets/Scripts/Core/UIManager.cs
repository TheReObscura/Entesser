using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject inventory;
    public GameObject crafting;
    public GameObject diary;
    public GameObject directory;
    public GameObject menu;
    public GameObject map;
    public GameObject achievements;
    public GameObject characteristics;
    public GameObject settings;
    public GameObject save;

    private WindowType currentWindow = WindowType.None;

    private Dictionary<WindowType, ContextItem> windowRules;

    private void Awake()
    {
        instance = this;

        windowRules = new Dictionary<WindowType, ContextItem>
        {
            { WindowType.Inventory, ContextItem.None },
            { WindowType.Crafting, ContextItem.Fire },
            { WindowType.Map,ContextItem.None },
            { WindowType.Directory, ContextItem.Book },
            { WindowType.Diary, ContextItem.Book },
            { WindowType.Achievements, ContextItem.None },
            { WindowType.Characteristics, ContextItem.Fire },
            { WindowType.Menu, ContextItem.None },
            { WindowType.Settings, ContextItem.None },
            {WindowType.Save, ContextItem.None  },
        };
    }

    public void TryToggleWindow(WindowType type, ContextItem context)
    {
        if (!windowRules.ContainsKey(type))
            return;

        ContextItem required = windowRules[type];

        if (required != ContextItem.None && required != context)
            return;

        ToggleWindow(type);
    }

    public void ToggleWindow(WindowType type)
    {
        if (currentWindow == type)
        {
            CloseAll();
            return;
        }

        OpenWindow(type);
    }

    public void OpenWindow(WindowType type)
    {
        CloseAllInternal();

        currentWindow = type;

        if (type == WindowType.Inventory && inventory)
            inventory.SetActive(true);
        else if (type == WindowType.Crafting && crafting)
            crafting.SetActive(true);
        else if (type == WindowType.Map && map)
            map.SetActive(true);
        else if (type == WindowType.Directory && directory)
            directory.SetActive(true);
        else if (type == WindowType.Diary && diary)
            diary.SetActive(true);
        else if (type == WindowType.Achievements && achievements)
            achievements.SetActive(true);
        else if (type == WindowType.Characteristics && characteristics)
            characteristics.SetActive(true);
        else if (type == WindowType.Menu && menu)
            menu.SetActive(true);
        else if (type == WindowType.Settings && settings)
            settings.SetActive(true);
        else if (type == WindowType.Save && save)
            save.SetActive(true);

        UpdateInputMode();
    }

    public void CloseAll()
    {
        currentWindow = WindowType.None;
        CloseAllInternal();
        UpdateInputMode();
    }

    private void CloseAllInternal()
    {
        if (inventory) inventory.SetActive(false);
        if (crafting) crafting.SetActive(false);
        if (diary) diary.SetActive(false);
        if (directory) directory.SetActive(false);
        if (menu) menu.SetActive(false);
        if (map) map.SetActive(false);
        if (achievements) achievements.SetActive(false);
        if (characteristics) characteristics.SetActive(false);
        if (settings) settings.SetActive(false);
        if (save) save.SetActive(false);
    }

    public bool IsAnyWindowOpen()
    {
        return currentWindow != WindowType.None;
    }

    private void UpdateInputMode()
    {
        if (InputModeController.instance == null)
            return;

        if (IsAnyWindowOpen())
            InputModeController.instance.SetUIMode();
        else
            InputModeController.instance.SetGameplayMode();
    }
}

public enum WindowType
{
    None,
    Inventory,
    Crafting,
    Map,
    Directory,
    Diary,
    Achievements,
    Characteristics,
    Menu,
    Settings,
    Save
}

public enum ContextItem
{
    None,
    Fire,
    Book,
    Sphere
}