using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DodgeGameStageHelper : MonoBehaviour
{
    [SerializeField] DodgeGame keyNoteGame;
    [SerializeField] GameObject button, ready, ani, tutorial;
    [SerializeField] private Timerandhits sliders;
    [SerializeField] Animator animator;

    [SerializeField]DodgeGameLevelStats levelStats1, levelStats2, levelStats3;

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
        animator.Play("New Animation");
        yield return new WaitForSecondsRealtime(4f);
        ani.SetActive(false);
        Level1();
        while (levelStats1.Running) yield return new WaitForSecondsRealtime(1f);
        if (levelStats1.Completed)
        {
            HorizontalAudioManager.instance.CurrentGameState = 2;
            ani.SetActive(true);
            animator.Play("New Animation");
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
            animator.Play("New Animation");
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
        keyNoteGame.GenerateGame(levelStats1);
        sliders.StartGame(levelStats1);
    }
    void Level2()
    {
        if (!levelStats1.Completed )
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        keyNoteGame.GenerateGame(levelStats2);
        sliders.StartGame(levelStats2);
    }
    void Level3()
    {
        if (!levelStats2.Completed )
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        keyNoteGame.GenerateGame(levelStats3);
        sliders.StartGame(levelStats3);
        if (levelStats3.Completed) button.SetActive(true);
    }
    public void Next()
    {
        SceneManager.LoadScene("Stage 3");
    }
    void ResetValues(DodgeGameLevelStats stats)
    {
        stats.hits = 0;
        stats.Completed = false;
    }
}

[System.Serializable]
public class DodgeGameLevelStats
{
    public bool Completed { get; set; }
    public int hits { get; set; }
    public float rOF;
    public float time;
    public float bulletSpeed;
    public float sineWeight;
    public float sineFrequency;
    public float sineAmplitude;
    public float DodgeFactor;
    public bool Running { get; set; }
}
