using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyNotesGame : MonoBehaviour
{
    [SerializeField] GameObject keyNote;
    [SerializeField] string[] letters;
    BoxCollider2D box;
    Vector2 min, max;
    public List<GameObject> _keyNotes;
    public float timeBtwDecreases;
    float _timePassedAfterLastDecrease = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
        box = GetComponent<BoxCollider2D>();
        min = box.bounds.min;
        max = box.bounds.max;

        GenerateMiniGame(15, true, false, 2f);
    }

    private void Update()
    {
        _timePassedAfterLastDecrease += Time.deltaTime;
        if(_timePassedAfterLastDecrease >= timeBtwDecreases && _keyNotes.Count != 0)
        foreach (GameObject note in _keyNotes)
        {
            KeyNote keyNote = note.GetComponent<KeyNote>();
            if (keyNote.StartShirking == false)
            {
                keyNote.StartShirking = true;
                _keyNotes.Remove(note);
                _timePassedAfterLastDecrease = 0f;
                break;
                  
            }
        }
    }

    public void GenerateMiniGame(int SequenceLength, bool sortHorizontally, bool ascending, float timeToWaitBtwRealses)
    {
        List<Vector2> vector2s = GetRandomPosInBox(SequenceLength);
        vector2s = SortVector2List(vector2s, sortHorizontally, ascending);
        StartCoroutine(SpawnNotes(vector2s, timeToWaitBtwRealses));
    }
    #region Helper
    private IEnumerator SpawnNotes(List<Vector2> positions, float timeToWait)
    {
        foreach (Vector2 position in positions)
        {
            StartCoroutine(GenNote(position, timeToWait));
            yield return new WaitForSecondsRealtime(timeToWait);
        }
    }

    private IEnumerator GenNote(Vector2 position, float timeToWait)
    {
        GameObject note = Instantiate(keyNote, position, Quaternion.identity);
        note.transform.parent = transform;
        _keyNotes.Add(note);

        string randomString = GetRandomString(letters);
        KeyNote keyNoteComponent = note.GetComponent<KeyNote>();

        if (keyNoteComponent != null)
        {
            keyNoteComponent.Key = StringToKeyCodeConverter(randomString);
        }

        TMP_Text textComponent = note.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = randomString;
        }

        // Wait for 3 seconds before finishing this coroutine (if needed)
        yield return new WaitForSecondsRealtime(3f);
    }
    List<Vector2> GetRandomPosInBox(int length)
    {
        List<Vector2> vector2s = new List<Vector2>(); 
        for (int x = 0; x < length; x++)
        {
            vector2s.Add(new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)));
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

        // Return KeyCode.None if the string doesn't match any valid KeyCode
        return KeyCode.None;
    }
    #endregion
}
