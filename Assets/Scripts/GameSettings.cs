using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SettingsData {

    public float mouseSensivity;
    public float zoom;

}

public class GameSettings : MonoBehaviour {

    public static GameSettings main { get; private set; }

    [SerializeField] private Slider mouseSensSlider;



    private void Awake() {
        main = this;
    }

    private void Start() {
        if (InventorySystem.main.gameSettings == null) InventorySystem.main.gameSettings = Load();
    }

    private void OnEnable() {
        mouseSensSlider.value = InventorySystem.main.gameSettings.mouseSensivity;
    }

    public void Save() {
        //string json = JsonUtility.ToJson(data);
        //PlayerPrefs.SetString("Settings", json);
        //PlayerPrefs.Save();
        InventorySystem.main.gameSettings.mouseSensivity = mouseSensSlider.value;
        UIController.main.ShowMenu(false);
    }

    public SettingsData Load() {
        //string encrypt = PlayerPrefs.GetString("Settings");
        //if (!string.IsNullOrEmpty(encrypt)) {
        //    data = JsonUtility.FromJson<SettingsData>(encrypt);
        //}
        var settings = new SettingsData();
        settings.mouseSensivity = 1.9f;
        settings.zoom = 2f;
        return settings;
    }

    public void Default() {
        var settings = Load();
        mouseSensSlider.value = settings.mouseSensivity;
    }


}
