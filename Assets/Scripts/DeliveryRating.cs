using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DeliveryRating : MonoBehaviour
{
    public static DeliveryRating Instance;

    public DeliveryTracker deliveryTracker;
    public float timeBeforeStarLoss = 30f;
    public float starLossInterval = 30f;
    public int maxStars = 5;

    public float timeLimit = 30f;
    public float timePenaltyInterval = 15f;

    private float levelStartTime;
    private int starsEarned;

    // ??? This is the missing piece
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // prevent duplicate
    }

    void Start()
    {
        levelStartTime = Time.time;
        starsEarned = maxStars;
    }

    public int CalculateFinalStars()
    {
        float elapsedTime = Time.time - levelStartTime;

        int timeStarsLost = Mathf.FloorToInt(Mathf.Max(0f, (elapsedTime - timeBeforeStarLoss)) / starLossInterval);
        int deliveryStarsLost = deliveryTracker != null ? deliveryTracker.GetUndeliveredCount() : 0;

        starsEarned = Mathf.Clamp(maxStars - timeStarsLost - deliveryStarsLost, 0, maxStars);
        PlayerPrefs.SetInt("StarsEarned", starsEarned);

        return starsEarned;
    }

    public int CalculateStars(float elapsedTime, int packagesDelivered, int totalPackages)
    {
        int stars = 5;

        if (elapsedTime > timeLimit)
            stars -= Mathf.FloorToInt((elapsedTime - timeLimit) / timePenaltyInterval);

        int missed = totalPackages - packagesDelivered;
        stars -= missed;

        return Mathf.Clamp(stars, 0, 5);
    }
}