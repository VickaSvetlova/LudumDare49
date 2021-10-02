using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Character character { get; private set; }

    [Header("Camera Settings")]
    [SerializeField] private float sensivity = 1.9f;
    [SerializeField] private float zoom = 10f;
    [SerializeField] private float zoomMin = 0f;
    [SerializeField] private float zoomMax = 20f;
    [SerializeField] private float zoomSens = 10f;
    [SerializeField] private Vector3 zoomDirection = new Vector3(0, .1f, -1f);
    [SerializeField] private bool allowFPV = true;
    [SerializeField] private float offsetHorizontal;

    [SerializeField] private LayerMask collisionMask;

    Transform cameraTransformZ;
    Transform cameraTransformV;
    Transform cameraTransformH;

    float cameraEulerV = 25f;
    float cameraEulerH;

    public bool isFPV { get; private set; } = false;
    public bool isTempFPV { get; private set; } = false;


    void Awake() {
        cameraTransformZ = Camera.main.transform;
        cameraTransformV = cameraTransformZ.parent;
        cameraTransformH = cameraTransformV.parent;
        character = GetComponent<Character>();
    }


    private void Update() {
        CameraControl();
        CharacterControl();
    }

    void LateUpdate() {
        CameraZoom();
        CameraMove();
    }

    void CameraControl() {
        cameraEulerH += Input.GetAxis("Mouse X") * sensivity;
        cameraEulerH = Mathf.Repeat(cameraEulerH, 360f);
        cameraEulerV -= Input.GetAxis("Mouse Y") * sensivity;
        cameraEulerV = Mathf.Clamp(cameraEulerV, -80f, 89f);
        cameraTransformV.localEulerAngles = new Vector3(cameraEulerV, 0, 0);
        cameraTransformH.eulerAngles = new Vector3(0, cameraEulerH, 0);
    }

    void CameraZoom() {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0) {
            if (allowFPV && zoom == zoomMin && mouseWheel < 0) SetFPV(false);
            zoom -= zoomSens * mouseWheel;
            zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            if (allowFPV && zoom == zoomMin) SetFPV(true);
        }
    }


    void SetFPV(bool value) {
        isFPV = value;
        _SetFPV(isFPV);
    }

    void SetTempFPV(bool value) {
        isTempFPV = value;
        _SetFPV(isTempFPV);
    }

    void _SetFPV(bool value) {
        if (value) {
            character.model.SetRenderType(RenderType.Shadow);
            cameraTransformZ.localPosition = Vector3.zero;
        } else {
            character.model.SetRenderType(RenderType.ModelShadow);
        }
    }

    void CameraMove() {
        if (isFPV) {
            cameraTransformH.position = character.model.headPoint.position;
        } else {
            float tempZoom = zoom;
            RaycastHit hit;
            var castDir = cameraTransformV.TransformDirection(zoomDirection).normalized;
            if (Physics.Raycast(cameraTransformV.position, castDir, out hit, zoom, collisionMask)) {
                tempZoom = hit.distance - 0.5f;
            }
            if (allowFPV) {
                if (isTempFPV) {
                    if (tempZoom > zoomMin) {
                        SetTempFPV(false);
                    }
                } else if (tempZoom <= zoomMin) {
                    SetTempFPV(true);
                }
            }
            if (isTempFPV) {
                cameraTransformH.position = character.model.headPoint.position;
            } else {
                cameraTransformZ.localPosition = zoomDirection * tempZoom;
            }
            cameraTransformH.position = character.model.headPoint.position + cameraTransformH.right * offsetHorizontal;
        }
    }


    private void CharacterControl() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(horizontal, vertical);
        input = Vector2.ClampMagnitude(input, 1f);
        Vector3 moveFactor = cameraTransformH.forward * input.y + cameraTransformH.right * input.x;
        character.movement.inputMoveDirection = moveFactor;
        if (Input.GetButtonDown("Jump")) character.movement.Jump();
    }

}
