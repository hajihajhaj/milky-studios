using UnityEngine;

public class Headquarters : MonoBehaviour
{
    private bool hasTriggered = false;
    public NextLevelButton levelUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            levelUI.ShowLevelComplete();
            Debug.Log("? Player reached HQ — level complete screen shown.");
        }
    }
}
