using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Mühendislik kuralı: Kodun kapıyı hareket ettirebilmesi için kapının ana gövdesine ihtiyacı var
    public GameObject doorObject; 
    public float openHeight = 3.5f; // Kapının yukarı doğru kaç metre kayacağı
    private bool isDoorOpened = false; // Kapının durumunu hafızada tutan State (Durum) değişkeni

    // Unity'nin hazır fizik sensör fonksiyonu (İçeriden bir nesne geçtiğinde otomatik tetiklenir)
    private void OnTriggerEnter(Collider other)
    {
        // Gelen nesnenin kimlik kartında (Tag) "Key" yazıyor mu ve kapı zaten açılmamış mı?
        if (other.CompareTag("Key") && !isDoorOpened)
        {
            isDoorOpened = true; // Durumu güncelle (Kapının tekrar tekrar tetiklenmesini engeller)
            OpenTheDoor();
        }
    }

    private void OpenTheDoor()
    {
        // Kapıyı odanın tavanına doğru, y ekseninde yukarı kaydırıyoruz
        doorObject.transform.position += new Vector3(0, openHeight, 0);
        Debug.Log("Sistem Doğrulandı: Kapı Açıldı!");
    }
}