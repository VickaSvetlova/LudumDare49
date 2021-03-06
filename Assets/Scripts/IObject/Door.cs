using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : CrashObject {

    private bool isOpen;
    public bool isLocked;
    public bool isActive;
    [SerializeField] private Transform rotator;
    private float targetAngle;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private bool nextScene;

    public override bool CanUse(Character character) {
        return isActive;
    }

    public override string GetName() {
        if (isLocked && destructible) {
            if (InventorySystem.main.HasItem(ItemType.Key)) return "Open " + name + " by key";
            return base.GetName();
        } else if (destructible) {
            return base.GetName();
        } else if (isLocked) {
            if (InventorySystem.main.HasItem(ItemType.Key)) return "Open " + name + " by key";
            return "Need a key";
        }
        return "Open " + name;
    }

    public override void Use(Character character) {
        if (isLocked && destructible) {
            if (InventorySystem.main.HasItem(ItemType.Key)) {
                Open(character);
                InventorySystem.main.RemoveItem(ItemType.Key);
                return;
            }
            Destr(character);
        } else if (destructible) {
            Destr(character);
        } else if (isLocked) {
            if (InventorySystem.main.HasItem(ItemType.Key)) {
                Open(character);
                InventorySystem.main.RemoveItem(ItemType.Key);
            }
        } else {
            Open(character);
        }
    }

    private void Destr(Character character) {
        base.Use(character);
        if (nextScene) {
            GameManager.main.LoadNextScene();
        } else if (teleportPoint) {
            character.movement.controller.enabled = false;
            character.transform.position = teleportPoint.position;
            character.transform.rotation = teleportPoint.rotation;
            character.movement.controller.enabled = true;
        }
    }

    private void Open(Character character) {
        if (nextScene) {
            GameManager.main.LoadNextScene();
        } else if (teleportPoint) {
            character.movement.controller.enabled = false;
            character.transform.position = teleportPoint.position;
            character.transform.rotation = teleportPoint.rotation;
            character.movement.controller.enabled = true;
            isLocked = false;
            destructible = false;
        } else {
            if (transform.InverseTransformPoint(character.transform.position).z > 0) {
                targetAngle = (isOpen) ? 90f : 180f;
            } else {
                targetAngle = (isOpen) ? 90f : 0f;
            }
            StopAllCoroutines();
            StartCoroutine(IEDoorRotation());
            isOpen = !isOpen;
            isLocked = false;
            destructible = false;
        }
    }

    IEnumerator IEDoorRotation() {
        for (int i = 0; i < 300; i++) {
            yield return new WaitForSeconds(0.01f);
            rotator.localEulerAngles = Vector3.up * Mathf.Lerp(rotator.localEulerAngles.y, targetAngle, i / 300f);
        }
    }

}
