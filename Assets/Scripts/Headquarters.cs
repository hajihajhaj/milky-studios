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

            // ? Show level complete UI
            levelUI.ShowLevelComplete();

            // ? Show star UI
            WinScreenStars starUI = FindObjectOfType<WinScreenStars>();
            if (starUI != null)
                starUI.ShowStars();
            else
                Debug.LogWarning("?? EndStarsUI not found in scene!");

            Debug.Log("? Player reached HQ — level complete screen and stars shown.");
        }
    }
}
