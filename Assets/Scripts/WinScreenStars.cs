using UnityEngine;
using UnityEngine.UI;

public class WinScreenStars : MonoBehaviour
{
    public Sprite emptyStar;
    public Sprite filledStar;
    public Image[] starImages;

    public GameObject starCanvas;
    public int maxScore = 40; // Full score gets 5 stars

    void Start()
    {
        maxScore = PlayerPackages.Instance.maxPackages * 10;
        // starCanvas.SetActive(false); // Hide initially
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
        int baseStars = Mathf.Clamp(Mathf.FloorToInt((score / (float)maxScore) * 4), 0, 4); // Max 4 stars from score

        int stars = baseStars;

        // ? Check for 5th star: must have full score + under 60s
        float timeTaken = Time.timeSinceLevelLoad;
        bool deliveredAll = score >= maxScore;

        if (deliveredAll && timeTaken < 60f)
        {
            stars++;
            Debug.Log($"?? Bonus star! Delivered all packages in {timeTaken:F1}s");
        }

        // Clamp stars to 5 just in case
        stars = Mathf.Clamp(stars, 0, 5);

        // Log result
        Debug.Log($"? Showing stars! Score: {score}, Time: {timeTaken:F1}s, Stars: {stars}");

        // Update visuals
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < stars ? filledStar : emptyStar;
        }
    }



}
