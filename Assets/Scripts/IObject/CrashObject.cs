using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashObject : InteractiveObject {

    private Rigidbody[] rigidBodies;

    public ItemType requiredItem;

    protected override void Awake() {
        base.Awake();
        rigidBodies = GetComponentsInChildren<Rigidbody>();
    }


    public void Crash(Vector3 explosivePos) {
        foreach (var rigidBody in rigidBodies) {
            rigidBody.GetComponent<MeshCollider>().convex = true;
            rigidBody.isKinematic = false;
            rigidBody.AddExplosionForce(100f, explosivePos, 20f);
        }
    }

    [ContextMenu("CrashTest")]
    private void CrashTest() {
        Crash(transform.position + transform.forward);
    }


    public override void Use(Character character) {
        base.Use(character);
        if (character.inventory.HasItem(requiredItem)) {
            Crash(character.model.headPoint.position);
            character.inventory.RemoveItem(requiredItem);
        }
    }

}
