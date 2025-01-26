using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Child : MonoBehaviour
{
    public int colorIndex;
    public HorizontalAudioManager manager;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(colorIndex== 0  && collision.tag == "Weapon_R" && Input.GetMouseButtonDown(0))
        {
            manager.StartPlaying();
            Destroy(gameObject);
        }
        else if (colorIndex == 1 && collision.tag == "Weapon_G" && Input.GetMouseButtonDown(0))
        {
            manager.StartPlaying();
            Destroy(gameObject);
        }
        else if (colorIndex == 2 && collision.tag == "Weapon_B" && Input.GetMouseButtonDown(0))
        {
            manager.StartPlaying();
            Destroy(gameObject);
        }

    }
}
