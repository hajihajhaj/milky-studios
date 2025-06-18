using UnityEngine;

public class DeliveryScoreManager : MonoBehaviour
{
    public static DeliveryScoreManager Instance;

    public int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"?? Delivery score updated: {score}");
    }

    public int GetScore()
    {
        return score;
    }
}
