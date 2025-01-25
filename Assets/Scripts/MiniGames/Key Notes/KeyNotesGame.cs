using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class KeyNotesGame : MonoBehaviour
{
    [SerializeField] GameObject keyNote;
    [SerializeField] string[] letters;
    public BoxCollider2D box;
    private Vector2 _min, _max;
     public  List<GameObject> keyNotes;
    public float timeBtwDecreases;
    private float _timePassedAfterLastDecrease = 0f;
    bool _isrunning = false;

    void Start()
    {
        //box = GetComponent<BoxCollider2D>();
        _min = box.bounds.min;
        _max = box.bounds.max;

        keyNotes = new List<GameObject>();
        //GenerateMiniGame(15, true, false, 2f);
    }

    private void Update()
    {
        _timePassedAfterLastDecrease += Time.deltaTime;
        if (_timePassedAfterLastDecrease >= timeBtwDecreases && keyNotes.Count != 0)
            foreach (GameObject note in keyNotes)
            {
                KeyNote _ = note.GetComponent<KeyNote>();
                if (_.startShirking == false)
                {
                    _.startShirking = true;
                    _timePassedAfterLastDecrease = 0f;
                    break;
                }
            }
    }

    public void GenerateMiniGame(KeyNoteGameLevelStats stats)
    {
        if (_isrunning) return;
        _isrunning = true;
        Debug.Log($"Generate game: {stats.sequenceLength} {stats.sortHorizontally} {stats.ascending} {stats.timeToWaitBtwRealses}");
        List<Vector2> vector2s = GetRandomPosInBox(stats.sequenceLength);
        Debug.Log("Got vectors");
        vector2s = SortVector2List(vector2s, stats.sortHorizontally, stats.ascending);
        StartCoroutine(SpawnNotes(vector2s, stats.timeToWaitBtwRealses, stats));
    }

    #region Helper
    private IEnumerator SpawnNotes(List<Vector2> positions, float timeToWait, KeyNoteGameLevelStats stats)
    {
        foreach (Vector2 position in positions)
        {
            StartCoroutine(GenNote(position, stats));
            yield return new WaitForSecondsRealtime(timeToWait);
        }
        while(keyNotes.Count != 0) yield return new WaitForSecondsRealtime(1f);
        if (stats.Correct / (stats.Missed + stats.Correct) >= stats.hitFactorRequirement)
        {
            stats.Completed = true;
        }
        stats.Missed = 0;
        stats.Correct = 0;
        _isrunning = false;
    }

    private IEnumerator GenNote(Vector2 position, KeyNoteGameLevelStats stats)
    {
        GameObject note = Instantiate(keyNote, position, Quaternion.identity);
        KeyNote keyNoteComponent = note.GetComponent<KeyNote>();
        note.transform.parent = transform;
        keyNoteComponent.stats = stats;
        keyNotes.Add(note);

        string randomString = GetRandomString(letters);

        if (keyNoteComponent != null)
        {
            keyNoteComponent.key = StringToKeyCodeConverter(randomString);
        }

        TMP_Text textComponent = note.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = randomString;
        }

        yield return null;
    }

    List<Vector2> GetRandomPosInBox(int length)
    {
        List<Vector2> vector2s = new List<Vector2>();
        for (int x = 0; x < length; x++)
        {
            vector2s.Add(new Vector2(Random.Range(_min.x, _max.x), Random.Range(_min.y, _max.y)));
        }

        return vector2s;
    }

    public static List<Vector2> SortVector2List(List<Vector2> vectorList, bool sortHorizontally, bool ascending)
    {
        if (vectorList == null || vectorList.Count == 0)
            return new List<Vector2>();

        vectorList.Sort((v1, v2) =>
        {
            int compareValue = sortHorizontally
                ? v1.x.CompareTo(v2.x)
                : v1.y.CompareTo(v2.y);

            return ascending ? compareValue : -compareValue;
        });

        return vectorList;
    }

    private string GetRandomString(string[] array)
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogError("Array is null or empty!");
            return string.Empty;
        }

        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }

    private KeyCode StringToKeyCodeConverter(string keyString)
    {
        if (System.Enum.TryParse(keyString, true, out KeyCode keyCode))
        {
            return keyCode;
        }

        return KeyCode.None;
    }

    #endregion
}