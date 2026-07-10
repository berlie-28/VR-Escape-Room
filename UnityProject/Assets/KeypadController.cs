using UnityEngine;
using System.Collections;

public class KeypadController : MonoBehaviour
{
    // açma şifresi
    public string correctPassword = "132";

    // basılan tuşları burada tutuyoz
    private string currentInput = "";

    // açılacak olan kapak
    public GameObject safeLid;

    // kapı koduna erişmek için buraya ekledik
    public DoorController doorControl;

    // renk değişimi için 
    public MeshRenderer panelRenderer;

    // panelin başlangıç rengini saklıyoruz
    private Color originalColor;

    void Start()
    {
        // açılışta orijinal rengi kaydet
        if (panelRenderer != null)
            originalColor = panelRenderer.material.color;
    }

    // sayıyı ekleme yeri
    public void AddDigit(string digit)
    {
        // yeni sayıyı sona ekle (1 yanına 2 gelince 12 olsun diye)
        currentInput += digit;
        Debug.Log("Şu anki durum: " + currentInput);

        // 3 basamak olduysa kontrol et
        if (currentInput.Length >= 3)
        {
            CheckPassword();
        }
    }

    void CheckPassword()
    {
        if (currentInput == correctPassword)
        {
            Debug.Log("ŞİFRE DOĞRU!");
            safeLid.SetActive(false);

            if (doorControl != null)
                doorControl.isSafeOpened = true;

            // paneli yeşil yak
            StartCoroutine(FlashColor(Color.green));
        }
        else
        {
            Debug.Log("YANLIŞ ŞİFRE!");
            currentInput = "";

            // paneli kırmızı yak
            StartCoroutine(FlashColor(Color.red));
        }
    }

    // paneli verilen renge boyayıp sonra orijinaline döndür
    private IEnumerator FlashColor(Color flashColor)
    {
        panelRenderer.material.color = flashColor;

        // 0.5 saniye bekle
        yield return new WaitForSeconds(0.5f);

        panelRenderer.material.color = originalColor;
    }
}