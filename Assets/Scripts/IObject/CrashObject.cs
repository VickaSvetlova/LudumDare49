using UnityEngine;

public class CrashObject : InteractiveObject
{
    [SerializeField] private AudioClip clip;

    private Rigidbody[] rigidBodies;
    private BoxCollider[] NonCrachebalModel;

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
        NonCrachebalModel = GetComponentsInChildren<BoxCollider>();
        rigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rigidBodies)
        {
            rb.gameObject.SetActive(false);
        }
    }

    public void Crash()
    {
        Crash(transform.position);
    }

    public void Crash(Vector3 explosivePos)
    {
        foreach (var rigidBody in rigidBodies)
        {
            foreach (var collider in NonCrachebalModel)
            {
                collider.gameObject.SetActive(false);
            }

            rigidBody.gameObject.SetActive(true);
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


    public override string GetName() {
        return "Destroy " + base.GetName();
    }

    public override bool CanUse(Character character) {
        return destructible;
    }

    public override void Use(Character character)
    {
        base.Use(character);
        if (destructible && InventorySystem.main.HasItem(requiredItem))
        {
            Crash(character.model.headPoint.position);
            InventorySystem.main.RemoveItem(requiredItem);
        }
    }
}