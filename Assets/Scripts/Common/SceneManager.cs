using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        LoadScene(currentScene.name);
    }


    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ResumeApplication()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseApplication()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}
