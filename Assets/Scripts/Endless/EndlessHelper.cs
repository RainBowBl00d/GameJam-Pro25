using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessHelper : MonoBehaviour
{
    [SerializeField] private FFAGame _ffaGame;
    [SerializeField] private KeyNotesGame _keyNotesGame;
    [SerializeField] private DodgeGame _dodgeGame;

    [SerializeField] private GameObject _ffaGameObject, _keyNotesObject, _dodgeGameObject;

    [SerializeField] private FFALevelStats _ffaLevelStats;
    [SerializeField] private KeyNoteGameLevelStats _keyNoteGameLevelStats;
    [SerializeField] private DodgeGameLevelStats _dodgeGameLevelStats;


    IEnumerator RoundManager()
    {
        ChooseRandomGame().SetActive(true);
        
    }

    GameObject ChooseRandomGame()
    {
        int random = Random.Range(0, 3);
        return random == 0 ? _keyNotesObject : random == 1 ? _dodgeGameObject : _ffaGameObject;
    }

    FFALevelStats IncrementFFA(FFALevelStats stats)
    {
        FFALevelStats newStats = stats;
        newStats.EnemySpeed += Random.Range(0, 4f);
        newStats.SpawnRate += Random.Range(0, 4f);
        newStats.SpawnTime += Random.Range(0, 4f);
        newStats.MaxLayers += Random.Range(0, 2);
        return newStats;
    }

    DodgeGameLevelStats IncrementDodge(DodgeGameLevelStats stats)
    {
        DodgeGameLevelStats newStats = stats;
        newStats.rOF += Random.Range(0, 4);
        newStats.time += Random.Range(0, 4f);
        newStats.bulletSpeed += Random.Range(0, 4f);
        return newStats;
    }

    KeyNoteGameLevelStats IncrementKey(KeyNoteGameLevelStats stats)
    {
        KeyNoteGameLevelStats newStats = stats;
        newStats.sequenceLength += Random.Range(0, 4);
        newStats.sortHorizontally = !newStats.sortHorizontally;
        newStats.ascending = !newStats.ascending;
        return newStats;
    }
}
