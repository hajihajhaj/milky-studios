using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneToLoad = "SampleScene"; 

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
