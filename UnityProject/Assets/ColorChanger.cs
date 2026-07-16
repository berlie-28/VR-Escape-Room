using UnityEngine;
using System.Collections;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer targetRenderer;

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

    void Start()
    {
        targetRenderer = GetComponent<MeshRenderer>();
        // oyun başında ilk renge ayarla
        targetRenderer.material.color = colors[currentIndex];
    }

    void OnMouseDown()
    {
        // bekleme varsa iptal et çünkü maviyi geçti
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;

            if (statusText != null)
                statusText.text = "";
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
        // ekranda mesaj göster
        if (statusText != null)
            statusText.text = "Counting to 3...";

        // 3 saniye bekle
        yield return new WaitForSeconds(3f);

        // şifreyi göster, topu yok et
        if (statusText != null)
            statusText.text = "";

        codeNote.SetActive(true);

        // ipucu yazısını gizle
        if (hintNote != null)
            hintNote.SetActive(false);

        gameObject.SetActive(false);
    }
}