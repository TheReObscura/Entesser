using Assets.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float range = 1.5f;
    private List<IInteractable> nearby = new List<IInteractable>();

    public IInteractable GetBestInteractable()
    {
        if (nearby.Count == 0)
            return null;
        return nearby[0];
    }
    public void TryInteract()
    {
        if (!GameInput.instance.IsInteract())
            return;

        Collider2D hit =
            Physics2D.OverlapCircle(
                transform.position,
                1.5f
            );

        if (hit != null)
        {
            Pickup pickup =
                hit.GetComponentInParent<Pickup>();

            if (pickup != null && pickup.Interact())
                return;

            Chest chest =
                hit.GetComponentInParent<Chest>();

            if (chest != null && chest.Interact())
                return;
        }

        InventoryManager.Instance.UseSlot(
            InventoryManager.Instance.selectedHotbarSlot
        );
    }

    public void Register(IInteractable obj)
    {
        if (!nearby.Contains(obj))
            nearby.Add(obj);

        Debug.Log("ENTER: " + obj);
    }

    public void Unregister(IInteractable obj)
    {
        nearby.Remove(obj);

        Debug.Log("EXIT: " + obj);
    }
}