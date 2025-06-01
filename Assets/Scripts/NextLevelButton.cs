using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public GameObject winScreen; // drag the UI screen here!
    public string[] playableScenes; // assign only your actual level scene names here
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void ShowLevelComplete()
    {
        Debug.Log("?? Showing win screen...");
        winScreen.SetActive(true);
    }

    public void LoadNextLevel()
    {
        currentScene = SceneManager.GetActiveScene().name; // ?? ensure it's updated at runtime

        int index = System.Array.IndexOf(playableScenes, currentScene);

        if (index >= 0 && index < playableScenes.Length - 1)
        {
            string nextScene = playableScenes[index + 1];
            Debug.Log("? Loading next level: " + nextScene);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("? No more playable levels.");
            // Optionally: SceneManager.LoadScene("start");
        }
    }
}
