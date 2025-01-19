using AdaptiveAudio;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioPool", menuName = "AudioPool", order = 1)]
public class AudioPool : ScriptableObject
{
    public List<Song> songlist;
}