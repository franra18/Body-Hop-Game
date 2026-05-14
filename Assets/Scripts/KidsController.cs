using UnityEngine;
using UnityEngine.InputSystem;

public class KidsController : MonoBehaviour
{
    [Header("Control de Estado")]
    public bool isControllable = true;

    [Header("Input Actions")]
    public InputAction moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick", expectedControlType: "Vector2");
    public InputAction lookAction = new InputAction("Look", binding: "<Pointer>/delta", expectedControlType: "Vector2");
    public InputAction jumpAction = new InputAction("Jump", binding: "<Keyboard>/space", expectedControlType: "Button");
    public InputAction fallAction = new InputAction("Fall", binding: "<Keyboard>/f", expectedControlType: "Button");
    public InputAction damageAction = new InputAction("Damage", binding: "<Keyboard>/q", expectedControlType: "Button");
    public InputAction faintAction = new InputAction("Faint", binding: "<Keyboard>/e", expectedControlType: "Button");
    public InputAction runAction = new InputAction("Run", binding: "<Keyboard>/leftCtrl", expectedControlType: "Button");

    [Header("Parámetros Físicos")]
    public float moveSpeed = 5.0f;
    public float runSpeed = 8.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    [Header("Cámara Primera Persona")]
    public Transform cameraTransform; 
    public float mouseSensitivity = 0.1f; 
    private float xRotation = 0f; 

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDirection = Vector3.zero;

    private void OnEnable()
    {
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.Enable(); lookAction.Enable(); jumpAction.Enable();
        fallAction.Enable(); damageAction.Enable(); faintAction.Enable(); runAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable(); lookAction.Disable(); jumpAction.Disable();
        fallAction.Disable(); damageAction.Disable(); faintAction.Disable(); runAction.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isDown = stateInfo.IsName("down");
        bool isIncapacitated = isDown || stateInfo.IsName("damage") || stateInfo.IsName("faint") || stateInfo.IsName("standup_faint");

        float moveH = 0f;
        float moveV = 0f;
        bool isRunning = false;

        if (isControllable && !isIncapacitated)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            moveH = moveInput.x;
            moveV = moveInput.y;
            if (runAction.IsPressed() && (Mathf.Abs(moveH) > 0.1f || Mathf.Abs(moveV) > 0.1f)) isRunning = true;
        }

        float verticalVelocity = moveDirection.y;
        Vector3 horizontalMove = new Vector3(moveH, 0, moveV);
        horizontalMove = transform.TransformDirection(horizontalMove);
        float currentMoveSpeed = isRunning ? runSpeed : moveSpeed;
        horizontalMove *= currentMoveSpeed;

        moveDirection = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.z);

        if (controller.isGrounded)
        {
            if (moveDirection.y < 0) moveDirection.y = -2f;
            if (isControllable && jumpAction.WasPressedThisFrame() && !isIncapacitated)
            {
                moveDirection.y = jumpForce;
                animator.SetTrigger("JumpTrigger");
            }
        }

        if (controller.enabled && cameraTransform != null)
        {
            if (isDown)
            {
                cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, Quaternion.Euler(70f, 0f, 0f), Time.deltaTime * 5f);
            }
            else if (isControllable)
            {
                Vector2 lookInput = lookAction.ReadValue<Vector2>();
                transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity);
                xRotation -= lookInput.y * mouseSensitivity;
                xRotation = Mathf.Clamp(xRotation, -80f, 80f);
                cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        float currentSpeed = new Vector2(moveH, moveV).magnitude;
        if (isRunning) currentSpeed *= (runSpeed / moveSpeed);
        animator.SetFloat("Speed", currentSpeed);

        if (isControllable)
        {
            if (fallAction.WasPressedThisFrame()) animator.SetTrigger("FallTrigger");
            if (damageAction.WasPressedThisFrame()) animator.SetTrigger("DamageTrigger");
            if (faintAction.WasPressedThisFrame()) animator.SetTrigger("FaintTrigger");
        }
    }
}