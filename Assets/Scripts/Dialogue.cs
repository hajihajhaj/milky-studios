using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Dialogue : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject nextLineArrow;
    public GameObject exitButton;
    public GameObject dialogueBox;

    [Header("Audio")]
    [SerializeField] private AudioClip dialogueTypingSoundClip;

    [SerializeField] private bool stopAudioSource;

    private AudioSource audioSource;

    private int index;
    private bool isTyping = false;

    void Awake()
    {
        // Get the AudioSource component (required)
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
        if (dialogueBox != null) dialogueBox.SetActive(true);

        index = 0;
        textComponent.text = string.Empty;

        if (exitButton != null) exitButton.SetActive(false);

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        if (nextLineArrow != null) nextLineArrow.SetActive(true);

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            PlayDialogueSound(); // play sound on each letter typed

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;

        if (index < lines.Length - 1)
        {
            if (nextLineArrow != null) nextLineArrow.SetActive(true);
        }
        else
        {
            if (nextLineArrow != null) nextLineArrow.SetActive(false);
            if (exitButton != null) exitButton.SetActive(true);
        }
    }

    public void OnNextLineClicked()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
            isTyping = false;

            if (index < lines.Length - 1)
            {
                if (nextLineArrow != null) nextLineArrow.SetActive(true);
            }
            else
            {
                if (nextLineArrow != null) nextLineArrow.SetActive(false);
                if (exitButton != null) exitButton.SetActive(true);
            }
        }
        else
        {
            NextLine();
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
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
    }

    private void PlayDialogueSound()
    {
        if (audioSource != null && dialogueTypingSoundClip != null)
        {
            if (!audioSource.isPlaying || stopAudioSource)
            {
                // Optional: random pitch
                audioSource.pitch = Random.Range(0.9f, 1.1f);

                audioSource.clip = dialogueTypingSoundClip;
                audioSource.Play();
            }
        }
    }
}