using UnityEngine;
using TMPro;
using System.Collections;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance;

    public TextMeshProUGUI messageText;
    public GameObject messagePanel;

    private Coroutine currentCoroutine;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        messagePanel.SetActive(false);
    }

    public void ShowMessage(string message, float duration)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        messageText.text = message;
        messagePanel.SetActive(true);
        currentCoroutine = StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messagePanel.SetActive(false);
    }
}