using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : CrashObject {

    private bool isOpen;
    public bool isLocked;

    [SerializeField] private Transform rotator;
    private float targetAngle;

    public override bool CanUse(Character character) {
        return base.CanUse(character) || !isLocked;
    }

    public override string GetName() {
        return isLocked ? base.GetName() : "Open " + name;
    }

    public override void Use(Character character) {
        if (isLocked) {
            base.Use(character);
        } else {
            if (transform.InverseTransformPoint(character.transform.position).z > 0) {
                targetAngle = (isOpen) ? 90f : 180f;
            } else {
                targetAngle = (isOpen) ? 90f : 0f;
            }
            StopAllCoroutines();
            StartCoroutine(IEDoorRotation());
            isOpen = !isOpen;
        }
    }

    IEnumerator IEDoorRotation() {
        for (int i = 0; i < 300; i++) {
            yield return new WaitForSeconds(0.01f);
            rotator.localEulerAngles = Vector3.up * Mathf.Lerp(rotator.localEulerAngles.y, targetAngle, i / 300f);
        }
    }

}
