using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class KeyNotesGameHelper : MonoBehaviour
{
    [SerializeField] KeyNotesGame keyNoteGame;
    [SerializeField] GameObject button;
    [SerializeField] private ProgressSlider slider;

    [SerializeField]KeyNoteGameLevelStats levelStats1, levelStats2, levelStats3;

    public void Level1()
    {
        keyNoteGame.GenerateMiniGame(levelStats1);
        slider.StartGame(levelStats1);
    }
    public void Level2()
    {
        if (!levelStats1.Completed )
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats2);
        slider.StartGame(levelStats2);
    }
    public void Level3()
    {
        if (!levelStats2.Completed )
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats3);
        slider.StartGame(levelStats3);
        if (levelStats3.Completed) button.SetActive(true);
    }
    public void Next()
    {
        if (!levelStats3.Completed )
        {
            Debug.Log("Level 3 unCompleted");
            return;
        }
        SceneManager.LoadScene(7);
    }
}
[System.Serializable]
public class KeyNoteGameLevelStats
{
    public int sequenceLength;
    public bool sortHorizontally;
    public bool ascending;
    public float timeToWaitBtwRealses;
    public float hitFactorRequirement;
    

    public bool Completed { get; set; }
    public int Lost { get; set; }
    public int Missed { get; set; }
    public int Correct {get; set;}
}
