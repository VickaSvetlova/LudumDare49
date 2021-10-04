using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour {

    private Character character;

    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioSource headAudioSource;

    [SerializeField] private AudioClip[] stepClips;

    private void Awake() {
        character = GetComponentInParent<Character>();
    }

    public void PlayStepSound() {
        if (!character.movement.isGrounded) return;
        footAudioSource.clip = stepClips[0];
        footAudioSource.pitch = Random.Range(0.9f, 1.1f);
        footAudioSource.Play();
    }

    public void PlayJumpSound() {
        //footAudioSource.clip = jumpClips[Random.Range(0, jumpClips.Length)];
        footAudioSource.clip = stepClips[0];
        footAudioSource.pitch = Random.Range(0.9f, 1.1f);
        footAudioSource.Play();
    }

    public void PlayTalkSound() {
        headAudioSource.Play();
    }

}
