using UnityEngine;

public class Sphere : MonoBehaviour
{
    public static Sphere instance { get; private set; }
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
