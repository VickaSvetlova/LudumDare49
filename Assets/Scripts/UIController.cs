using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController main { get; private set; }

    [SerializeField] private Text interactLabel;
    [SerializeField] private GameObject interactPanel;

    [SerializeField] private GameSettings gameSettings;

    [Header("Inventory")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text[] itemLables;

    public bool isMouseControlled;

    private void Awake() {
        main = this;
    }

    private void Start() {
        ShowMenu(false);
        ShowInventory(false);
    }

    public void InteractShow(string text) {
        interactLabel.text = "[E] " + text;
        interactLabel.gameObject.SetActive(true);
    }

    public void InteractHide() {
        interactLabel.gameObject.SetActive(false);
    }


    public void SetMouseControl(bool value) {
        isMouseControlled = value;
        if (isMouseControlled) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ShowMenu(bool value) {
        interactPanel.gameObject.SetActive(!value);
        gameSettings.gameObject.SetActive(value);
        SetMouseControl(value);
        Time.timeScale = (value) ? 0f : 1f;
    }

    public void ShowInventory(bool value) {
        inventoryPanel.gameObject.SetActive(value);
        SetMouseControl(value);
    }

    public void UpdateItemCounts(List<int> items) {
        for (int i = 0; i < itemLables.Length; i++) {
            itemLables[i].text = "x" + items[i].ToString();
        }
    }

}
