using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool countDown = false;
    public float timeLimit = 300f;

    private float time;

    void Start()
    {
        time = countDown ? timeLimit : 0f;
        UpdateTimerDisplay();
    }

    void Update()
    {
        time += countDown ? -Time.deltaTime : Time.deltaTime;

        if (countDown && time <= 0f)
        {
            time = 0f;
            TimerEnded();
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
        Debug.Log("Time's up!");
    }
}
