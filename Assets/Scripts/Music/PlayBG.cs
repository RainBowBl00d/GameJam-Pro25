using UnityEngine;

public class PlayBG : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<HorizontalAudioManager>().StartPlaying();
    }
}
