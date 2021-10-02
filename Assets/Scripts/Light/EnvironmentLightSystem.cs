using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLightSystem : MonoBehaviour {

    public static EnvironmentLightSystem main { get; private set; }

    [SerializeField] private Light sunLight;
    [SerializeField] private Light flashlight;
    [SerializeField] private float fadeSpeed;

    private Color environmentColor;
    private float environmentIntensity;
    private float reflectionIntensity;
    private Color sunColor;
    private float sunIntensity;
    private float factor = 1f;

    private void Awake() {
        main = this;
    }


    public void Set(Color environmentColor, float environmentIntensity, float reflectionIntensity, 
    Color sunColor, float sunIntensity, bool flashlight = false) {
        this.environmentColor = environmentColor;
        this.environmentIntensity = environmentIntensity;
        this.reflectionIntensity = reflectionIntensity;
        this.sunColor = sunColor;
        this.sunIntensity = sunIntensity;
        StopAllCoroutines();
        StartCoroutine(IEFade(flashlight));
    }

    public void SetDefault() {
        Set(Color.white, 1f, 1f, Color.white, 1f, false);
    }

    public void EnableFlashlight(bool value) {
        flashlight.enabled = value;
    }



    IEnumerator IEFade(bool flashlight) {
        Color startEnvColor = RenderSettings.ambientSkyColor;
        float startIntasity = RenderSettings.ambientIntensity;
        float startReflIntasity = RenderSettings.reflectionIntensity;
        Color startSunColor = sunLight.color;
        float startSunIntensity = sunLight.intensity;
        factor = 0;
        while (factor < 1f) {
            yield return new WaitForSecondsRealtime(0.02f);
            factor += fadeSpeed;
            RenderSettings.ambientSkyColor = Color.Lerp(startEnvColor, environmentColor, factor);
            RenderSettings.ambientIntensity = Mathf.Lerp(startIntasity, environmentIntensity, factor);
            RenderSettings.reflectionIntensity = Mathf.Lerp(startReflIntasity, reflectionIntensity, factor);
            sunLight.color = Color.Lerp(startSunColor, sunColor, factor);
            sunLight.intensity = Mathf.Lerp(startSunIntensity, sunIntensity, factor);
        }
        factor = 1f;
        EnableFlashlight(flashlight);
    }

}
