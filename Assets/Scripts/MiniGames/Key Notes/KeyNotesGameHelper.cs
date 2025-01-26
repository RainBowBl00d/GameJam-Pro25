using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyNotesGameHelper : MonoBehaviour
{
    public static event Action<int> OnLevelChanged; // Event to notify level changes

    [SerializeField] KeyNotesGame keyNoteGame;
    [SerializeField] GameObject button, ready, ani, tutorial;
    [SerializeField] private ProgressSlider slider;
    [SerializeField] Animator animator;

    [SerializeField] KeyNoteGameLevelStats levelStats1, levelStats2, levelStats3;

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
        NotifyLevelChanged(0);
        yield return new WaitForSecondsRealtime(4f);
        ani.SetActive(false);
        Level1();
        while (levelStats1.Running) yield return new WaitForSecondsRealtime(1f);
        if (levelStats1.Completed)
        {
            ani.SetActive(true);
            animator.Play("countDownAnim");
            NotifyLevelChanged(1);
            yield return new WaitForSecondsRealtime(4f);
            ani.SetActive(false);
            Level2();
            while (levelStats2.Running) yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            
            NotifyLevelChanged(-1);
            ready.SetActive(true);
            tutorial.SetActive(true);
        }
        if (levelStats2.Completed)
        {
            ani.SetActive(true);
            animator.Play("countDownAnim");
            NotifyLevelChanged(2);
            yield return new WaitForSecondsRealtime(4f);
            ani.SetActive(false);
            Level3();
            while (levelStats3.Running) yield return new WaitForSecondsRealtime(1f);
        }
        
        NotifyLevelChanged(-1);
        ready.SetActive(true);
        tutorial.SetActive(true);
        
        if (levelStats3.Completed)
        {
            button.SetActive(true);
        }
    }
    public void Next()
    {
        SceneManager.LoadScene("Stage 2");
    }
    private void NotifyLevelChanged(int level)
    {
        OnLevelChanged?.Invoke(level); // Trigger event
    }

    void Level1()
    {
        keyNoteGame.GenerateMiniGame(levelStats1);
        slider.StartGame(levelStats1);
    }
    void Level2()
    {
        if (!levelStats1.Completed)
        {
            Debug.Log("Level 1 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats2);
        slider.StartGame(levelStats2);
    }
    void Level3()
    {
        if (!levelStats2.Completed)
        {
            Debug.Log("Level 2 unCompleted");
            return;
        }
        keyNoteGame.GenerateMiniGame(levelStats3);
        slider.StartGame(levelStats3);
        if (levelStats3.Completed) button.SetActive(true);
    }

    void ResetValues(KeyNoteGameLevelStats stats)
    {
        stats.Correct = 0;
        stats.Missed = 0;
        stats.Lost = 0;
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
    public int Correct { get; set; }
    public bool Running { get; set; }
}
