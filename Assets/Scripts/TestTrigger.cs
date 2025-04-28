using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something hit me: " + other.name);
    }
}
