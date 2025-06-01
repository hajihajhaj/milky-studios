// ------------------------------
// GameManager.cs
// ------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float elapsedTime;
    public int packagesDelivered;
    public int totalPackages = 5;
    public string winSceneName = "win";
    public string loseSceneName = "loser";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 300f) // 5 minutes
        {
            SceneManager.LoadScene(loseSceneName);
        }
    }

    public void PackageDelivered()
    {
        packagesDelivered++;
    }

    public void FinishLevel()
    {
        int stars = DeliveryRating.Instance.CalculateStars(elapsedTime, packagesDelivered, totalPackages);
        PlayerPrefs.SetInt("StarsEarned", stars);
        SceneManager.LoadScene(winSceneName);
    }
}