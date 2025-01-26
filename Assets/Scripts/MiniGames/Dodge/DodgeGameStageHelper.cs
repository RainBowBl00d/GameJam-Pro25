using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DodgeGameStageHelper : MonoBehaviour
{
    [SerializeField] DodgeGameLevelStats levelStats1, levelStats2, levelStats3;
    [SerializeField] DodgeGame dodgeGame;
    [SerializeField] GameObject button;
    [SerializeField] private Timerandhits timerandhits;

    public void Level1()
    {
        dodgeGame.GenerateGame(levelStats1);
        timerandhits.StartGame(levelStats1);
    }
    public void Level2()
    {
        if (!levelStats1.Completed)
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        dodgeGame.GenerateGame(levelStats2);
        timerandhits.StartGame(levelStats2);

    }
    public void Level3()
    {
        if (!levelStats2.Completed)
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        dodgeGame.GenerateGame(levelStats3);
        timerandhits.StartGame(levelStats3);
        if (levelStats3.Completed) button.SetActive(true);
    }
    public void NextStage()
    {
        if (!levelStats3.Completed)
        {
            Debug.Log("Level 3 unCompleted");
            return;
        }
        SceneManager.LoadScene(8);
    }
}
[System.Serializable]
public class DodgeGameLevelStats
{
    public float rOF;
    public float bulletSpeed;
    public float time;
    public float sineWeight;
    public float sineFrequency;
    public float sineAmplitude;
    public float DodgeFactor;

    public bool Completed { get; set; }
    public int hits { get; set; }
}
