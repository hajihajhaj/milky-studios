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
    public GameObject nextCustomerButton;

    [Header("Delivery Info")]
    public float deliveryTime = 30f; // 30 seconds per delivery
    private float timeLeft;
    private bool isTiming = false;

    private int deliveriesCompleted = 0;
    public int totalDeliveries = 4;

    private Color originalTimerColor;
    private Color defaultTimerColor = Color.black;

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

        if (timerText != null && timerText.color.a > 0)
            originalTimerColor = timerText.color;
        else
            originalTimerColor = defaultTimerColor;

        timerText.color = originalTimerColor;

        UpdateDeliveryCountUI();
        timeLeft = deliveryTime;
        UpdateTimerUI();

        if (nextCustomerButton != null)
            nextCustomerButton.SetActive(false);
    }

    void Update()
    {
        if (isTiming)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                isTiming = false;

                if (nextCustomerButton != null)
                    nextCustomerButton.SetActive(true);
            }

            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        if (timeLeft <= 15f)
        {
            timerText.color = Color.red;
        }
        else if (timeLeft <= 20f)
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
        timeLeft = deliveryTime;
        isTiming = true;
        timerText.color = originalTimerColor;
        UpdateTimerUI();
        UpdateDeliveryCountUI();

        if (nextCustomerButton != null)
            nextCustomerButton.SetActive(false);
    }

    public void CompleteDelivery()
    {
        isTiming = false;
        deliveriesCompleted++;
        timerText.color = originalTimerColor;
        UpdateDeliveryCountUI();
    }

    public void OnNextCustomerButtonPressed()
    {
        if (nextCustomerButton != null)
            nextCustomerButton.SetActive(false);

        // Tell DeliveryManager to skip to the next delivery (without marking current one complete)
        DeliveryManager deliveryManager = FindObjectOfType<DeliveryManager>();
        if (deliveryManager != null)
        {
            deliveryManager.SkipCurrentDelivery();
        }
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    // New methods to show/hide phone UI
    public void HidePhoneUI()
    {
        if (phoneUI != null)
            phoneUI.SetActive(false);
    }

    public void ShowPhoneUI()
    {
        if (phoneUI != null)
            phoneUI.SetActive(true);
    }
}