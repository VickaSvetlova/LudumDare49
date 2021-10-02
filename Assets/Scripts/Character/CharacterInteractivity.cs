using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractivity : MonoBehaviour
{

    [HideInInspector] public Character character;

    [SerializeField] private UIController uiController;

    [SerializeField] private LayerMask layerMask;
    
    [HideInInspector] public InteractiveObject activeObject;


    // Start is called before the first frame update
    void Awake() {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)) {
            if (Physics.Raycast(character.model.headPoint.position, hit.point - character.model.headPoint.position, out hit, 1.5f, layerMask)) {
                if (hit.collider.tag == TagList.InteractiveObject) {
                    var iobject = hit.collider.GetComponentInParent<InteractiveObject>();
                    if (iobject != activeObject)
                    {
                        activeObject = iobject;
                        if (uiController is { }) uiController.InteractShow(iobject.name);
                    }
                    return;
                }
            }
        }

        if (uiController is { }) uiController.InteractHide();
        activeObject = null;
    }

    private void Update() {
        if (Input.GetButtonDown("Use") && activeObject) {
            activeObject.Use(character);
        }
    }

}
