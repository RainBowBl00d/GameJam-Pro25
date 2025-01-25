using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyNotesGameHelper : MonoBehaviour
{
    [SerializeField] KeyNotesGame keyNoteGame;

    [SerializeField]KeyNoteGameLevelStats levelStats1, levelStats2, levelStats3;

    public void Level1()
    {
        keyNoteGame.GenerateMiniGame(levelStats1);

    }
    public void Level2()
    {
        if (!levelStats1.Completed )
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats2);

    }
    public void Level3()
    {
        if (!levelStats2.Completed )
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats3);
    }
    public void Next()
    {
        if (!levelStats3.Completed )
        {
            Debug.Log("Level 3 unCompleted");
            return;
        }
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
    public int Missed { get; set; }
    public int Correct {get; set;}
}
