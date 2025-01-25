using UnityEngine;

public class KeyNote : MonoBehaviour
{
    public KeyCode key = KeyCode.A;
    public bool correct = false;
    public float timeForPressing = 5;
    public bool startShirking = false;
    float _timePassed = 0f;

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

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(key))
        {
            correct = true;
            SendAndDestroy();
        }
        else if (Input.anyKeyDown && !Input.GetKeyDown(key))
        {
            SendAndDestroy();
        }
    }

    public void SendAndDestroy()
    {
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
