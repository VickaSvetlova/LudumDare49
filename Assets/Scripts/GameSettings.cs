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
        Load();
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

    public void Load() {
        //string encrypt = PlayerPrefs.GetString("Settings");
        //if (!string.IsNullOrEmpty(encrypt)) {
        //    data = JsonUtility.FromJson<SettingsData>(encrypt);
        //}
        data.mouseSensivity = 1.9f;
    }

    public void Default() {
        Load();
        OnEnable();
    }


}
