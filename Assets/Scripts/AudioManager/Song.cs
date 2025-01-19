using System.Collections.Generic;

namespace AdaptiveAudio{

    [System.Serializable]
    public class Song
    {
        public string name;
        public List<Layer> layerList;

        public void Play(float time = 0f, bool loop = true, float volume = 1)
        {
            foreach (Layer layer in this.layerList)
            {
                layer.Play(time, loop, volume);
            }
        }

        public void Stop()
        {
            foreach (Layer layer in this.layerList)
            {
                layer.Stop();
            }
        }

        public void Pause()
        {
            foreach (Layer layer in this.layerList)
            {
                layer.Pause();
            }
        }

        public void Resume(bool loop = true)
        {
            foreach (Layer layer in this.layerList)
            {
                layer.Resume(loop);
            }
        }

        public void SetVolume(float volume)
        {
            foreach (Layer l in layerList)
            {
                l.SetVolume(volume);
            }
        }

        public float GetTrackTime()
        {
            foreach(Layer l in layerList)
            {
                foreach (Track t in l.tracksList)
                {
                    if (t.AudioSource.isPlaying)
                        return t.AudioSource.time;
                }
            }
            return 0;
        }
    }
}