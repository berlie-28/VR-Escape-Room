using UnityEngine;

public class DoorController : MonoBehaviour
{
    // kapının ana gövdesine ihtiyacı var
    public GameObject doorObject; 
    public float openHeight = 3.5f; // kapının yukarı doğru kaç metre kayacağı
    private bool isDoorOpened = false; // kapının durumunu hafızada tutan değişkeni

    // kasanın açılıp açılmadığını buradan takip ediyorum
    [HideInInspector] public bool isSafeOpened = false; 

    // Unity hazır fizik sensör fonksiyonu
    private void OnTriggerEnter(Collider other)
    {
        // hem tag "Key" olcak, hem kapı açılmamış olcak, hem de şifre doğru girilmiş olcak
        if (other.CompareTag("Key") && !isDoorOpened && isSafeOpened)
        {
            isDoorOpened = true; // durumu güncelle 
            OpenTheDoor();
        }
        // şifreyi girmeden anahtarı kapıya değdirirlerse uyar
        else if (other.CompareTag("Key") && !isSafeOpened)
        {
            Debug.Log("Kasa açılmadan bu kilit/anahtar çalışmaz!");
        }
    }

    private void OpenTheDoor()
    {
        // kapıyı açma efekti veriyoruz yukarı kayıyor
        doorObject.transform.position += new Vector3(0, openHeight, 0);
        Debug.Log("Sistem Doğrulandı: Kapı Açıldı!");
    }
}