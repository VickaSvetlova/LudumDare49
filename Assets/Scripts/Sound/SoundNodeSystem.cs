using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNodeSystem : MonoBehaviour {

    public AudioClip audioClip;

    public float distanceMin, distanceMax;

    public LayerMask soundLayerRaycast;
    [SerializeField] private SoundNode soundNodeSource;


    [ContextMenu("Play")]
    private void Play() {
        soundNodeSource.RequestSound(null, 0, true);
    }


    private void Start() {
        //soundNodeSource.RequestSound(null, 0, true);
        StartCoroutine(soundRepeater());
    }


    IEnumerator soundRepeater () {
        while (enabled) {
            Play();
            yield return new WaitForSeconds(3f);
        }
    }

}
