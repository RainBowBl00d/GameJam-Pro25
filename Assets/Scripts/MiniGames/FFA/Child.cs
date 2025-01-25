using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public int colorIndex;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(colorIndex== 0  && collision.tag == "Weapon_R" && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
        else if (colorIndex == 1 && collision.tag == "Weapon_G" && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
        else if (colorIndex == 2 && collision.tag == "Weapon_B" && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }

    }
}
