using UnityEngine;

public class DeliveryRating : MonoBehaviour
{
    public static DeliveryRating Instance;

    public DeliveryTracker deliveryTracker;
    public int maxStars = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // prevent duplicate
    }

    public int CalculateFinalStars()
    {
        Debug.Log("?? CalculateFinalStars() called!");
        if (deliveryTracker == null)
        {
            Debug.LogWarning("DeliveryTracker is not assigned.");
            return 0;
        }

        int totalPackages = deliveryTracker.GetTotalDeliveries(); // e.g. 4
        int delivered = deliveryTracker.GetDeliveredCount();      // e.g. 4
        int missed = totalPackages - delivered;                   // 0

        int starsEarned = Mathf.Clamp(maxStars - missed, 0, maxStars); // should be 5
        PlayerPrefs.SetInt("StarsEarned", starsEarned);


        Debug.Log($"?? Total: {totalPackages}, Delivered: {delivered}, Stars: {starsEarned}");
        return starsEarned;


    }

}
