using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNote : MonoBehaviour
{
    public KeyCode Key = KeyCode.A;
    public bool Correct = false;
    public float TimeForPressing = 5;
    public bool StartShirking = false;
    float _timePassed = 0f;

    void Update()
    {
        if (StartShirking)
        {
            _timePassed += Time.deltaTime;
            transform.localScale = new((TimeForPressing - _timePassed) / TimeForPressing, (TimeForPressing - _timePassed) / TimeForPressing, 1f) ;
        }
        if(TimeForPressing - _timePassed <= 0)
        {
            SendAndDestroy();
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Mouse")
        {
            if (Input.GetKeyDown(Key))
            {
                Correct = true;
                SendAndDestroy();
            }
            else if (Input.anyKeyDown && !Input.GetKeyDown(Key))
            {
                SendAndDestroy();
            }
        }
    }

    public void SendAndDestroy()
    {
        GetComponentInParent<KeyNotesGame>()._keyNotes.Remove(gameObject);
        Destroy(gameObject);
    }

}
