using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EditorTestCamera : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 0.15f;
    public float gravity = -9.81f;

    private float rotX;
    private float rotY;
    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        rotX = currentRotation.x;
        rotY = currentRotation.y;
    }

    void Update()
    {
        // WASD movement
        Vector3 moveDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) moveDirection += transform.forward;
        if (Keyboard.current.sKey.isPressed) moveDirection -= transform.forward;
        if (Keyboard.current.aKey.isPressed) moveDirection -= transform.right;
        if (Keyboard.current.dKey.isPressed) moveDirection += transform.right;

        moveDirection.y = 0;
        moveDirection = moveDirection.normalized * moveSpeed;

        // reset fall speed when on the ground, apply gravity when in the air
        if (controller.isGrounded)
            verticalVelocity = -1f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        moveDirection.y = verticalVelocity;

        // CharacterController takes care of wall collisions
        controller.Move(moveDirection * Time.deltaTime);

        // look around with the mouse while holding right click
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            rotY += mouseDelta.x * mouseSensitivity;
            rotX -= mouseDelta.y * mouseSensitivity;

            rotX = Mathf.Clamp(rotX, -85f, 85f);

            transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
        }

        // don't raycast into the 3D world if we clicked on a UI element
        if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            // shoot a ray forward from where the mouse clicked
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Ray hit: " + hit.transform.name);
                // check if we hit a keypad button
                KeypadButton button = hit.transform.GetComponent<KeypadButton>();
                if (button != null)
                {
                    button.PressButton();
                }
            }
        }
    }
}