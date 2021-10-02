using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNode : MonoBehaviour {

    [SerializeField] private SoundNode[] soundNodeList;

    private AudioSource audioSource;
    private SoundNodeSystem soundNodeSystem;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        soundNodeSystem = GetComponentInParent<SoundNodeSystem>();
        audioSource.clip = soundNodeSystem.audioClip;
    }

    public void Play(float distance) {
        StartCoroutine(color());
        audioSource.volume = 1f - distance / soundNodeSystem.distanceMax;
        Debug.Log(audioSource.volume);
        audioSource.Play();
    }

    public void RequestSound(SoundNode parentNode, float distance, bool clearSound) {
        if (Physics.Linecast(transform.position, Camera.main.transform.position, soundNodeSystem.soundLayerRaycast)) {
            if (parentNode) distance += Vector3.Distance(parentNode.transform.position, transform.position);
            if (distance > soundNodeSystem.distanceMax) return;
            StartCoroutine(BroadcastRequestSound(parentNode, distance, false));
            return;
        }
        if (parentNode) parentNode.Play(distance); else Play(0);
    }

    IEnumerator BroadcastRequestSound(SoundNode parentNode, float distance, bool clearSound) {
        yield return new WaitForSeconds(distance / soundNodeSystem.distanceMax);
        foreach (var node in soundNodeList) {
            if (node != parentNode) node.RequestSound(this, distance, clearSound);
        }
    }
    


    IEnumerator color() {
        var renderer = GetComponentInChildren<Renderer>();
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = Color.white;
    }

}
