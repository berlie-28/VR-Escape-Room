using UnityEngine;

public class KeyDrag : MonoBehaviour
{
    // kamerayı elle bağlıyoruz
    public Camera testCamera;

    private Rigidbody rb;

    // anahtarın collider'ı, tutarken trigger'a çevirip oyuncuya çarpmasını engelliyoruz
    private Collider col;

    // anahtar elimizde mi değil mi
    private bool isHolding = false;

    // tutarken kameraya olan derinlik mesafesi
    private float holdDistance;

    // aldığımız frame'de bırakma sorununu önlemek için
    private bool justPickedUp = false;

    // anahtarı alınca çalacak ses
    public AudioSource audioSource;
    public AudioClip pickupSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        if (!isHolding)
        {
            // anahtarı al: fiziği durdur, mesafeyi kaydet
            isHolding = true;
            rb.isKinematic = true;

            if (audioSource != null && pickupSound != null)
                audioSource.PlayOneShot(pickupSound);

            // tutarken collider'ı trigger yap: fiziksel olarak itmesin ama kilide değince hâlâ algılansın
            if (col != null) col.isTrigger = true;

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
            if (col != null) col.isTrigger = false;
            return;
        }

        Ray ray = testCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 newPos = ray.GetPoint(holdDistance);

        // anahtarı sağa ve aşağıya kaydır, daha doğal görünsün
        newPos += testCamera.transform.right * 0.1f;
        newPos -= testCamera.transform.up * 0.2f;

        // aradaki yönde duvar/kapı varsa anahtarı onun önünde durdur (kendi gövdesine çarpmasını yok say)
        Vector3 dir = newPos - testCamera.transform.position;
        float dist = dir.magnitude;
        if (Physics.Raycast(testCamera.transform.position, dir.normalized, out RaycastHit hit, dist, ~0, QueryTriggerInteraction.Ignore)
            && hit.rigidbody != rb)
        {
            newPos = hit.point - dir.normalized * 0.1f;
        }

        transform.position = newPos;
    }
}