using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestRaycast : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HIT: " + name);
    }
}