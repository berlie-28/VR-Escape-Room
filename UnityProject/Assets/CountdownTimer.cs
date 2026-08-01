using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    // başlangıç süresi saniye cinsinden (180 = 3 dakika)
    public float totalTime = 180f;

    // sayacı gösteren yazı
    public TMP_Text timerText;

    // süre bitince çıkan yazı
    public GameObject timeUpText;

    // kalan süre
    private float timeRemaining;

    // sayaç duruyor mu
    private bool timerRunning = true;

    void Start()
    {
        // başlangıçta toplam süreyi ver
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (!timerRunning) return;

        // kalan süreden geçen zamanı çıkar
        timeRemaining -= Time.deltaTime;

        // sıfırın altına düşmesin diye
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            TimerEnded();
        }

        // ekrana yaz
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // saniyeyi dakika ve saniyeye çevir
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        // sayacı sıfır göster
        timerText.text = "00:00";

        // süre bitti yazısını göster
        if (timeUpText != null)
            timeUpText.SetActive(true);
    }
}
