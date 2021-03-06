using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public CharacterMovement movement { get; private set; }
    public CharacterModel model { get; private set; }
    public CharacterInteractivity interactivity { get; private set; }
    public CharacterAudio audio { get; private set; }

    public bool isActive { get; set; } = true;
    public bool isHidden { get; private set; }

    private void Awake() {
        movement = GetComponent<CharacterMovement>();
        model = GetComponentInChildren<CharacterModel>();
        interactivity = GetComponent<CharacterInteractivity>();
        audio = model.GetComponent<CharacterAudio>();
    }

    public void SetHidden(bool value) {
        isHidden = value;
    }

    [ContextMenu("Die")]
    public void Die() {
        PlayerController.main.isActive = false;
        model.animator.Play("Death");
    }

    private void OnDestroy() {
        GameManager.main.Restart();
    }

}
