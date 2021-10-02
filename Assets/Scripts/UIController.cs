using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Text interactLabel;

    public void InteractShow(string text) {
        interactLabel.text = text;
        interactLabel.gameObject.SetActive(true);
    }

    public void InteractHide() {
        interactLabel.gameObject.SetActive(false);
    }

}
