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
    public float deliveryTime = 30f; // Default time per delivery
    private float timeLeft;
    private bool isTiming = false;

    private int deliveriesCompleted = 0;
    public int totalDeliveries = 4;

    private Color originalTimerColor;
    private Color defaultTimerColor = Color.black;

    [Header("Popup UI")]
    public GameObject popupPanel; // The whole panel GameObject
    public TextMeshProUGUI popupText; // The TMP text inside the panel
    public float popupDuration = 2f;

    [Header("Custom Thresholds")]
    public float yellowThreshold = 15f;
    public float redThreshold = 10f;

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

        if (popupPanel != null)
            popupPanel.SetActive(false);
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

        if (timeLeft <= redThreshold)
        {
            timerText.color = Color.red;
        }
        else if (timeLeft <= yellowThreshold)
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
        deliveryCountText.text = $"Deliveries {deliveriesCompleted}/{totalDeliveries}";
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

    // New: Show a popup using panel + TMP
    public void ShowPopup(string message)
    {
        if (popupPanel != null && popupText != null)
        {
            popupText.text = message;
            popupPanel.SetActive(true);
            CancelInvoke(nameof(HidePopup));
            Invoke(nameof(HidePopup), popupDuration);
        }
    }

    void HidePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}