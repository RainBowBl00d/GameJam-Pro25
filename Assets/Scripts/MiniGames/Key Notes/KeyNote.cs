using UnityEngine;

public class KeyNote : MonoBehaviour
{
    public KeyCode key = KeyCode.A;
    public bool correct = false;
    public float timeForPressing = 5;
    public bool startShirking = false;
    float _timePassed = 0f;

    public KeyNoteGameLevelStats stats;

    void Update()
    {
        if (startShirking)
        {
            _timePassed += Time.deltaTime;
            Rescale(transform, new((timeForPressing - _timePassed) / timeForPressing, (timeForPressing - _timePassed) / timeForPressing, 1f));
        }
        if(timeForPressing - _timePassed <= 0)
        {
            SendAndDestroy();
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(key) && collider.tag ==  "Mouse")
        {
            correct = true;
            SendAndDestroy();
        }
        else if (Input.anyKeyDown && !Input.GetKeyDown(key) && ! (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
        {
            SendAndDestroy();
        }
    }

    public void SendAndDestroy()
    {
        if(correct)
        {
            stats.Correct++;
        }
        else if(!correct)
        {
            stats.Missed++;
        }
        GetComponentInParent<KeyNotesGame>().keyNotes.Remove(this);
        Destroy(gameObject);
    }
    void Rescale(Transform obj, Vector3 newScale)
    {
        if (obj.root != obj)
        {
            Transform parent = obj.parent;
            obj.SetParent(null);
            obj.localScale = newScale;
            obj.SetParent(parent, true);
        }
    }
}
