using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private String volumeVariable;
    [SerializeField] private Slider slider;
    [SerializeField] private float scale;
    [SerializeField] private float sub;
    
    private void Awake()
    {
        float volume = PlayerPrefs.GetFloat(volumeVariable, 0);
        slider.value = Mathf.Exp(Mathf.Exp((volume + sub) / scale));
    }

    public void OnChangeSlider(float value)
    {
        float volume = Mathf.Log(Mathf.Log(value)) * scale - sub;
        Debug.Log(volumeVariable + " set to " + volume);
        mixer.SetFloat(volumeVariable, volume);
        PlayerPrefs.SetFloat(volumeVariable, volume);
        PlayerPrefs.Save();
    }
}
