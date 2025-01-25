using AdaptiveAudio;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState {
    public List<Track> trackList;
    public int gameState;
    public WhenTrackFinishes whenTrackFinishes;
}

public class HorizontalAudioManager : MonoBehaviour{
    public AudioSource audioSourcePrefab;
    public List<GameState> gameStateList;
    private bool startedPlaying = false;
    private int currentGameState = 0;
    public int CurrentGameState { get => currentGameState; set {
            if (value >= 0 && value < gameStateList.Count)
            {
                currentGameState = value;
            }
            else
            {
                print("Value " + value + " isn't a valid GameState");
            }
        }
    }
    
    void Awake()
    {
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
        if (startedPlaying && !isTrackPlaying()){
            StopAllTracks();
            switch (SearchGameState(currentGameState).whenTrackFinishes) {
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
        print("GameState: " + this.currentGameState.ToString() + "   Choice: " + choice.ToString());
        PlayTrack(gameState.trackList[choice]);
    }

    void PlayTrack(Track track) {
        track.Play(loop: false);
    }

    public void StartPlaying()
    {
        GameState gameState = SearchGameState(this.currentGameState);
        if (startedPlaying == false)
        {
            PlayRandomTrack(gameState);
            startedPlaying = true;
        }
        if (gameState.whenTrackFinishes == WhenTrackFinishes.PlayAndIncrement) {
            this.currentGameState++;
        }
    }

    public void StopPlaying()
    {
        startedPlaying = false;
    }

    void StopAllTracks() {
        foreach (GameState state in gameStateList)
        {
         foreach(Track track in state.trackList)
            {
                track.AudioSource.Stop();
            }
        }
    }

    bool isTrackPlaying() {
        foreach(GameState state in gameStateList)
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

    GameState SearchGameState(int gameState) { 
        foreach(GameState state in gameStateList)
        {
            if(gameState == state.gameState)
            {
                return state;
            }
        }
        throw new System.Exception("GameState inexistente");
    }
}

public enum WhenTrackFinishes{
    PlayAnotherTrack,
    PlayAndIncrement,
    PlayAndStop
}