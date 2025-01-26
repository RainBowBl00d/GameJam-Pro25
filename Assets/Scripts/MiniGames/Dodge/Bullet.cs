using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.right; 
    public float speed = 5f; 
    public float sineAmplitude = 1f; 
    public float sineFrequency = 1f;
    [Range(0f, 1f)]
    public float sineWeight = 0f;

    public HorizontalAudioManager manager;

    private float timeOffset;

    public DodgeGameLevelStats stats;

    private void Start()
    {
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update()
    {
        MoveWithSineWave();
    }

    private void MoveWithSineWave()
    {
        direction.Normalize();

        Vector2 linearMovement = direction * speed * Time.deltaTime;

        Vector2 sineMovement = Vector2.Perpendicular(direction) * Mathf.Sin((Time.time + timeOffset) * sineFrequency) * sineAmplitude;

        Vector2 finalMovement = linearMovement + sineMovement * sineWeight;

        transform.Translate(finalMovement);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mouse")
        {
            stats.hits++;
            manager.StartPlaying();
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Dodge")
        {
            Destroy(gameObject);
        }
    }
}
