using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EditorTestCamera : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 0.15f;
    
    private float rotX;
    private float rotY;

    void Start()
    {
        // Kameranın açısını açılışta alıp değişkenlere eşitledim
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

        // Karakter havaya uçmasın diye y eksenini sıfırladım
        moveDirection.y = 0; 
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        // Sağ tık basılıyken mouse hareketine göre etrafa bakma
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            rotY += mouseDelta.x * mouseSensitivity;
            rotX -= mouseDelta.y * mouseSensitivity;
            
            // Kamera kendi etrafında takla atmasın diye bakışı sınırlandırma
            rotX = Mathf.Clamp(rotX, -85f, 85f);

            transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
        }

        // tıklama bir UI elemanının (buton vs.) üzerindeyse 3D dünyaya ışın gönderme
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