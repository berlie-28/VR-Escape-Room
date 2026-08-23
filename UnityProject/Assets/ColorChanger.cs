using UnityEngine;
using System.Collections;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip revealSound;
    private MeshRenderer targetRenderer;

    // code note'un text bileşeni, animasyon için lazım
    private TMP_Text codeNoteText;

    // code note'un asıl metni (şifre), başta kaydediyoruz
    private string finalCodeText;

    // sırayla değişecek renkler
    private Color[] colors = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0f),    // turuncu
        Color.yellow,
        Color.green,
        new Color(0.5f, 0f, 0.5f),  // mor
        Color.white,
        Color.blue                   // hedef renk
    };

    // şu anki renk indexi
    private int currentIndex = 0;

    // mavinin indexi 
    private int correctIndex = 6;

    // 3 saniyelik beklemeyi saklıyoruz
    private Coroutine waitCoroutine;

    // 3 saniye beklerkenki mesaj
    public GameObject codeNote;

    // baştaki mesaj
    public TMP_Text statusText;

    // ipucu yazısı, şifre çıkınca gizlenecek
    public GameObject hintNote;

    // şifreyi aktifleştirmek için keypad'e erişiyoruz
    public KeypadController keypadController;

    void Start()
    {
        targetRenderer = GetComponent<MeshRenderer>();
        // oyun başında ilk renge ayarla
        targetRenderer.material.color = colors[currentIndex];

        // code note'un text bileşenini bul ve asıl metni kaydet
        codeNoteText = codeNote.GetComponent<TMP_Text>();
        if (codeNoteText != null)
            finalCodeText = codeNoteText.text;
    }

    void OnMouseDown()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        // bekleme varsa iptal et çünkü maviyi geçti
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;

            // code note'u tekrar gizle, animasyon yarıda kaldı
            codeNote.SetActive(false);

            if (hintNote != null)
            hintNote.SetActive(true);
        }

        // bir sonraki renge geç ve sona gelince başa dön
        currentIndex = (currentIndex + 1) % colors.Length;
        targetRenderer.material.color = colors[currentIndex];

        // doğru renge geldi mi diye bakıyoruz
        if (currentIndex == correctIndex)
        {
            waitCoroutine = StartCoroutine(ShowCodeAfterDelay());
        }
    }

    private IEnumerator ShowCodeAfterDelay()
    {
        // code note'u hemen göster ama önce yükleniyor animasyonu
        codeNote.SetActive(true);

        // ipucu yazısını gizle
        if (hintNote != null)
        hintNote.SetActive(false);

        // 3 saniye boyunca nokta animasyonu oynat
        string[] dots = { ".", ". .", ". . ." };
        float elapsed = 0f;
        int dotIndex = 0;

        while (elapsed < 3f)
        {
            // sıradaki nokta desenini göster
            if (codeNoteText != null)
                codeNoteText.text = dots[dotIndex % 3];

            dotIndex++;
            yield return new WaitForSeconds(0.75f);
            elapsed += 0.75f;
        }

        // animasyon bitti, asıl şifreyi yaz
        if (codeNoteText != null)
            codeNoteText.text = finalCodeText;

        // yazı gerçekten ekrana gelince ses çal
        if (audioSource != null && revealSound != null)
            audioSource.PlayOneShot(revealSound);

        // artık keypad kullanılabilir
        keypadController.isCodeRevealed = true;


        // topu tamamen kapatmak yerine sadece görünmez/tıklanamaz yap, ses kesilmesin
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}