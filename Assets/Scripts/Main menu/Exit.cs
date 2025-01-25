using UnityEngine;

public class Exit : MonoBehaviour
{
    // Make sure the method is public and outside of Start
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}