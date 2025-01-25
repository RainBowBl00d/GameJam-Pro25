using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private HorizontalAudioManager hor;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private String volumeVariable;
    [SerializeField] private Slider slider;
    [SerializeField] private float scale;
    [SerializeField] private float sub;
    
    private void Start()
    {
        float volume = Mathf.Exp(Mathf.Exp((PlayerPrefs.GetFloat(volumeVariable, 0) + sub) / scale));
        if (!Mathf.Approximately(slider.value, volume)) slider.value = volume;
    }

    public void OnChangeSlider(float value)
    {
        float volume = Mathf.Log(Mathf.Log(value)) * scale - sub;
        Debug.Log(volumeVariable + " set to " + volume);
        mixer.SetFloat(volumeVariable, volume);
        PlayerPrefs.SetFloat(volumeVariable, volume);
        PlayerPrefs.Save();
        if (hor != null)
        {
            hor.StartPlaying();
        }
    }
}
