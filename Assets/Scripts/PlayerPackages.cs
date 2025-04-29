using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerPackages : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        Invoke("LoadGameOverScene", 1f); // Waits 1 second, then calls LoadGameOverScene
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("loser"); // <-- Replace "GameOver" with your scene name
    }

}
