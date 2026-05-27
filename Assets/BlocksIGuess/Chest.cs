using UnityEngine;

public class Chest :
    MonoBehaviour,
    IInteractable
{
    public bool opened;

    public bool Interact()
    {
        if (opened)
            return true;

        Open();

        opened = true;

        return true;
    }

    void Open()
    {
        Debug.Log(
            "OPEN CHEST"
        );
    }
}