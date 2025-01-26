using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Timerandhits : MonoBehaviour
{
    public Slider timer;
    public Slider hits;
    public Slider requirement;
    private DodgeGameLevelStats _stats;

    void Update()
    {
        if (_stats != null)
        {
            timer.value -= Time.deltaTime;
            hits.value = hits.maxValue - _stats.hits;
        }
    }

    public void StartGame(DodgeGameLevelStats stats)
    {
        _stats = stats;
        requirement.value = stats.DodgeFactor;
        timer.maxValue = stats.time;
        timer.value = timer.maxValue;
        hits.maxValue = stats.time * stats.rOF;
        hits.value = hits.maxValue;
    }
}
