using System;
using System.Collections.Generic;
using AdaptiveAudio;
using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private String volumeVariable;
    [SerializeField] private List<HorizontalAudioManager> horizontalAudioManagers;
    [SerializeField] private List<VerticalAudioManager> verticalAudioManagers;
    
    public void SetVolume(float volume)
    {
        foreach (HorizontalAudioManager horizontalAudioManager in horizontalAudioManagers)
        {
            if (horizontalAudioManager.name.Equals(name))
            {
                
            }
            horizontalAudioManager.setVolume(volume);
        }

        foreach (VerticalAudioManager verticalAudioManager in verticalAudioManagers)
        {
            verticalAudioManager.SetVolume(volume);
            
        }
    }
    
    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat(gameObject.name, 1));
    }

    public void OnChangeSlider(float value)
    {
        float volume = Mathf.Log10(value);
        Debug.Log(gameObject.name + " set to " + volume);
        SetVolume(volume);
        PlayerPrefs.SetFloat(gameObject.name, volume);
        PlayerPrefs.Save();
    }
}
