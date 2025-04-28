using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float speed = 2f;  // Speed at which birds move left
    public float destroyX = -15f;  // X position to destroy bird after leaving screen

    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("Player").GetComponent<Collider2D>());
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy bird if off-screen
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
