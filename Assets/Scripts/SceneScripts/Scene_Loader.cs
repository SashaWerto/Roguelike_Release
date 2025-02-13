using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Loader : MonoBehaviour
{
    private static Scene_Loader sceneLoader;
    public static Scene_Loader Instance => sceneLoader;

    private void Awake()
    {
        sceneLoader = this;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
