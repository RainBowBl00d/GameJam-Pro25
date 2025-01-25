using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public FFALevelStats stats;
    public GameObject player;
    

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, stats.EnemySpeed * Time.deltaTime);
        
        if (!HasActiveChildren(gameObject) && transform.childCount != 0)
        {
            GetChild().SetActive(true);
        }
        else if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
    
    GameObject GetChild()
    {
        return transform.GetChild(0).gameObject;
    }
    public bool HasActiveChildren(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                return true; 
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            stats.Hits++;
            Destroy(gameObject);
        }
    }

}
