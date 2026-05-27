using Assets.Scripts.Items;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    public ItemData item;

    public bool Interact()
    {
        bool added =
            InventoryManager.Instance.AddItem(item);

        if (!added)
            return false;

        Destroy(gameObject);
        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerPickup>();

        if (player != null)
            player.Register(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerPickup>();

        if (player != null)
            player.Unregister(this);
    }
}