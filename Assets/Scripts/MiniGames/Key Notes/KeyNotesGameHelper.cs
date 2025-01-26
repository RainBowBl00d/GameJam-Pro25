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
    [SerializeField] GameObject button, ready_Button, tutorial, ready_ani_obj;
    [SerializeField] private ProgressSlider slider;
    [SerializeField] Animator readyAnimation;

    [SerializeField]KeyNoteGameLevelStats levelStats1, levelStats2, levelStats3;

    public void Ready_click()
    {
        StartCoroutine(Ready());
    }
    IEnumerator Ready()
    {
        ResetVariables(levelStats1);
        ResetVariables(levelStats2);
        ResetVariables(levelStats3);

        ready_ani_obj.SetActive(true);
        readyAnimation.Play("New Animation");
        yield return new WaitForSecondsRealtime(4f);
        ready_ani_obj.SetActive(false);
        Level1();
        while (levelStats1.running) yield return new WaitForSecondsRealtime(1f);
        if (levelStats1.Completed)
        {
            ready_ani_obj.SetActive(true);
            readyAnimation.Play("New Animation");
            yield return new WaitForSecondsRealtime(4f);
            ready_ani_obj.SetActive(false);
            Level2();
            while (levelStats2.running) yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            ready_ani_obj.SetActive(false);
            ready_Button.SetActive(true);
            tutorial.SetActive(true);
        }
        if (levelStats2.Completed)
        {
            ready_ani_obj.SetActive(true);
            readyAnimation.Play("New Animation");
            yield return new WaitForSecondsRealtime(4f);
            ready_ani_obj.SetActive(false);
            Level3();
            while (levelStats3.running) yield return new WaitForSecondsRealtime(1f);
            if (levelStats3.Completed) button.SetActive(true);

        }
        else
        {
            ready_ani_obj.SetActive(false);
            ready_Button.SetActive(true);
            tutorial.SetActive(true);
        }

    }
    void Level1()
    {
        keyNoteGame.GenerateMiniGame(levelStats1);
        slider.StartGame(levelStats1);
    }
    void Level2()
    {
        keyNoteGame.GenerateMiniGame(levelStats2);
        slider.StartGame(levelStats2);
    }
    void Level3()
    {
        if (!levelStats2.Completed )
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats3);
        slider.StartGame(levelStats3);
    }
    public void Next()
    {
        SceneManager.LoadScene(4);
    }
    void ResetVariables(KeyNoteGameLevelStats stats)
    {
        stats.Lost = 0;
        stats.Missed = 0;
        stats.Correct = 0;
        stats.Completed = false;
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
    public bool running { get; set; }
}
