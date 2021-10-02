using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject {

    private bool isOpen;
    public bool isLocked;

    [SerializeField] private Transform rotator;

    public override void Use(Character character) {
        base.Use(character);
        StopAllCoroutines();
        StartCoroutine(IEDoorRotation());
        isOpen = !isOpen;
    }

    IEnumerator IEDoorRotation() {
        var targetVector = (isOpen) ? Vector3.right : Vector3.forward;
        for (int i = 0; i < 300; i++) {
            yield return new WaitForSeconds(0.01f);
            rotator.forward = Vector3.Lerp(rotator.forward, targetVector, i / 300f);
        }
    }

}
