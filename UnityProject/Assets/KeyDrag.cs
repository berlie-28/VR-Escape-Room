using UnityEngine;

public class KeyDrag : MonoBehaviour
{
    // kamerayı elle bağlıyoruz
    public Camera testCamera;

    private Rigidbody rb;

    public AudioSource audioSource;
    public AudioClip pickupSound;

    // anahtar elimizde mi değil mi
    private bool isHolding = false;

    // tutarken kameraya olan derinlik mesafesi
    private float holdDistance;

    // aldığımız frame'de bırakma sorununu önlemek için
    private bool justPickedUp = false;

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
            if (audioSource != null && pickupSound != null)
                audioSource.PlayOneShot(pickupSound);
            rb.isKinematic = true;
            holdDistance = Mathf.Min(Vector3.Distance(testCamera.transform.position, transform.position), 1.5f);

            // bu frame'de bırakmayı engelle
            justPickedUp = true;
        }
    }

    void Update()
    {
        if (!isHolding) return;

        // aldığımız frame'de bırakma kontrolünü atla
        if (justPickedUp)
        {
            justPickedUp = false;
            return;
        }

        // sol tıkla bırak
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = false;
            rb.isKinematic = false;
            return;
        }

        Ray ray = testCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 newPos = ray.GetPoint(holdDistance);

        // anahtarı sağa ve aşağıya kaydır, daha doğal görünsün
        newPos += testCamera.transform.right * 0.1f;
        newPos -= testCamera.transform.up * 0.2f;

        transform.position = newPos;
    }
}