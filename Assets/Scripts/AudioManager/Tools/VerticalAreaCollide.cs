using UnityEngine;

namespace AdaptiveAudio
{
    public class VerticalAreaCollide : MonoBehaviour
    {
        VerticalAudioManager AudioManager;
        public Area area;

        void Start()
        {
            AudioManager = VerticalAudioManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (area)
            {
                case Area.red:
                    AudioManager.FadeOut(AudioManager.SearchForTrack("Bass"), AudioManager.defaultCurve);
                    AudioManager.FadeOut(AudioManager.SearchForTrack("Percussion"), AudioManager.defaultCurve);
                    AudioManager.Play(AudioManager.SearchForTrack("Guitar"), loop: true, volume: 1f, time: AudioManager.SearchForSong("Main").GetTrackTime());
                    break;
                case Area.green:
                    AudioManager.FadeOut(AudioManager.SearchForTrack("Flute"), AudioManager.defaultCurve);
                    AudioManager.FadeIn(AudioManager.SearchForTrack("Percussion"), AudioManager.defaultCurve, loop: true, fadeDuration: 3, volume: 1f, AudioManager.SearchForSong("Main").GetTrackTime());
                    AudioManager.FadeIn(AudioManager.SearchForTrack("Bass"), AudioManager.defaultCurve, loop: true, fadeDuration: 3, volume: 1f, AudioManager.SearchForSong("Main").GetTrackTime());
                    break;
                case Area.blue:
                    AudioManager.FadeIn(AudioManager.SearchForLayer("Slow"), AudioManager.defaultCurve, time: AudioManager.SearchForSong("Main").GetTrackTime());
                    AudioManager.FadeOut(AudioManager.SearchForLayer("Fast"), AudioManager.defaultCurve);
                    break;
                case Area.yellow:
                    AudioManager.CrossFade(AudioManager.SearchForLayer("Fast"), AudioManager.SearchForLayer("Slow"), AudioManager.defaultCurve, AudioManager.defaultCurve, loop: true, volume: 1, fadeDuration: 3);
                    break;
                default:
                    break;
            }
        }
    }
    public enum Area
    {
        red, green, blue, yellow
    }
}