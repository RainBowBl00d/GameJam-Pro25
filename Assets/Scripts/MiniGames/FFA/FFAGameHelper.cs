using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FFAGameHelper : MonoBehaviour
{
    [SerializeField] FFALevelStats levelStats1, levelStats2, levelStats3;
    [SerializeField] FFAGame fFAGame;
    [SerializeField] GameObject button, ready, ani, tutorial;
    [SerializeField] Animator animator;
    public void Ready()
    {
        StartCoroutine(RoundManage());
    }
    IEnumerator RoundManage()
    {
        ResetValues(levelStats3);
        ResetValues(levelStats2);
        ResetValues(levelStats1);

        ani.SetActive(true);
        animator.Play("countDownAnim");
        yield return new WaitForSecondsRealtime(4f);
        ani.SetActive(false);
        Level1();
        while (levelStats1.Running) yield return new WaitForSecondsRealtime(1f);
        if (levelStats1.Completed)
        {
            ani.SetActive(true);
            animator.Play("countDownAnim");
            yield return new WaitForSecondsRealtime(4f);
            ani.SetActive(false);
            Level2();
            while (levelStats2.Running) yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            ready.SetActive(true);
            tutorial.SetActive(true);
        }
        if (levelStats2.Completed)
        {
            ani.SetActive(true);
            animator.Play("countDownAnim");
            yield return new WaitForSecondsRealtime(4f);
            ani.SetActive(false);
            Level3();
            while (levelStats3.Running) yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            ready.SetActive(true);
            tutorial.SetActive(true);
        }
        if (levelStats3.Completed)
        {
            button.SetActive(true);
        }
    }
    void Level1()
    {
        fFAGame.GenerateGame(levelStats1);
    }
    void Level2()
    {
        if (!levelStats1.Completed)
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        fFAGame.GenerateGame(levelStats2);

    }
    void Level3()
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

        SceneManager.LoadScene(5);
    }
    void ResetValues(FFALevelStats stats)
    {
        stats.Hits = 0;
        stats.Completed = false;
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
    public bool Running { get; set; }

}
