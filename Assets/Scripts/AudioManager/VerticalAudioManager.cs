using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AdaptiveAudio{

    public class VerticalAudioManager : MonoBehaviour
        {
        public static VerticalAudioManager instance;
        public AudioSource audioSourcePrefab;
        public AudioPool audioPool;
        [HideInInspector]
        public AnimationCurve defaultCurve;

        void Awake() {
            if (instance == null){
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else {
                Destroy(this);
            }
            foreach(Song song in audioPool.songlist){
                foreach(Layer layer in song.layerList){
                    foreach (Track track in layer.tracksList){
                        track.AudioSource = Instantiate(audioSourcePrefab, this.transform);
                        track.AudioSource.clip = track.sound;
                    }
                }
            }
            defaultCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            defaultCurve.preWrapMode = WrapMode.Clamp;
            defaultCurve.postWrapMode = WrapMode.Clamp;
        }

        public void Play(Song song, float time = 0f, bool loop = true,float volume = 1f)
        {
            foreach (Song s in audioPool.songlist)
            {
                if (song.Equals(s))
                {
                    song.Play(time, loop, volume);
                }
                else
                {
                    song.Stop();
                }
            }
        }

        public void Play(Layer layer, float time = 0f, bool loop = true, bool stopOtherLayers = false, float volume = 1f)
        {
            if (!stopOtherLayers)
            {
                layer.Play(time, loop, volume);
            }
            else
            {
                foreach (Song s in audioPool.songlist)
                {
                    foreach (Layer l in s.layerList)
                    {
                        if (layer.Equals(l))
                            layer.Play(time, loop, volume);
                        else
                            layer.Stop();
                    }
                }
            }
        }

        public void Play(Track track, float time = 0f, bool loop = true, bool stopOthertracks = false, float volume = 1f)
        {
            if (!stopOthertracks)
            {
                track.Play(time, loop, volume);
            }
            else
            {
                foreach (Song s in audioPool.songlist)
                {
                    foreach (Layer l in s.layerList)
                    {
                        foreach (Track t in l.tracksList)
                        {
                            if (track.Equals(t))
                                track.Play(time, loop, volume);
                            else
                                track.Stop();
                        }
                    }
                }
            }
        }

        public void StopAll()
        {
            foreach (Song s in audioPool.songlist)
            {
                s.Stop();
            }
        }

        public void PauseAll()
        {
            foreach (Song s in audioPool.songlist)
            {
                s.Pause();
            }
        }

        public void Stop(Song song)
        {
            song.Stop();
        }

        public void Stop(Layer layer)
        {
            layer.Stop();
        }

        public void Stop(Track track)
        {
            track.Stop();
        }

        public void Pause(Song song)
        {
            song.Pause();
        }

        public void Pause(Layer layer)
        {
            layer.Pause();
        }

        public void Pause(Track track)
        {
            track.Pause();
        }

        public void Resume(Song song, bool loop = true)
        {
            song.Resume(loop);
        }

        public void Resume(Layer layer, bool loop = true)
        {
            layer.Resume(loop);
        }

        public void Resume(Track track, bool loop = true)
        {
            track.Resume(loop);
        }

        public void FadeIn(Song song, AnimationCurve animationCurveType, bool loop = true, float fadeDuration = 3f, float time = 0f)
        {
            foreach (Layer l in song.layerList)
            {
                FadeIn(l, animationCurveType, loop, fadeDuration, time);
            }
        }

        public void FadeIn(Layer layer, AnimationCurve animationCurveType, bool loop = true, float fadeDuration = 3f, float time = 0f)
        {
            foreach(Track t in layer.tracksList)
            {
                FadeIn(t, animationCurveType, loop, fadeDuration, time);
            }
        }

        public void FadeIn(Track track, AnimationCurve animationCurveType, bool loop = true, float fadeDuration = 3f, float time = 0f)
        {
            track.StopFadingOut();
            track.IsFadingIn = true;
            StartCoroutine(FadeInStart(track, animationCurveType, loop, fadeDuration, time));
        }

        private IEnumerator FadeInStart(Track track, AnimationCurve animationCurveType, bool loop = true, float fadeDuration = 3f, float time = 0f, float volume = 1f)
        {
            float timer = 0f;
            float initialVolume = 0f;
            if(track.AudioSource.isPlaying)
                initialVolume = track.AudioSource.volume;
            track.Play(time, loop, initialVolume);
            
            while (timer < fadeDuration && track.IsFadingIn)
            {
                timer += Time.deltaTime;
                track.AudioSource.volume = Mathf.Lerp(initialVolume, volume, animationCurveType.Evaluate(timer / fadeDuration));
                yield return null;
            }          

            track.IsFadingIn = false;
            yield break;
        }

        public void FadeOut(Song song, AnimationCurve animationCurveType, float fadeDuration = 3f)
        {
            foreach (Layer l in song.layerList)
            {
                FadeOut(l, animationCurveType, fadeDuration);
            }
        }

        public void FadeOut(Layer layer, AnimationCurve animationCurveType, float fadeDuration = 3f)
        {
            foreach (Track t in layer.tracksList)
            {
                FadeOut(t, animationCurveType, fadeDuration);
            }
        }

        public void FadeOut(Track track, AnimationCurve animationCurveType, float fadeDuration = 3f)
        {
            track.StopFadingIn();
            track.IsFadingOut = true;
            StartCoroutine(FadeOutStart(track, animationCurveType, fadeDuration));
        }


        private IEnumerator FadeOutStart(Track track, AnimationCurve animationCurveType, float fadeDuration = 3f)
        {
            if (track.AudioSource.isPlaying)
            {
                float timer = 0f;
                float startVolume = track.AudioSource.volume;

                while (timer < fadeDuration && track.IsFadingOut)
                {
                    if (track.IsFadingIn)
                    {
                        yield break;
                    }
                    timer += Time.deltaTime;
                    track.AudioSource.volume = Mathf.Lerp(startVolume, 0f, animationCurveType.Evaluate(timer / fadeDuration));
                    yield return null;
                }
                if (track.IsFadingOut)
                {
                    track.Stop();
                }
                track.IsFadingOut = false;
            }
            yield break;
        }

        public void FadeOutAll(AnimationCurve animationCurveType, float fadeDuration = 3f)
        {
            foreach (Song s in audioPool.songlist)
            {
                FadeOut(s, animationCurveType, fadeDuration);
            }
        }

        public void CrossFade(Track trackIn, Track trackOut, AnimationCurve animationCurveIn,  AnimationCurve animationCurveOut, bool loop = true, float fadeDuration = 3f, float time = 0f)
        {
            FadeIn(trackIn, animationCurveIn, loop, fadeDuration, time);
            FadeOut(trackOut, animationCurveOut, fadeDuration);
        }

        public void CrossFade(Layer layerIn, Layer layerOut, AnimationCurve animationCurveIn, AnimationCurve animationCurveOut, bool loop = true, float fadeDuration = 3f, float time = 0f)
        {
            FadeIn(layerIn, animationCurveIn, loop, fadeDuration, time);
            FadeOut(layerOut, animationCurveOut, fadeDuration);
        }

        public void CrossFade(Song songIn, Song songOut, AnimationCurve animationCurveIn, AnimationCurve animationCurveOut, bool loop = true, float fadeDuration = 3f, float time = 0f)
        {
            FadeIn(songIn, animationCurveIn, loop, fadeDuration, time);
            FadeOut(songOut, animationCurveOut, fadeDuration);
        }

        public bool AudioIsPlaying()
        {
            foreach (Song song in audioPool.songlist)
            {
                foreach (Layer layer in song.layerList)
                {
                    foreach (Track t in layer.tracksList)
                    {
                        if (t.AudioSource.isPlaying)
                            return false;
                    }
                }
            }
            return true;
        }

        public Song SearchForSong(string songName)
        {
            foreach (Song song in audioPool.songlist)
            {
                if (String.Equals(songName, song.name, StringComparison.OrdinalIgnoreCase))
                {
                    return song;
                }
            }
            throw new System.ArgumentException("No Song found");
        }

        public Layer SearchForLayer(string layerName){
            foreach(Song song in audioPool.songlist){
                foreach(Layer layer in song.layerList){
                    if(String.Equals(layerName, layer.name, StringComparison.OrdinalIgnoreCase)) { 
                        return layer;
                    }
                }
            }
             throw new System.ArgumentException("No Layer found");
        }

        public Track SearchForTrack(string trackName){
            foreach(Song song in audioPool.songlist){
                foreach(Layer layer in song.layerList){
                    foreach(Track track in layer.tracksList){
                        if(String.Equals(trackName, track.name, StringComparison.OrdinalIgnoreCase)){
                            return track;
                        }
                    }
                }
            }
            throw new System.ArgumentException("No track found");
        }
    }
}