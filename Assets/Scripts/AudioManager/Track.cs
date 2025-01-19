using System.Collections;
using UnityEngine;

namespace AdaptiveAudio{

    [System.Serializable]
    public class Track 
    {
        public string name;
        public AudioClip sound;
        private AudioSource audioSource;
        public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

        private bool isFadingIn = false;
        private bool isFadingOut = false;
        public bool IsFadingIn { get => isFadingIn; set => isFadingIn = value; }
        public bool IsFadingOut { get => isFadingOut; set => isFadingOut = value; }

        public void Play(float time = 0, bool loop = true, float volume = 1)
        {
            this.audioSource.loop = loop;
            this.audioSource.time = time;
            this.audioSource.volume = volume;
            this.audioSource.Play();
        }

        public void Stop()
        {
            this.audioSource.Stop();
        }

        public void Pause()
        {
            this.audioSource.Pause();
        }

        public void Resume(bool loop = true)
        {
            if (!this.AudioSource.isPlaying)
            {
                this.audioSource.loop = loop;
                this.audioSource.Play();
            }
        }

        public void SetVolume(float volume)
        {
            this.audioSource.volume = volume;
        }

        public float GetTrackTime()
        {
            if (audioSource.isPlaying)
            {
                return audioSource.time;
            }
            return 0;
        }

        public void StopFadingIn()
        {
            isFadingIn = false;
        }

        public void StopFadingOut()
        {
            isFadingOut = false; 
        }
    }
}