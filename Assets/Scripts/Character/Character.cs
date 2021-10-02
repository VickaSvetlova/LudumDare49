using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public CharacterMovement movement { get; private set; }
    public CharacterModel model { get; private set; }

    public bool isActive { get; set; } = true;
    public bool isHidden { get; private set; }

    private void Awake() {
        movement = GetComponent<CharacterMovement>();
        model = GetComponentInChildren<CharacterModel>();
    }

    public void SetHidden(bool value) {
        isHidden = value;
    }

}
