using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base FPS movement controller — Unity Input System.
///
/// Hierarchy:
///   Player  (this script + CharacterController + PlayerInput)
///   └── CameraHolder  (empty Transform at eye level)
///       └── Main Camera
///
/// PlayerInput component:
///   - Actions asset with an "Player" action map containing:
///     Move      (Value, Vector2)
///     Look      (Value, Vector2)
///     Jump      (Button)
///     Sprint    (Button)
///     Escape    (Button)   ← optional, for cursor unlock
///   - Behavior: Send Messages  (or Invoke Unity Events — see OnMove etc.)
/// </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovementBase : MonoBehaviour
{
    // ---------------------------------------------------------------
    //  Inspector
    // ---------------------------------------------------------------

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 15f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.4f;
    public float gravity = -20f;
    public float groundedGravity = -2f;

    [Header("Look")]
    public float mouseSensitivity = 0.15f;
    public float verticalClamp = 88f;

    [Header("References")]
    public Transform cameraHolder;

    // ---------------------------------------------------------------
    //  Protected state
    // ---------------------------------------------------------------

    protected CharacterController cc;
    protected Vector3 velocity;
    protected Vector3 horizontalVelocity;
    protected bool isGrounded;
    protected bool isSprinting;

    // ---------------------------------------------------------------
    //  Private — raw input values from callbacks
    // ---------------------------------------------------------------

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpPressed;
    private bool sprintHeld;
    private float verticalLookAngle;
    private bool cursorLocked = true;

    // ---------------------------------------------------------------
    //  Unity lifecycle
    // ---------------------------------------------------------------

    protected virtual void Awake()
    {
        cc = GetComponent<CharacterController>();
        LockCursor(true);
    }

    protected virtual void Update()
    {
        HandleLook();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        ApplyVelocity();

        jumpPressed = false; // consume once per frame
    }

    // ---------------------------------------------------------------
    //  Input System callbacks (Send Messages / Invoke Unity Events)
    //  PlayerInput calls these automatically by action name.
    // ---------------------------------------------------------------

    public virtual void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public virtual void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public virtual void OnJump(InputValue value)
    {
        if (value.isPressed) jumpPressed = true;
    }

    public virtual void OnSprint(InputValue value)
    {
        sprintHeld = value.isPressed;
    }

    // Escape — unlock cursor
    public virtual void OnEscape(InputValue value)
    {
        if (value.isPressed) LockCursor(false);
    }

    // Click to re-lock (bind to LeftClick action if needed,
    // or handle via PlayerInput "Click" action)
    public virtual void OnClick(InputValue value)
    {
        if (value.isPressed) LockCursor(true);
    }

    // ---------------------------------------------------------------
    //  Look
    // ---------------------------------------------------------------

    protected virtual void HandleLook()
    {
        if (!cursorLocked) return;

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookAngle -= mouseY;
        verticalLookAngle = Mathf.Clamp(verticalLookAngle, -verticalClamp, verticalClamp);

        if (cameraHolder != null)
            cameraHolder.localRotation = Quaternion.Euler(verticalLookAngle, 0f, 0f);
    }

    // ---------------------------------------------------------------
    //  Movement
    // ---------------------------------------------------------------

    protected virtual void HandleMovement()
    {
        isGrounded = cc.isGrounded;
        isSprinting = isGrounded && sprintHeld;

        float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 inputDir = transform.right * moveInput.x
                         + transform.forward * moveInput.y;

        if (inputDir.magnitude > 1f) inputDir.Normalize();

        Vector3 targetVelocity = inputDir * targetSpeed;

        float rate = inputDir.magnitude > 0.01f ? acceleration : deceleration;
        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity, targetVelocity, rate * Time.deltaTime);
    }

    // ---------------------------------------------------------------
    //  Jump
    // ---------------------------------------------------------------

    protected virtual void HandleJump()
    {
        if (isGrounded && jumpPressed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            OnJumped();
        }
    }

    /// <summary>Called once when the player jumps. Override in child classes.</summary>
    protected virtual void OnJumped() { }

    // ---------------------------------------------------------------
    //  Gravity
    // ---------------------------------------------------------------

    protected virtual void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = groundedGravity;
        else
            velocity.y += gravity * Time.deltaTime;
    }

    // ---------------------------------------------------------------
    //  Final move
    // ---------------------------------------------------------------

    protected virtual void ApplyVelocity()
    {
        cc.Move((horizontalVelocity + Vector3.up * velocity.y) * Time.deltaTime);
    }

    // ---------------------------------------------------------------
    //  Utility
    // ---------------------------------------------------------------

    protected void LockCursor(bool locked)
    {
        cursorLocked = locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}