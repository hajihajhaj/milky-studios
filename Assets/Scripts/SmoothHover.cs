using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float zoomScale = 1.1f;
    public float zoomTime = 0.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, Vector3.one * zoomScale, zoomTime).setEaseOutExpo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, Vector3.one, zoomTime).setEaseOutExpo();
    }
}
