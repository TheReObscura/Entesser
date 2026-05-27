public class PlayerItemSystem
{
    private ActiveItem currentItem;

    public float selectCooldown = 0.3f;
    private float timer;

    public void Tick(float dt)
    {
        timer -= dt;

        if (timer > 0) return;

        HandleInput();
        HandleSelection();
    }

    void HandleInput()
    {
        if (GameInput.instance.IsFireActive())
            Select(ActiveItem.Fire);

        else if (GameInput.instance.IsBookActive())
            Select(ActiveItem.Book);

        else if (GameInput.instance.IsSphereActive())
            Select(ActiveItem.Sphere);
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
    }
}
public enum ActiveItem { None, Fire, Sphere, Book, }