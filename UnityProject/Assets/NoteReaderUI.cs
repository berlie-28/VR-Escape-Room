using UnityEngine;
using TMPro;

public class NoteReaderUI : MonoBehaviour
{
    // metnin göründüğü panel (arka plan kutusu)
    public GameObject panel;

    // notun yazısının basıldığı text alanı
    public TMP_Text noteDisplayText;

    void Start()
    {
        // oyun başında panel kapalı olsun
        panel.SetActive(false);
    }

    // bir not tıklanınca çağrılacak, metni gösterir
    public void Show(string text)
    {
        panel.SetActive(true);
        noteDisplayText.text = text;
    }

    // kapat butonuna basınca çağrılacak
    public void Hide()
    {
        panel.SetActive(false);
    }
}