using UnityEngine;

public class DeliveryTracker : MonoBehaviour
{
    public int totalPackages = 4; // Can be set in Inspector
    public int packagesDelivered = 0;

    public void PackageDelivered()
    {
        packagesDelivered++;
    }

    public int GetUndeliveredCount()
    {
        return Mathf.Max(0, totalPackages - packagesDelivered);
    }
}