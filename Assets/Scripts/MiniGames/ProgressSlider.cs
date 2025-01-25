using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    [SerializeField] private Slider progress;
    [SerializeField] private Slider required;
    private KeyNoteGameLevelStats _stats;

    void Update()
    {
        if (_stats != null)
        {
            progress.value = _stats.Correct;
        }
    }
    
    public void StartGame(KeyNoteGameLevelStats stats)
    {
        _stats = stats;
        progress.value = 0;
        progress.maxValue = (int) (stats.sequenceLength);
        required.value = stats.hitFactorRequirement;
    }
}
