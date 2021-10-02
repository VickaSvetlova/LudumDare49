using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController main { get; private set; }

    [SerializeField] private Text interactLabel;

    public bool isMouseControlled;

    private void Awake() {
        main = this;
        SetMouseControl(false);
    }

    public void InteractShow(string text) {
        interactLabel.text = text;
        interactLabel.gameObject.SetActive(true);
    }

    public void InteractHide() {
        interactLabel.gameObject.SetActive(false);
    }


    private void Update() {
        if (Input.GetButtonDown("Cancel")) SetMouseControl(!isMouseControlled);
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

}
