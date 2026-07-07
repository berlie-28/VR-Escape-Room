using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    // kapının ana gövdesine ihtiyacı var
    public GameObject doorObject;
    public float openHeight = 3.5f;   // kapının yukarı kaç metre kayacağı
    public float openDuration = 1.5f; // kaç saniyede açılacağı

    // kapının durumunu hafızada tutan değişken
    private bool isDoorOpened = false;

    // kasanın açılıp açılmadığını buradan takip ediyorum
    [HideInInspector] public bool isSafeOpened = false;

    // Unity hazır fizik sensör fonksiyonu
    private void OnTriggerEnter(Collider other)
    {
        // hem tag "Key" olacak, hem kapı açılmamış olacak, hem de şifre doğru girilmiş olacak
        if (other.CompareTag("Key") && !isDoorOpened && isSafeOpened)
        {
            isDoorOpened = true;
            // animasyonu başlat
            StartCoroutine(OpenTheDoor());
        }
        // şifreyi girmeden anahtarı kapıya değdirirlerse uyar
        else if (other.CompareTag("Key") && !isSafeOpened)
        {
            Debug.Log("Kasa açılmadan bu kilit/anahtar çalışmaz!");
        }
    }

    private IEnumerator OpenTheDoor()
    {
        // başlangıç ve hedef pozisyonunu kaydet
        Vector3 startPos = doorObject.transform.position;
        Vector3 targetPos = startPos + new Vector3(0, openHeight, 0);

        // ne kadar zaman geçtiğini tutan sayaç
        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            // her frame geçen süreyi ekle
            elapsed += Time.deltaTime;

            // 0'dan 1'e giden ilerleme değeri
            float t = elapsed / openDuration;

            // kapı önce hızlı açılır, sona doğru yavaşlar
            t = t * t * (3f - 2f * t);

            // başlangıç ile hedef arasında o anki pozisyonu hesaplıyor
            doorObject.transform.position = Vector3.Lerp(startPos, targetPos, t);

            // bir sonraki frame'e geç
            yield return null;
        }

        // tam olarak hedefe kilitle
        doorObject.transform.position = targetPos;
        Debug.Log("Sistem Doğrulandı: Kapı Açıldı!");
    }
}