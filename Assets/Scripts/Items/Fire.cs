using UnityEngine;

public class Fire : MonoBehaviour
{
    public static Fire instance { get; private set; }
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
