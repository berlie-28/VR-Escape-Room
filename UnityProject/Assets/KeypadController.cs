using UnityEngine;
using System.Collections;
using TMPro;



public class KeypadController : MonoBehaviour
{
    // doğru/yanlış şifre sesleri
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;// açma şifresi
    
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

    // bastıkça sayıları gösteren ekran
    public TMP_Text displayText;

    // renk bulmacası çözüldükten sonra true oluyor
    [HideInInspector] public bool isCodeRevealed = false;

    void Start()
    {
        // açılışta orijinal rengi kaydet
        if (panelRenderer != null)
            originalColor = panelRenderer.material.color;

        // ekranı başta boş göster
        if (displayText != null)
            displayText.text = "";
    }

    // sayıyı ekleme yeri
    public void AddDigit(string digit)
    {
         // şifre henüz alınmadıysa butona basmak işe yaramasın
        if (!isCodeRevealed) return;

        // yeni sayıyı sona ekle
        currentInput += digit;

        // ekranda göster
        if (displayText != null)
            displayText.text = currentInput;

        Debug.Log("Şu anki durum: " + currentInput);

        // 3 basamak olduysa kontrol et
        if (currentInput.Length >= 3)
        {
            StartCoroutine(CheckPasswordWithDelay());
        }
    }

    // son sayıyı ekranda göster, biraz bekle sonra kontrol et
    private IEnumerator CheckPasswordWithDelay()
    {
        yield return new WaitForSeconds(0.4f);
        CheckPassword();
    }
    
    void CheckPassword()
    {
        if (currentInput == correctPassword)
        {
            Debug.Log("ŞİFRE DOĞRU!");
            safeLid.SetActive(false);

            if (doorControl != null)
                doorControl.isSafeOpened = true;

            if (audioSource != null && correctSound != null)
                audioSource.PlayOneShot(correctSound);

            // paneli yeşil yak
            StartCoroutine(FlashColor(Color.green));
        }
        else
        {
            Debug.Log("YANLIŞ ŞİFRE!");

            if (audioSource != null && wrongSound != null)
                audioSource.PlayOneShot(wrongSound);

            // paneli kırmızı yak
            StartCoroutine(FlashColor(Color.red));
        }

        // ekranı ve girişi sıfırla
        currentInput = "";
        if (displayText != null)
            displayText.text = "";
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