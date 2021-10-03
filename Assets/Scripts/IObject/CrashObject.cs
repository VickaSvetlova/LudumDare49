using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashObject : InteractiveObject
{
    [SerializeField] private AudioClip clip;

    private Rigidbody[] rigidBodies;

    public ItemType requiredItem;

    public bool destructible;
    private AudioSource audioSource;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && clip != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        base.Awake();
        rigidBodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Crash()
    {
        Crash(transform.position);
    }

    public void Crash(Vector3 explosivePos)
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.GetComponent<MeshCollider>().convex = true;
            rigidBody.isKinematic = false;
            rigidBody.AddExplosionForce(150f, explosivePos, 20f);
        }

        if (clip != null && audioSource != null)
            PlaySound(clip);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    [ContextMenu("CrashTest")]
    private void CrashTest()
    {
        Crash(transform.position + transform.forward);
    }


    public override void Use(Character character)
    {
        base.Use(character);
        if (destructible && character.inventory.HasItem(requiredItem))
        {
            Crash(character.model.headPoint.position);
            character.inventory.RemoveItem(requiredItem);
        }
    }
}