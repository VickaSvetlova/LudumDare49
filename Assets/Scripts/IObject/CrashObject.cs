using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashObject : InteractiveObject {

    private Rigidbody[] rigidBodies;


    // Start is called before the first frame update
    void Awake() {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
    }


    public void Crash(Vector3 explosivePos) {
        foreach (var rigidBody in rigidBodies) {
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
        Crash(character.model.torsoPoint.position);
    }

}
