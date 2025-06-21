using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public string levelName;
    public int levelIndex;

    public Button playButton;                    // <-- Assign the child Button
    public TextMeshProUGUI labelText;
    public Image[] starImages;
    public Sprite filledStar;
    public Sprite emptyStar;
    public GameObject lockOverlay;

    void Start()
    {
        // Label
        if (labelText != null)
            labelText.text = "Day " + levelIndex;

        // Get saved progress
        int stars = PlayerPrefs.GetInt("Stars_Level" + levelIndex, 0);
        int unlocked = PlayerPrefs.GetInt("UnlockedLevelIndex", 1);

        // Update stars
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < stars ? filledStar : emptyStar;
        }

        // Lock logic
        bool isUnlocked = levelIndex <= unlocked;
        lockOverlay.SetActive(!isUnlocked);
        playButton.interactable = isUnlocked;

        // Add click listener
        playButton.onClick.AddListener(() => LoadLevel());
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
