using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DodgeGame : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] GameObject bulletPrefab;
    Vector2 min, max;
    bool running = false;
    public GameObject mouse;
    //private bool _lockMouse;
    private Vector2 _position;
    
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        min = box.bounds.min;
        max = box.bounds.max;
        _position = mouse.transform.position;
    }
    
    private void LateUpdate()
    {
        // get the current position
        Vector3 newPosition = mouse.transform.position;

        // limit the x and y positions to be between the area's min and max x and y.
        Vector2 clampedPosition =
            new Vector2(Mathf.Clamp(newPosition.x, min.x, max.x),
                Mathf.Clamp(newPosition.y, min.y, max.y));

        // if locked, use starting position, otherwise use clamped position
        if (running)
        {
            newPosition.x = clampedPosition.x;
            newPosition.y = clampedPosition.y;
            mouse.transform.position = newPosition;
        }
    }

    public void GenerateGame(DodgeGameLevelStats stats)
    {
        StartCoroutine(SpawnBulletRoutine(stats));
    }

    #region Helper Methods

    private IEnumerator SpawnBulletRoutine(DodgeGameLevelStats stats)
    {
        running = true;
        stats.Running = true;
        float elapsedTime = 0f;

        while (elapsedTime < stats.time)
        {
            Vector2 spawnPosition = GetRandom();
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new(cursorPosition.x, cursorPosition.y);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.parent = transform;
            Bullet _bullet = bullet.GetComponent<Bullet>();
            _bullet.direction = direction - spawnPosition;
            _bullet.speed = stats.bulletSpeed;
            _bullet.sineWeight = stats.sineWeight;
            _bullet.sineFrequency = stats.sineFrequency;
            _bullet.sineAmplitude = stats.sineAmplitude;
            _bullet.stats = stats;

            elapsedTime += 1f / stats.rOF;
            yield return new WaitForSeconds(1f / stats.rOF); 
        }

        stats.Running = false;
        running = false;
        if (((stats.rOF * stats.time) - stats.hits) / (stats.rOF * stats.time) >= stats.DodgeFactor)
        {
            stats.Completed = true;
        }
    }

    Vector2 GetRandom()
    {
        float y = Random.Range(0, 2) == 1 ? max.y : min.y;
        float x = Random.Range(min.x, max.x); 
        return new Vector2(x, y);
    }

    #endregion
}

