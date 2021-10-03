using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractiveObject
{
    public int id;

    [SerializeField] private Transform rotator;
    [SerializeField] private float rotationSpeed = 20f;

    private void FixedUpdate() {
        rotator.localRotation *= Quaternion.AngleAxis(rotationSpeed * Time.fixedDeltaTime, Vector3.up);
    }

    public override void Use(Character character) {
        base.Use(character);
        character.inventory.AddItem(this);
    }

}
