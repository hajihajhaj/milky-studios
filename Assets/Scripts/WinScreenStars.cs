using UnityEngine;
using UnityEngine.UI;

public class WinScreenStars : MonoBehaviour
{
    public Sprite emptyStar;
    public Sprite filledStar;
    public Image[] starImages;

    public GameObject starCanvas;
    public int maxScore = 40; // Full score from deliveries (4 × 10)

    [Header("Set This Per Scene")]
    public int currentLevelIndex = 1; // Example: 1 = sunny level, 2 = rainy level...

    void Start()
    {
        maxScore = PlayerPackages.Instance.maxPackages * 10;
        // starCanvas.SetActive(false); // Optional: hide at start
    }

    public void ShowStars()
    {
        if (starCanvas == null)
        {
            Debug.LogError("? starCanvas (Win panel) is not assigned!");
            return;
        }

        starCanvas.SetActive(true);

        int score = DeliveryScoreManager.Instance.GetScore();
        int baseStars = Mathf.Clamp(Mathf.FloorToInt((score / (float)maxScore) * 4), 0, 4); // 0–4 stars from delivery score

        int stars = baseStars;

        // ?? Time bonus: +1 star if player delivered all packages under 60s
        float timeTaken = Time.timeSinceLevelLoad;
        bool deliveredAll = score >= maxScore;

        if (deliveredAll && timeTaken < 60f)
        {
            stars++;
            Debug.Log($"?? Bonus star! Delivered all packages in {timeTaken:F1}s");
        }

        // Clamp stars to max available
        stars = Mathf.Clamp(stars, 0, 5);

        // Log final result
        Debug.Log($"? Showing stars! Score: {score}, Time: {timeTaken:F1}s, Stars: {stars}");

        // Update visuals
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < stars ? filledStar : emptyStar;
        }

        // --- Save Progress ---
        int previousBest = PlayerPrefs.GetInt("Stars_Level" + currentLevelIndex, 0);
        if (stars > previousBest)
        {
            PlayerPrefs.SetInt("Stars_Level" + currentLevelIndex, stars);
        }

        int unlocked = PlayerPrefs.GetInt("UnlockedLevelIndex", 1);
        if (currentLevelIndex + 1 > unlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevelIndex", currentLevelIndex + 1);
        }

        PlayerPrefs.Save();
    }
}
