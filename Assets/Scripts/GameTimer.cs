using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool countDown = false;

    [Tooltip("Time limit in seconds before triggering loser screen (e.g., 150 = 2 minutes 30 seconds)")]
    public float timeLimit = 150f;

    public GameObject loserScreen; // Drag your UI panel here

    private float time;
    private bool gameEnded = false;

    void Start()
    {
        time = countDown ? timeLimit : 0f;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (gameEnded) return;

        time += countDown ? -Time.deltaTime : Time.deltaTime;

        if (!countDown && time >= timeLimit)
        {
            TimerEnded();
            gameEnded = true;
        }
        else if (countDown && time <= 0f)
        {
            time = 0f;
            TimerEnded();
            gameEnded = true;
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        Debug.Log("? Timer reached limit. Showing loser screen.");
        if (loserScreen != null)
        {
            loserScreen.SetActive(true);
        }
    }
}
