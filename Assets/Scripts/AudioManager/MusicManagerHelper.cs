using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerHelper : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the activeSceneChanged event
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        // Subscribe to KeyNotesGameHelper events
        KeyNotesGameHelper.OnLevelChanged += OnLevelChanged;
        DodgeGameStageHelper.OnLevelChanged1 += OnLevelChanged;
        FFAGameHelper.OnLevelChanged2 += OnLevelChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        KeyNotesGameHelper.OnLevelChanged -= OnLevelChanged;
        DodgeGameStageHelper.OnLevelChanged1 -= OnLevelChanged;
        FFAGameHelper.OnLevelChanged2 -= OnLevelChanged;

    }

    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        int newBuildIndex = newScene.buildIndex;

        if (newBuildIndex == 0)
        {
            HorizontalAudioManager.instance.CurrentGameState = 4;
        }
        else if (newBuildIndex == 2 || newBuildIndex == 3 || newBuildIndex == 4)
        {
            HorizontalAudioManager.instance.CurrentGameState = 0;
        }
    }

    private void OnLevelChanged(int level)
    {
        if (level == 0)
        {
            HorizontalAudioManager.instance.CurrentGameState = 0;
        }
        else if (level == 1)
        {
            HorizontalAudioManager.instance.CurrentGameState = 1;
        }
        else if (level == 2)
        {
            HorizontalAudioManager.instance.CurrentGameState = 2;
        }
        else if (level == -1) // Current Round stopped early
        {
            HorizontalAudioManager.instance.CurrentGameState = -1;
            
        }
    }
}
