using UnityEngine;

public class DeliveryTracker : MonoBehaviour
{
    private int packagesDelivered = 0;

    [Header("Delivery Settings")]
    [SerializeField] private int totalPackages = 4; // Set in Inspector

    public static DeliveryTracker Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    /// <summary>
    /// Call this when a package is successfully delivered.
    /// </summary>
    public void MarkPackageDelivered()
    {
        packagesDelivered++;
    }

    /// <summary>
    /// How many packages have been delivered.
    /// </summary>
    public int GetDeliveredCount()
    {
        return packagesDelivered;
    }

    /// <summary>
    /// Total number of packages expected for this level.
    /// </summary>
    public int GetTotalDeliveries()
    {
        return totalPackages;
    }

    /// <summary>
    /// How many deliveries are still pending.
    /// </summary>
    public int GetUndeliveredCount()
    {
        return Mathf.Max(0, totalPackages - packagesDelivered);
    }
}
