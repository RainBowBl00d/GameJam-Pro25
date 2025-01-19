using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AdaptiveAudio{

    [System.Serializable]
    public class Layer
    {
        public string name;
        public List<Track> tracksList;

        public void Play(float time = 0f, bool loop = true, float volume = 1)
        {
            foreach (Track track in this.tracksList)
            {
                track.Play(time, loop, volume);
            }
        }

        public void Stop()
        {
            foreach (Track track in this.tracksList)
            {
                track.Stop();
            }
        }

        public void Pause()
        {
            foreach (Track track in this.tracksList)
            {
                track.Pause();
            }
        }

        public void Resume(bool loop = true)
        {
            foreach (Track track in this.tracksList)
            {
                track.Resume(loop);
            }
        }

        public void SetVolume(float volume)
        {
            foreach(Track t in tracksList)
            {
                t.SetVolume(volume);
            }
        }

        public float GetTrackTime()
        {
            foreach(Track t in tracksList)
            {
                if (t.AudioSource.isPlaying)
                    return t.AudioSource.time;
            }
            return 0;
        }
    }
}