using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject {

    private bool isOpen;
    public bool isLocked;

    [SerializeField] private Transform rotator;
    private float targetAngle;

    public override void Use(Character character) {
        base.Use(character);
        if (transform.InverseTransformPoint(character.transform.position).x > 0) {
            targetAngle = (isOpen) ? 0f : 90f;
        } else {
            targetAngle = (isOpen) ? 0f : -90f;
        }
        StopAllCoroutines();
        StartCoroutine(IEDoorRotation());
        isOpen = !isOpen;
    }

    IEnumerator IEDoorRotation() {
        for (int i = 0; i < 300; i++) {
            yield return new WaitForSeconds(0.01f);
            rotator.localEulerAngles = Vector3.up * Mathf.Lerp(rotator.localEulerAngles.y, targetAngle, i / 300f);
        }
    }

}
