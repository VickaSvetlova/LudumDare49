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
        ShowInventory(false);
    }

    private void Start() {
        InteractHide();
        ShowMenu(false);
        if (GameManager.main.sceneIndex == 0) SetMouseControl(true);
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
        if (value) UpdateItemCounts(InventorySystem.main.items);
        inventoryPanel.gameObject.SetActive(value);
    }

    public void UpdateItemCounts(List<int> items) {
        for (int i = 0; i < itemLables.Length; i++) {
            itemLables[i].text = "x" + items[i].ToString();
        }
    }

}
