using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public CharacterMovement movement { get; private set; }
    public CharacterModel model { get; private set; }
    public CharacterInteractivity interactivity { get; private set; }
    public CharacterInventory inventory { get; private set; }

    public bool isActive { get; set; } = true;
    public bool isHidden { get; private set; }

    private void Awake() {
        movement = GetComponent<CharacterMovement>();
        model = GetComponentInChildren<CharacterModel>();
        inventory = GetComponent<CharacterInventory>();
        interactivity = GetComponent<CharacterInteractivity>();
    }

    public void SetHidden(bool value) {
        isHidden = value;
    }

    public void Die() {

    }

}
