using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour {

    [SerializeField] private VoxMaterialManager voxMaterialManager;

    private Character character;

    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioSource headAudioSource;

    [SerializeField] private AudioClip[] stepClips;

    [SerializeField] private AudioClip[] jumpClips;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private VoxMaterial voxMaterial;

    private void Awake() {
        character = GetComponentInParent<Character>();
    }

    public void PlayStepSound() {
        if (!character.movement.isGrounded) return;
        RaycastHit hit;
        VoxMaterial newVoxMaterial = VoxMaterial.Null;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, layerMask)) {
            newVoxMaterial = voxMaterialManager.GetMaterial(hit);
        } else {
            newVoxMaterial = VoxMaterial.Asphalt;
        }
        if (newVoxMaterial != VoxMaterial.Null && newVoxMaterial != voxMaterial) {
            voxMaterial = newVoxMaterial;
            footAudioSource.clip = stepClips[(int)voxMaterial];
        }
        footAudioSource.pitch = Random.Range(0.9f, 1.1f);
        footAudioSource.Play();
    }

    public void PlayJumpSound() {
        //footAudioSource.clip = jumpClips[Random.Range(0, jumpClips.Length)];
        footAudioSource.clip = stepClips[(int)voxMaterial];
        footAudioSource.pitch = Random.Range(0.9f, 1.1f);
        footAudioSource.Play();
    }

    public void PlayTalkSound() {
        headAudioSource.Play();
    }

}
