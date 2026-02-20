using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneLogic : MonoBehaviour
{
    [SerializeField] private GameObject _endScreenObject;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        _endScreenObject.SetActive(false);
    }
}
