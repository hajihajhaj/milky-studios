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

        Debug.Log("? Enabling WIN panel");
        starCanvas.SetActive(true); // Shows the "win" panel

        int score = DeliveryScoreManager.Instance.GetScore();
        int stars = Mathf.Clamp(Mathf.FloorToInt((score / (float)maxScore) * 4), 0, 4); // 4-star max

        Debug.Log($"? Showing stars! Score: {score}, Stars: {stars}");

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < stars ? filledStar : emptyStar;
        }
    }

}
