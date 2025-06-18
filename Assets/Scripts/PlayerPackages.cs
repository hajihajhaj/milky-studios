using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPackages : MonoBehaviour
{
    public int maxPackages = 4;
    public int currentPackages;
    public int packagesDelivered = 0; // NEW
    public static PlayerPackages Instance;


    void Start()
    {
        currentPackages = maxPackages;
        Instance = this;
    }

    public void TakeDamage(int amount)
    {
        currentPackages = Mathf.Clamp(currentPackages - amount, 0, maxPackages);
        Debug.Log("Package Lost. Remaining: " + currentPackages);

        // Show popup message when player loses a package
        if (PhoneUIController.Instance != null)
        {
            PhoneUIController.Instance.ShowPopup("Package dropped!");
        }

        // Skip the current delivery so the NPC disappears
        DeliveryManager deliveryManager = FindObjectOfType<DeliveryManager>();
        if (deliveryManager != null)
        {
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

            // ? Add score!
            DeliveryScoreManager.Instance.AddScore(10);

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