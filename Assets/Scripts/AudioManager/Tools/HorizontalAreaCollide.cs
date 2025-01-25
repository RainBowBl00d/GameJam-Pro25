using AdaptiveAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAreaCollide : MonoBehaviour
{
    VerticalAudioManager AudioManager;
    public Area area;
    public HorizontalAudioManager audioSegmentator;
    void Start()
    {
        AudioManager = VerticalAudioManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (area)
        {
            case Area.red:
                audioSegmentator.CurrentGameState = 0;
                audioSegmentator.StartPlaying();
                break;
            case Area.green:
                audioSegmentator.CurrentGameState = 2;
                break;
            case Area.blue:
                audioSegmentator.CurrentGameState = 4 ;
                break;
        }
    }
}
public enum Area
{
    red, green, blue, yellow
}