using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadVolumes : MonoBehaviour
{
    [SerializeField] private List<String> names;
    [SerializeField] private AudioMixer mixers;
    void Start()
    {
        for (int i = 0; i < names.Count; i++)
        {
            float volume = PlayerPrefs.GetFloat(names[i], 0);
            mixers.SetFloat(names[i], volume);
            Debug.Log($"{names[i]} set to {volume}");
        }
    }
}
