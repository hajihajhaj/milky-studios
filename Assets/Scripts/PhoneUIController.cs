using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneUIController : MonoBehaviour
{
    public static PhoneUIController Instance;

    [Header("UI References")]
    public GameObject phoneUI;
    public TextMeshProUGUI timerText;
    public Image customerImage;
    public TextMeshProUGUI deliveryCountText;

    [Header("Delivery Info")]
    public float deliveryTime = 120f; // 2 minutes
    private float timeLeft;
    private bool isTiming = false;

    private int deliveriesCompleted = 0;
    public int totalDeliveries = 3;

    private Color originalTimerColor;  // Store original color of timer text
    private Color defaultTimerColor = Color.black;  // fallback default color

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        phoneUI.SetActive(true);

        // Save original color or fallback to black if null/transparent
        if (timerText != null && timerText.color.a > 0)
            originalTimerColor = timerText.color;
        else
            originalTimerColor = defaultTimerColor;

        timerText.color = originalTimerColor;

        UpdateDeliveryCountUI();
        timeLeft = deliveryTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isTiming)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0f) timeLeft = 0f;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        // Update timer color based on remaining time
        if (timeLeft <= 30f)
        {
            timerText.color = Color.red;
        }
        else if (timeLeft <= 60f)
        {
            timerText.color = Color.yellow;
        }
        else
        {
            timerText.color = originalTimerColor;
        }
    }

    void UpdateDeliveryCountUI()
    {
        deliveryCountText.text = $"Delivery {deliveriesCompleted}/{totalDeliveries}";
    }

    public void StartNewDelivery(Sprite newCustomerFace)
    {
        customerImage.sprite = newCustomerFace;
        timeLeft = deliveryTime; // reset timer
        isTiming = true;
        timerText.color = originalTimerColor;  // Reset color to original at start of delivery
        UpdateTimerUI();
        UpdateDeliveryCountUI();
    }

    public void CompleteDelivery()
    {
        isTiming = false;
        deliveriesCompleted++;
        timerText.color = originalTimerColor;  // Reset color when delivery completed
        UpdateDeliveryCountUI();
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }
}