using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SettingsData {

    public float mouseSensivity;

}

public class GameSettings : MonoBehaviour {

    public static GameSettings main { get; private set; }
    public static SettingsData data = new SettingsData();

    [SerializeField] private Slider mouseSensSlider;



    private void Awake() {
        main = this;
    }

    private void Start() {
        data = Load();
    }

    private void OnEnable() {
        mouseSensSlider.value = data.mouseSensivity;
    }

    public void Save() {
        //string json = JsonUtility.ToJson(data);
        //PlayerPrefs.SetString("Settings", json);
        //PlayerPrefs.Save();
        data.mouseSensivity = mouseSensSlider.value;
        UIController.main.ShowMenu(false);
    }

    public SettingsData Load() {
        //string encrypt = PlayerPrefs.GetString("Settings");
        //if (!string.IsNullOrEmpty(encrypt)) {
        //    data = JsonUtility.FromJson<SettingsData>(encrypt);
        //}
        var settings = new SettingsData();
        settings.mouseSensivity = 1.9f;
        return settings;
    }

    public void Default() {
        var settings = Load();
        mouseSensSlider.value = settings.mouseSensivity;
    }


}
