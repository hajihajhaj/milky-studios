using UnityEngine;
using UnityEngine.UI;

public class WinScreenStars : MonoBehaviour
{
    public Image[] starImages;          // Drag the star image UI elements in the Inspector
    public Sprite filledStar;           // Assign your filled star sprite
    public Sprite emptyStar;            // Assign your empty star sprite

    void Start()
    {
        int stars = PlayerPrefs.GetInt("StarsEarned", 0);  // Default to 0 if not set

        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < stars)
                starImages[i].sprite = filledStar;
            else
                starImages[i].sprite = emptyStar;
        }
    }
}