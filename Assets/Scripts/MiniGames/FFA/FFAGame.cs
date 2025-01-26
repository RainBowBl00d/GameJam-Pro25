using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFAGame : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Weapon1_O, Weapon2_O, Weapon3_O;
    [SerializeField] CircleCollider2D circle;
    [SerializeField] GameObject Enemy_R, Enemy_B, Enemy_G, Enemy_Pre;
    public float speed;

    bool running;

    private Vector2 Direction;

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Weapon1_O.SetActive(true);
            Weapon2_O.SetActive(false);
            Weapon3_O.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Weapon1_O.SetActive(false);
            Weapon2_O.SetActive(true);
            Weapon3_O.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Weapon1_O.SetActive(false);
            Weapon2_O.SetActive(false);
            Weapon3_O.SetActive(true);
        }
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = Player.transform.position;
        Direction = (cursorPos - playerPos).normalized;
    }

    void RotateTowards()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * speed);

    }


    void Update()
    {
        GetInput();
        RotateTowards();
        
    }
    public void GenerateGame(FFALevelStats stats)
    {
        if (running) return;
        StartCoroutine(EnemyRoutine(stats));
    }
    #region Helper
    IEnumerator EnemyRoutine(FFALevelStats stats)
    {
        running = true;
        float elapsedTime = 0f;

        while (elapsedTime < stats.SpawnTime)
        {
            GenerateEnemy(stats);

            elapsedTime += 1f / stats.SpawnRate;
            yield return new WaitForSeconds(1f / stats.SpawnRate);
        }
        running = false;
        if (stats.Hits < stats.lives) stats.Completed = true;
    }
    void GenerateEnemy(FFALevelStats stats)
    {
        GameObject enemy = Instantiate(Enemy_Pre, GetRandomPos(), Quaternion.identity);

        Enemy enemyComp = enemy.GetComponent<Enemy>();
        if (enemyComp != null)
        {
            enemyComp.stats = stats;
            enemyComp.player = Player;
        }
        else
        {
            Debug.LogError($"The prefab {enemy.name} does not have an Enemy component attached!");
        }

        for (int x = 0; x < Random.Range(1, stats.MaxLayers + 1); x++)
        {
            GameObject child = Instantiate(GetRandomPrefab(), enemy.transform.position, Quaternion.identity);

            child.transform.parent = enemy.transform;
            child.SetActive(false);
        }
    }

    Vector2 GetRandomPos()
    {
        float radius = circle.radius * transform.lossyScale.x; 
        Vector2 center = (Vector2)circle.transform.position + circle.offset;

        float randomAngle = Random.Range(0f, Mathf.PI * 2);

        float x = Mathf.Cos(randomAngle) * radius;
        float y = Mathf.Sin(randomAngle) * radius;

        return center + new Vector2(x, y);
    }
    GameObject GetRandomPrefab()
    {
        int random = Random.Range(0, 3);
        return random == 0 ? Enemy_B : random == 1 ? Enemy_G : Enemy_R; 
    }
    #endregion
}
