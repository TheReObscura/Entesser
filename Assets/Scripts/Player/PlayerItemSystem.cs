using Assets.Scripts.Items;
using Assets.Scripts.Player;

public class PlayerItemSystem
{
    private ActiveItem currentItem;
    public WeaponData equippedWeapon;
    public float selectCooldown = 0.3f;
    private float timer;
    

    public void Tick(float dt)
    {
        timer -= dt;

        if (timer > 0) return;

        HandleInput();
        HandleSelection();
    }
    public void EquipWeapon(ItemData item)
    {
        equippedWeapon = item as WeaponData;

        Player.Instance.handView.SetItem(item);
    }
    void HandleInput()
    {
        if (GameInput.instance.IsFireActive())
            Select(ActiveItem.Fire);

        else if (GameInput.instance.IsBookActive())
            Select(ActiveItem.Book);

        else if (GameInput.instance.IsSphereActive())
            Select(ActiveItem.Sphere);

        if (GameInput.instance.IsInteract())
        { 
            TryCast();
        }
    }

    void TryCast()
    {
        if (currentItem != ActiveItem.Sphere)
            return;

        var stats = Player.Instance.stats;

        float cost = PlayerCombat.instance.manaCost;

        if (stats.currentMana < cost)
            return;

        stats.UseMana(cost);

        PlayerCombat.instance.CastFireball();
    }

    void Select(ActiveItem item)
    {
        currentItem = (currentItem == item) ? ActiveItem.None : item;
        timer = selectCooldown;
    }

    void HandleSelection()
    {
        Fire.instance.SetActive(currentItem == ActiveItem.Fire);
        Book.instance.SetActive(currentItem == ActiveItem.Book);
        Sphere.instance.SetActive(currentItem == ActiveItem.Sphere);
        var slot = InventoryManager.Instance.GetSelectedSlot();

        if (slot == null || slot.item == null)
        {
            equippedWeapon = null;
            Player.Instance.handView.SetItem(null);
            return;
        }

        equippedWeapon = slot.item as WeaponData;

        Player.Instance.handView.SetItem(slot.item);
    }
}
public enum ActiveItem { None, Fire, Sphere, Book, Weapon }