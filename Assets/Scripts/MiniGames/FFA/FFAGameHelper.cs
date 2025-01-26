using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FFAGameHelper : MonoBehaviour
{
    [SerializeField] FFALevelStats levelStats1, levelStats2, levelStats3;
    [SerializeField] FFAGame fFAGame;
    [SerializeField] GameObject button;

    public void Level1()
    {
        fFAGame.GenerateGame(levelStats1);
    }
    public void Level2()
    {
        if (!levelStats1.Completed)
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        fFAGame.GenerateGame(levelStats2);

    }
    public void Level3()
    {
        if (!levelStats2.Completed)
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        fFAGame.GenerateGame(levelStats3);
        if (levelStats3.Completed) button.SetActive(true);
    }
    public void NextStage()
    {
        if (!levelStats3.Completed)
        {
            Debug.Log("Level 3 unCompleted");
            return;
        }
        SceneManager.LoadScene(9);
    }
}
[System.Serializable]
public class FFALevelStats
{
    public float SpawnRate;
    public int MaxLayers;
    public float EnemySpeed;
    public float SpawnTime;
    public int lives;

    public bool Completed { get; set; }
    public int Hits { get; set; }

}
