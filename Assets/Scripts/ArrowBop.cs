using UnityEngine;

public class ArrowBop : MonoBehaviour
{
    public float amplitude = 0.5f; // How far it moves up and down
    public float speed = 2f;       // How fast it moves

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
