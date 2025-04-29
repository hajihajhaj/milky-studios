using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed = 2f;
    public float destroyOffset = 1f;
    public int damage = 1; // How much damage the bird does

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        if (transform.position.x < leftEdge.x - destroyOffset)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bird collided with: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Bird hit player!");

            PlayerPackages health = other.GetComponent<PlayerPackages>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject); // Bird disappears after hitting the player
        }
    }
}
