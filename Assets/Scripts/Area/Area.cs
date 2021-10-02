using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    [Header("Sound")]
    [SerializeField] private AmbientSoundSystem ambientSound;
    [SerializeField] private AudioClip ambientClip;
    [SerializeField] private float ambientVolume;


    [Header("Light")]
    [SerializeField] private EnvironmentLightSystem environmentLight;
    [SerializeField][ColorUsage(true,true)] public Color environmentColor;
    [SerializeField] public float environmentColorIntensity = 1f;
    [SerializeField] public float environmentReflectionIntensity = 1f;
    [SerializeField] [ColorUsage(true, true)] public Color sunColor;
    [SerializeField] public float sunColorIntensity = 1f;
    [SerializeField] public bool flashlightEnabled;

    public static Area current;

    private void OnEnter() {
    }

    private void OnExit() {
    }

    public void Enter() {
        if (current == this) return;
        if (current) current.OnExit();
        ambientSound.PlaySmooth(ambientClip, ambientVolume);
        environmentLight.Set(environmentColor, environmentColorIntensity, environmentReflectionIntensity,
            sunColor, sunColorIntensity, flashlightEnabled);
        OnEnter();
        
    }

    public void Exit() {
        if (current != this) return;
        ambientSound.PlaySmooth(null, 0);
        environmentLight.SetDefault();
    }

    public static void Change(Area area) {
        if (area) area.Enter();
        else current.Exit();
    }

}
