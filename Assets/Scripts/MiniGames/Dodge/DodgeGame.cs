using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGame : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] GameObject bulletPrefab;
    Vector2 min, max;
    bool running = false;


    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        min = box.bounds.min;
        max = box.bounds.max;

    }

    public void GenerateGame(DodgeGameLevelStats stats)
    {
        if (running) return;
        StartCoroutine(SpawnBulletRoutine(stats));
    }

    #region Helper Methods

    private IEnumerator SpawnBulletRoutine(DodgeGameLevelStats stats)
    {
        running = true;
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
        running = false;
        if (((stats.rOF * stats.time) - stats.hits) / (stats.rOF * stats.time) >= stats.DodgeFactor) stats.Completed = true;
    }

    Vector2 GetRandom()
    {
        float y = Random.Range(0, 2) == 1 ? max.y : min.y;
        float x = Random.Range(min.x, max.x); 
        return new Vector2(x, y);
    }

    #endregion
}

