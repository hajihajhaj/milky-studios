using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed = 2f;
    public float destroyOffset = 1f;

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bird hit player!");  // You should see this in the Console

            PlayerPackages health = other.GetComponent<PlayerPackages>();
            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
