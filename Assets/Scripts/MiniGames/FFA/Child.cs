using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Child : MonoBehaviour
{
    public int colorIndex;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(colorIndex== 0  && collision.tag == "Weapon_R")
        {
            AudioManager.instance.Play("Hit");
            Destroy(gameObject);
        }
        else if (colorIndex == 1 && collision.tag == "Weapon_G" )
        {
            AudioManager.instance.Play("Hit");
            Destroy(gameObject);
        }
        else if (colorIndex == 2 && collision.tag == "Weapon_B" )
        {
            AudioManager.instance.Play("Hit");
            Destroy(gameObject);
        }

    }
}
