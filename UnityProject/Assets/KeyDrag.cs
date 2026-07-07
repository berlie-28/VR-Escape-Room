using UnityEngine;

public class KeyDrag : MonoBehaviour
{
    // kamerayı elle bağlıyoruz
    public Camera testCamera;

    private Rigidbody rb;

    // anahtar elimizde mi değil mi
    private bool isHolding = false;

    // tutarken kameraya olan derinlik mesafesi
    private float holdDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (!isHolding)
        {
            // anahtarı al: fiziği durdur, mesafeyi kaydet
            isHolding = true;
            rb.isKinematic = true;
            holdDistance = testCamera.WorldToScreenPoint(transform.position).z;
        }
        else
        {
            // anahtarı bırak: fiziği geri aç
            isHolding = false;
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        // elimizde anahtar varsa her frame fareyi takip etsin
        if (!isHolding) return;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, holdDistance);
        transform.position = testCamera.ScreenToWorldPoint(mousePos);
    }
}