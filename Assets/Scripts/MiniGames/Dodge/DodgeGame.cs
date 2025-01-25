using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGame : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] GameObject bulletPrefab;
    Vector2 min, max;


    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        min = box.bounds.min;
        max = box.bounds.max;

        GenerateGame(3f, 5f, 10f, 0f, 3f, 3f);
    }

    public void GenerateGame(float rOF, float bulletSpeed, float time, float sineWeight, float sineFrequency, float sineAmplitude)
    {
        StartCoroutine(SpawnBulletRoutine(rOF, bulletSpeed,time ,sineWeight, sineFrequency, sineAmplitude));
    }

    #region Helper Methods

    private IEnumerator SpawnBulletRoutine(float rOF, float bulletSpeed, float time,  float sineWeight, float sineFrequency, float sineAmplitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            Vector2 spawnPosition = GetRandom();
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new(cursorPosition.x, cursorPosition.y);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.parent = transform;
            Bullet _bullet = bullet.GetComponent<Bullet>();
            _bullet.direction = direction - spawnPosition;
            _bullet.speed = bulletSpeed;
            _bullet.sineWeight = sineWeight;
            _bullet.sineFrequency = sineFrequency;
            _bullet.sineAmplitude = sineAmplitude;

            elapsedTime += 1f / rOF;
            yield return new WaitForSeconds(1f / rOF); 
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

