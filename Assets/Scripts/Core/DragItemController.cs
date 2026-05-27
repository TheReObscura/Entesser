using UnityEngine;
using UnityEngine.UI;

public class DragIconController : MonoBehaviour
{
    public static DragIconController Instance;

    public Image icon;

    RectTransform rect;

    void Awake()
    {
        Instance = this;

        rect =
            GetComponent<RectTransform>();

        icon.enabled = false;
    }

    public void Show(Sprite sprite)
    {
        icon.sprite = sprite;

        icon.enabled = true;
    }

    public void Hide()
    {
        icon.enabled = false;
    }

    public void SetPosition(Vector2 pos)
    {
        rect.position = pos;

       // Debug.Log(rect.position);
       
    }
}