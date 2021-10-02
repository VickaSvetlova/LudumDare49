using UnityEngine;
using System.Collections;

public class AmbientSoundSystem : MonoBehaviour {

    public static AmbientSoundSystem main { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float fadeOutSpeed;
    private float clipVolume;
    private float _factor = 1f;

    private void Awake() {
        main = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float targetVolume) {
        if (audioSource.clip == clip) return;
        StopAllCoroutines();
        clipVolume = targetVolume;
        audioSource.volume = targetVolume;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySmooth(AudioClip clip, float targetVolume) {
        StopAllCoroutines();
        StartCoroutine(IEPlaySmooth(clip, targetVolume));
    }

    IEnumerator IEPlaySmooth(AudioClip clip, float targetVolume) {
        if (audioSource.clip != clip) {
            if (audioSource.isPlaying) yield return IEFadeOut();
            audioSource.Stop();
            audioSource.clip = clip;
            clipVolume = targetVolume;
            if (clip) audioSource.Play();
        }
        if (clip) yield return IEFadeIn();
    }

    IEnumerator IEFadeOut() {
        float startOutVolume = audioSource.volume;
        float fadeOutFactor = startOutVolume / clipVolume;
        while (fadeOutFactor > 0f) {
            yield return new WaitForSecondsRealtime(0.02f);
            fadeOutFactor -= fadeOutSpeed;
            audioSource.volume = Mathf.Lerp(0f, clipVolume, fadeOutFactor);
        }
    }

    IEnumerator IEFadeIn() {
        float startInVolume = audioSource.volume;
        float fadeInFactor = startInVolume / clipVolume;
        while (fadeInFactor < 1f) {
            yield return new WaitForSecondsRealtime(0.02f);
            fadeInFactor += fadeInSpeed;
            audioSource.volume = Mathf.Lerp(0f, clipVolume, fadeInFactor);
        }
    }

}
