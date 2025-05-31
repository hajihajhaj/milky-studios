using UnityEngine;

public class ButtonBop : MonoBehaviour
{
    public float amplitude = 10f;  // UI usually needs bigger values for visible movement
    public float speed = 2f;

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * amplitude;
        rectTransform.anchoredPosition = startPos + new Vector2(0, newY);
    }
}
