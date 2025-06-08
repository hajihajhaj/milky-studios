using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Dialogue : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI timerTextComponent;
    public GameObject nextLineArrow;
    public GameObject exitButton;
    public GameObject dialogueBox;

    [Header("Audio")]
    [SerializeField] private AudioClip dialogueTypingSoundClip;
    [SerializeField] private bool stopAudioSource = false;

    [Header("Delivery Reaction Settings")]
    public float sassyThreshold = 20f;
    public float angryThreshold = 15f;

    [Header("Dialogue Lines")]
    [TextArea(2, 5)] public string[] niceLines;
    [TextArea(2, 5)] public string[] sassyLines;
    [TextArea(2, 5)] public string[] angryLines;

    private AudioSource audioSource;
    private string[] activeLines;
    private int index = 0;
    private bool isTyping = false;

    [Header("NPC Settings")]
    public int npcID;

    [System.Serializable]
    public class DialogueClosedEvent : UnityEvent<int> { }
    public DialogueClosedEvent onDialogueClosedWithID;

    [Header("Typewriter Settings")]
    public float textSpeed = 0.04f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (dialogueBox != null) dialogueBox.SetActive(false);
        if (nextLineArrow != null) nextLineArrow.SetActive(true);
        if (exitButton != null) exitButton.SetActive(false);
    }

    public void StartDialogue()
    {
        // ✅ Check if this NPC is the correct delivery target
        DeliveryManager deliveryManager = FindObjectOfType<DeliveryManager>();
        if (deliveryManager != null && deliveryManager.GetCurrentDeliveryNPCID() != npcID)
        {
            MessageUI.Instance.ShowMessage("Wrong house!", 2f);
            return; // Block dialogue from starting
        }

        PhoneUIController.Instance?.HidePhoneUI();

        if (dialogueBox != null) dialogueBox.SetActive(true);
        if (exitButton != null) exitButton.SetActive(false);

        float timeLeft = PhoneUIController.Instance != null ? PhoneUIController.Instance.GetTimeLeft() : 120f;

        UpdateTimerVisual(timeLeft);
        ChooseLinesBasedOnTimeLeft(timeLeft);

        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private void UpdateTimerVisual(float timeLeft)
    {
        if (timerTextComponent == null) return;

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerTextComponent.text = $"{minutes:00}:{seconds:00}";

        if (timeLeft <= angryThreshold)
            timerTextComponent.color = new Color(1f, 0.5f, 0f);
        else if (timeLeft <= sassyThreshold)
            timerTextComponent.color = Color.yellow;
        else
            timerTextComponent.color = new Color(0.9f, 0.9f, 0.9f);
    }

    private void ChooseLinesBasedOnTimeLeft(float timeLeft)
    {
        if (timeLeft > sassyThreshold)
        {
            activeLines = niceLines.Length > 0 ? niceLines : new string[] { "Thanks for being on time!" };
        }
        else if (timeLeft > angryThreshold)
        {
            activeLines = sassyLines.Length > 0 ? sassyLines : new string[] { "Cutting it close, huh?" };
        }
        else
        {
            activeLines = angryLines.Length > 0 ? angryLines : new string[] { "Are you kidding me?!" };
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        if (nextLineArrow != null) nextLineArrow.SetActive(true);

        foreach (char c in activeLines[index])
        {
            textComponent.text += c;
            PlayDialogueSound();
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;

        bool hasMore = index < activeLines.Length - 1;
        if (nextLineArrow != null) nextLineArrow.SetActive(hasMore);
        if (exitButton != null) exitButton.SetActive(!hasMore);
    }

    public void OnNextLineClicked()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            textComponent.text = activeLines[index];
            isTyping = false;

            bool hasMore = index < activeLines.Length - 1;
            if (nextLineArrow != null) nextLineArrow.SetActive(hasMore);
            if (exitButton != null) exitButton.SetActive(!hasMore);
        }
        else
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        if (index < activeLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        textComponent.text = "";
        if (dialogueBox != null) dialogueBox.SetActive(false);
        if (exitButton != null) exitButton.SetActive(false);
        if (nextLineArrow != null) nextLineArrow.SetActive(false);

        onDialogueClosedWithID?.Invoke(npcID);
        PhoneUIController.Instance?.ShowPhoneUI();

        // ✅ Deliver after dialogue closes
        PlayerPackages player = FindObjectOfType<PlayerPackages>();
        if (player != null)
        {
            player.DeliverPackage();
        }
    }

    private void PlayDialogueSound()
    {
        if (audioSource != null && dialogueTypingSoundClip != null)
        {
            if (!audioSource.isPlaying || stopAudioSource)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.clip = dialogueTypingSoundClip;
                audioSource.Play();
            }
        }
    }
}
