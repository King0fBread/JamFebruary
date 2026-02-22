using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuSceneTransition : MonoBehaviour
{
    private string _menuSceneName = "MenuScene";
    public void LoadMenuFromEndingScreen()
    {
        SceneManager.LoadScene(_menuSceneName);
    }
}
