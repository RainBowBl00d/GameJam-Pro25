using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene1 : MonoBehaviour
{
    public String name;

    private void Start()
    {
        SceneManager.LoadSceneAsync(name);
    }
}
