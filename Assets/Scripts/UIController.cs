using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController main { get; private set; }

    [SerializeField] private Text interactLabel;

    [SerializeField] private GameSettings gameSettings;

    public bool isMouseControlled;

    private void Awake() {
        main = this;
    }

    private void Start() {
        ShowMenu(false);
    }

    public void InteractShow(string text) {
        interactLabel.text = text;
        interactLabel.gameObject.SetActive(true);
    }

    public void InteractHide() {
        interactLabel.gameObject.SetActive(false);
    }


    public void SetMouseControl(bool value) {
        isMouseControlled = value;
        if (isMouseControlled) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ShowMenu(bool value) {
        gameSettings.gameObject.SetActive(value);
        SetMouseControl(value);
        Time.timeScale = (value) ? 0f : 1f;
    }

}
