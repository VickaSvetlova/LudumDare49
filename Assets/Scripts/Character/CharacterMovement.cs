using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Character character { get; private set; }

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 10f;

    [SerializeField] private float gravityForce = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallinDistance = 3f;
    [SerializeField] private float stepOffset = 0.3f;
    public bool isGrounded {
        get { return controller.stepOffset != 0; }
        private set { controller.stepOffset = (value) ? stepOffset : 0; }
    }

    public CharacterController controller { get; private set; }

    [HideInInspector] public Vector3 inputMoveDirection;
    private float gravity = -2f;
    private float fallinStartY = 0;

    private void Awake() {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if (!character.isHidden && character.isActive) {
            Grounding();
            if (inputMoveDirection != Vector3.zero) {
                controller.Move(inputMoveDirection * moveSpeed * Time.deltaTime);
                character.transform.forward = Vector3.RotateTowards(character.transform.forward, inputMoveDirection, turnSpeed * Time.deltaTime, 1f);
            }
        }
        controller.Move(Vector3.up * gravity * Time.deltaTime);
        character.model.animator.SetFloat("Speed", inputMoveDirection.magnitude);
    }

    public void Jump() {
        if (isGrounded) {
            isGrounded = false;
            gravity = jumpForce;
            fallinStartY = transform.position.y;
            controller.Move(Vector3.up * gravity * Time.deltaTime);
            character.model.animator.Play("Jump");
            character.model.animator.SetBool("IsGrounded", isGrounded);
        }
    }

    private void Grounding() {
        if (controller.isGrounded) {
            if (!isGrounded) {
                isGrounded = true;
                gravity = gravityForce;
                if (fallinStartY - transform.position.y > fallinDistance) character.model.animator.SetTrigger("Roll");
            }
        } else {
            if (isGrounded) {
                isGrounded = false;
                gravity = 0;
                fallinStartY = transform.position.y;
            }
            if (gravity > gravityForce) gravity += gravityForce * Time.deltaTime;
        }
        character.model.animator.SetBool("IsGrounded", isGrounded);
    }
}
