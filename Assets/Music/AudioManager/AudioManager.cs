using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Property's
    public AudioMixerGroup mixerGroups;
    public AudioMixer audioMixer;

    public Sound[] sounds;
    #region Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InstanceAwake();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }
    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s.source.isPlaying) return;
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }
    #endregion
    #region Instance Managment
    public static AudioManager instance;
    void InstanceAwake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
   
    #endregion
}
