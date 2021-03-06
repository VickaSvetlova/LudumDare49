using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractivity : MonoBehaviour
{

    [HideInInspector] public Character character;

    [SerializeField] private LayerMask layerMask;
    
    [HideInInspector] public InteractiveObject activeObject;

    [SerializeField] private float distance = 2f;


    // Start is called before the first frame update
    void Awake() {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)) {
            if (Physics.Raycast(character.model.headPoint.position, hit.point - character.model.headPoint.position, out hit, distance, layerMask)) {
                if (hit.collider.tag == TagList.InteractiveObject) {
                    var iobject = hit.collider.GetComponentInParent<InteractiveObject>();
                    if (iobject != activeObject)
                    {
                        activeObject = iobject;
                        if (iobject.CanUse(character)) UIController.main.InteractShow(iobject.GetName());
                    }
                    return;
                }
            }
        }
        UIController.main.InteractHide();
        activeObject = null;
    }

    private void Update() {
        if (Input.GetButtonDown("Use") && activeObject) {
            if (activeObject.CanUse(character)) activeObject.Use(character);
        }
    }

}
