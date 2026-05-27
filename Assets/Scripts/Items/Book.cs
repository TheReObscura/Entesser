using UnityEngine;

public class Book : MonoBehaviour
{
    public static Book instance { get; private set; }
    private SpriteRenderer sprite;
    private bool isActive;

    void Awake()
    {
        instance = this;
        sprite = GetComponent<SpriteRenderer>();
        sprite.forceRenderingOff = true;
    }
    public void SetActive(bool value)
    {
        isActive = value;
        sprite.forceRenderingOff = !value;
    }
    public bool GetActive() => isActive;
}
