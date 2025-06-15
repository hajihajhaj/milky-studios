using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPackages : MonoBehaviour
{
    public int maxPackages = 4;
    public int currentPackages;
    public int packagesDelivered = 0;

    [Header("Delivery Management")]
    public DeliveryManager deliveryManager; // new

    void Start()
    {
        currentPackages = maxPackages;

        // Find DeliveryManager if not set in Inspector
        if (deliveryManager == null)
        {
            deliveryManager = FindObjectOfType<DeliveryManager>();
        }
    }

    public void TakeDamage(int amount)
    {
        currentPackages = Mathf.Clamp(currentPackages - amount, 0, maxPackages);
        Debug.Log("Package Lost. Remaining: " + currentPackages);

        // Lose the current delivery too
        if (deliveryManager != null)
        {
            Debug.Log("Lost NPC package due to damage! Skipping to next delivery.");
            deliveryManager.SkipCurrentDelivery();
        }

        if (currentPackages <= 0)
        {
            Die();
        }
    }

    public void DeliverPackage()
    {
        if (currentPackages > 0)
        {
            packagesDelivered++;
            currentPackages--;
            Debug.Log("Package Delivered! Total: " + packagesDelivered);
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        Invoke("LoadGameOverScene", 1f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("loser");
    }
}