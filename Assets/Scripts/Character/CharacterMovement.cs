using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Character character { get; private set; }


    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float crowlSpeed = 1.2f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float crowlTurnSpeed = 2f;

    [SerializeField] private float height = 1.5f;
    [SerializeField] private float crowlingHeight = 0.5f;

    [SerializeField] private float gravityForce = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallinDistance = 3f;
    [SerializeField] private float stepOffset = 0.3f;




    [Header("Grounding")]
    [HideInInspector] public VoxMaterial groundMaterial;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 slopeDirection = Vector3.up;
    [SerializeField] private float slopeAngle;
    public bool isGrounded {
        get { return controller.stepOffset != 0; }
        private set { controller.stepOffset = (value) ? stepOffset : 0; }
    }
    public bool isCrowling { get; private set; }





    public CharacterController controller { get; private set; }

    [HideInInspector] public Vector3 inputMoveDirection;
    private float gravity = -2f;
    private float fallinStartY = 0;
    private float fallTimer;

    private void Awake() {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if (UIController.main.isMouseControlled) return; 
        if (!character.isHidden && character.isActive) {
            Grounding();
            var speed = (isCrowling) ? crowlSpeed : moveSpeed;
            if (inputMoveDirection != Vector3.zero) {
                controller.Move(inputMoveDirection * speed * Time.deltaTime);
                var turnSpeed = (isCrowling) ? crowlTurnSpeed : this.turnSpeed;
                character.transform.forward = Vector3.RotateTowards(character.transform.forward, inputMoveDirection, turnSpeed * Time.deltaTime, 1f);
            }
        }
        if (slopeAngle > controller.slopeLimit) {
            controller.Move(slopeDirection * (slopeAngle * 0.1f) * Time.deltaTime);
        }
        controller.Move(Vector3.up * gravity * Time.deltaTime);
        character.model.animator.SetFloat("Speed", inputMoveDirection.magnitude);
    }

    public void Jump() {
        if (isGrounded && slopeAngle < controller.slopeLimit) {
            if (isCrowling) {
                ChangeCrowl(false);
            } else {
                isGrounded = false;
                gravity = jumpForce;
                fallinStartY = transform.position.y;
                controller.Move(Vector3.up * gravity * Time.deltaTime);
                character.model.animator.Play("Jump");
                character.model.animator.SetBool("IsGrounded", isGrounded);
            }
        }
    }

    public void Crowl() {
        if (isGrounded && slopeAngle < controller.slopeLimit) {
            ChangeCrowl(!isCrowling);
        }
    }

    private void ChangeCrowl(bool value, bool force = false) {
        if (value) {
            controller.height = crowlingHeight;
            controller.center = new Vector3(0, 0.83f / 2f, 0);
        } else {
            if (!force) {
                var ray = new Ray(transform.position, Vector3.up);
                if (Physics.SphereCast(ray, controller.radius, 1.5f, layerMask)) {
                    return;
                }
            }
            controller.height = height;
            controller.center = new Vector3(0, 0.83f, 0);
        }
        isCrowling = value;
        character.model.ChangeHeadHeight(isCrowling);
        character.model.animator.SetBool("IsCrowling", isCrowling);
    }

    private void Grounding() {
        if (controller.isGrounded) {
            if (!isGrounded) {
                fallTimer = 0;
                ChangeCrowl(false, true);
                isGrounded = true;
                gravity = gravityForce;
                if (fallinStartY - transform.position.y > fallinDistance) character.model.animator.SetTrigger("Roll");
            }
            RaycastHit hit;
            VoxMaterial newVoxMaterial = VoxMaterial.Null;
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, layerMask)) {
                newVoxMaterial = VoxMaterialManager.main.GetMaterial(hit);
                slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                slopeDirection = Vector3.Cross(hit.normal, Vector3.Cross(hit.normal, Vector3.up));
            } else {
                newVoxMaterial = VoxMaterial.Asphalt;
            }
            if (newVoxMaterial != VoxMaterial.Null && newVoxMaterial != groundMaterial) {
                groundMaterial = newVoxMaterial;
            }
        } else {
            fallTimer += Time.deltaTime;
            if (isGrounded) {
                isGrounded = false;
                gravity = -2f;
                fallinStartY = transform.position.y;
            }
            if (gravity > gravityForce) gravity += gravityForce * Time.deltaTime;
        }
        character.model.animator.SetBool("IsGrounded", (isGrounded && slopeAngle < controller.slopeLimit) || fallTimer < .01f);
    }
}
