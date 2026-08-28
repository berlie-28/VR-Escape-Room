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
        // WASD yürüme kısmı
        Vector3 moveDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) moveDirection += transform.forward;
        if (Keyboard.current.sKey.isPressed) moveDirection -= transform.forward;
        if (Keyboard.current.aKey.isPressed) moveDirection -= transform.right;
        if (Keyboard.current.dKey.isPressed) moveDirection += transform.right;

        moveDirection.y = 0;
        moveDirection = moveDirection.normalized * moveSpeed;

        // yere basılıyken düşme hızını sıfırla, havadaysa yerçekimi uygula
        if (controller.isGrounded)
            verticalVelocity = -1f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        moveDirection.y = verticalVelocity;

        // duvarlara çarpmayı hallediyor
        controller.Move(moveDirection * Time.deltaTime);

        // Sağ tık basılıyken mouse hareketine göre etrafa bakma
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            rotY += mouseDelta.x * mouseSensitivity;
            rotX -= mouseDelta.y * mouseSensitivity;

            rotX = Mathf.Clamp(rotX, -85f, 85f);

            transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
        }

        // tıklama bir UI elemanının üzerindeyse 3D dünyaya ışın gönderme
        if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            // Tıkladığım mouse koordinatından ileriye doğru ışın gönderiyorum
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Lazer şuna çarptı: " + hit.transform.name);
                // Çarptığım şey şifre butonu mu?
                KeypadButton button = hit.transform.GetComponent<KeypadButton>();
                if (button != null)
                {
                    button.PressButton();
                }
            }
        }
    }
}