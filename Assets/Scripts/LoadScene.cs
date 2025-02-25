using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadSceneAsync(scene.name);
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void LoadScene(String scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
