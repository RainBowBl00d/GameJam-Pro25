using AdaptiveAudio;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public List<Track> trackList;
    public int gameState;
    public WhenTrackFinishes whenTrackFinishes;
}

public class HorizontalAudioManager : MonoBehaviour
{
    public AudioSource audioSourcePrefab;
    public List<GameState> gameStateList;
    private bool startedPlaying = false;
    private int currentGameState = 0;
    public Track currentTrack = new Track();
    public int CurrentGameState
    {
        get => currentGameState; set
        {
            if (value >= 0 && value < gameStateList.Count)
            {
                currentGameState = value;
            }
            else
            {
                Debug.Log("Value " + value + " isn't a valid GameState");
            }
        }
    }

    private AudioSource currentAudioSource;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        InstanceAwake();
        foreach (GameState gameState in gameStateList)
        {
            foreach (Track track in gameState.trackList)
            {
                track.AudioSource = Instantiate(audioSourcePrefab, this.transform);
                track.AudioSource.clip = track.sound;
            }
        }
    }

    private void Update()
    {
        if (startedPlaying && !isTrackPlaying())
        {
            StopAllTracks();
            switch (SearchGameState(currentGameState).whenTrackFinishes)
            {
                case WhenTrackFinishes.PlayAndIncrement:
                    PlayRandomTrack(SearchGameState(currentGameState));
                    CurrentGameState++;
                    break;
                case WhenTrackFinishes.PlayAnotherTrack:
                    PlayRandomTrack(SearchGameState(currentGameState));
                    break;
                case WhenTrackFinishes.PlayAndStop:
                    PlayRandomTrack(SearchGameState(currentGameState));
                    StopPlaying();
                    break;
            }
        }
    }

    public void setVolume(float volume)
    {
        foreach (GameState gameState in gameStateList)
        {
            foreach (Track track in gameState.trackList)
            {
                track.SetVolume(volume);
            }
        }
    }

    void PlayRandomTrack(GameState gameState)
    {
        int choice = UnityEngine.Random.Range(0, gameState.trackList.Count);
        Debug.Log("GameState: " + this.currentGameState.ToString() + "   Choice: " + choice.ToString());
        SwitchToNextTrack(gameState);
    }

    void PlayTrack(Track track)
    {
        if (currentAudioSource != null && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        currentAudioSource = track.AudioSource;
        track.Play(loop: false);
    }

    public void StartPlaying()
    {
        GameState gameState = SearchGameState(this.currentGameState);
        if (!startedPlaying)
        {
            PlayRandomTrack(gameState);
            startedPlaying = true;
        }
        if (gameState.whenTrackFinishes == WhenTrackFinishes.PlayAndIncrement)
        {
            this.currentGameState++;
        }
    }

    public void StopPlaying()
    {
        startedPlaying = false;
    }

    void StopAllTracks()
    {
        foreach (GameState state in gameStateList)
        {
            foreach (Track track in state.trackList)
            {
                track.AudioSource.Stop();
            }
        }
    }

    bool isTrackPlaying()
    {
        foreach (GameState state in gameStateList)
        {
            foreach (Track track in state.trackList)
            {
                if (track.AudioSource.isPlaying)
                {
                    return true;
                }
            }
        }
        return false;
    }

    GameState SearchGameState(int gameState)
    {
        foreach (GameState state in gameStateList)
        {
            if (gameState == state.gameState)
            {
                return state;
            }
        }
        throw new System.Exception("GameState not found");
    }

    public static HorizontalAudioManager instance;
    void InstanceAwake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        if (track.AudioSource.isPlaying)
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

    public IEnumerator CrossFade(Track trackIn, Track trackOut, AnimationCurve animationCurveIn, AnimationCurve animationCurveOut, bool loop = true, float fadeDuration = 3f, float time = 0f)
    {
        FadeIn(trackIn, animationCurveIn, loop, fadeDuration, time);
        FadeOut(trackOut, animationCurveOut, fadeDuration);
        yield return null;
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

    private void CrossfadeToNewTrack(Track newTrack, float fadeDuration = 0.4f)
    {
        if (currentAudioSource != null)
        {
            fadeCoroutine = StartCoroutine(CrossFade(newTrack, currentTrack, new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1)), new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1)), false, fadeDuration));
            currentAudioSource = newTrack.AudioSource;
        }
        else
        {
            PlayTrack(newTrack);
        }
    }

    public void SwitchToNextTrack(GameState gameState)
    {
        int choice = UnityEngine.Random.Range(0, gameState.trackList.Count);
        Track newTrack = gameState.trackList[choice];
        CrossfadeToNewTrack(newTrack);
        currentTrack = newTrack;
    }
}

public enum WhenTrackFinishes
{
    PlayAnotherTrack,
    PlayAndIncrement,
    PlayAndStop
}
