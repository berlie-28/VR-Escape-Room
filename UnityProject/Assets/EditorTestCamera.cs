using UnityEngine;
using UnityEngine.InputSystem; // Yeni nesil girdi kütüphanemiz

public class EditorTestCamera : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 0.15f;
    
    private float rotX;
    private float rotY;

    void Start()
    {
        // Kameranın sahnedeki mevcut bakış açısını hafızaya alıyoruz
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        rotX = currentRotation.x;
        rotY = currentRotation.y;
    }

    void Update()
    {
        // 1. WASD ile Odada Yürüme Mekaniği
        Vector3 moveDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) moveDirection += transform.forward;
        if (Keyboard.current.sKey.isPressed) moveDirection -= transform.forward;
        if (Keyboard.current.aKey.isPressed) moveDirection -= transform.right;
        if (Keyboard.current.dKey.isPressed) moveDirection += transform.right;

        // Yürürken havaya uçmayı engellemek için dikey ekseni sıfırlıyoruz
        moveDirection.y = 0; 
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        // 2. Fare Sağ Tık Basılıyken Etrafa Bakma Mekaniği
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            rotY += mouseDelta.x * mouseSensitivity;
            rotX -= mouseDelta.y * mouseSensitivity;
            
            // Kafanın tamamen geriye takla atmasını engelleme (Sınırlandırma)
            rotX = Mathf.Clamp(rotX, -85f, 85f);

            transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
        }

        // 3. Fare Sol Tık ile Objeye Tıklayıp Kodu Çalıştırma (VR Eli Simülasyonu)
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Ekranda tıkladığımız noktadan ileriye doğru görünmez bir lazer (Ray) fırlatıyoruz
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Lazerin çarptığı objede ColorChanger kodu var mı diye bakıyoruz
                ColorChanger changer = hit.transform.GetComponent<ColorChanger>();
                if (changer != null)
                {
                    // Varsa renk değiştirme fonksiyonunu doğrudan tetikliyoruz
                    changer.ChangeToRandomColor();
                }
            }
        }
    }
}