using UnityEngine;

public class CrashObject : InteractiveObject
{
    [SerializeField] private AudioClip clip;
    
    private BoxCollider[] NonCrachebalModel;
    private LightProbs[] lightProbs;

    public ItemType requiredItem;

    public bool destructible;
    private AudioSource audioSource;

    protected override void Awake()
    {
        lightProbs = GetComponentsInChildren<LightProbs>(true);
        if (lightProbs.Length > 0) ShowLightProbs(destructible);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && clip != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        base.Awake();
        NonCrachebalModel = GetComponentsInChildren<BoxCollider>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>(true);

        foreach (var rb in rigidBodies) {
            rb.gameObject.SetActive(false);
        }
    }

    public void SetDestructible(bool value) {
        destructible = value;
        ShowLightProbs(destructible);
    }

    private void ShowLightProbs(bool value) {
        foreach (var lp in lightProbs) {
            lp.gameObject.SetActive(value);
        }
    }

    public void Crash()
    {
        Crash(transform.position);
    }

    public void Crash(Vector3 explosivePos)
    {
        MeshCollider firstCollider = null;
        var rigidBodies = GetComponentsInChildren<Rigidbody>(true);
        foreach (var rigidBody in rigidBodies)
        {
            var rigidCollider = rigidBody.GetComponent<MeshCollider>();
            if (firstCollider == null) {
                firstCollider = rigidCollider;
            } else {
                Physics.IgnoreCollision(firstCollider, rigidCollider, true);
            }
            foreach (var collider in NonCrachebalModel)
            {
                collider.gameObject.SetActive(false);
            }

            rigidBody.gameObject.SetActive(true);
            rigidBody.isKinematic = false;
            rigidBody.AddExplosionForce(150f, explosivePos, 50f);
        }

        if (lightProbs.Length > 0) ShowLightProbs(false);

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
        if (!InventorySystem.main.HasItem(requiredItem)) {
            if (requiredItem == ItemType.Axe) return "Need an axe";
            else if (requiredItem == ItemType.Hammer) return "Need a hammer";
            else if (requiredItem == ItemType.Key) return "Need a key";
            else if (requiredItem == ItemType.Soda) return "Need a soda";
        }
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
            requiredItem = ItemType.Null;
        }
    }
}