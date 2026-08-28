using UnityEngine;
using TMPro;

public class WinCondition : MonoBehaviour
{
    // kazanma yazısı
    public TMP_Text winText;

    // kalan süreyi almak için sayaca erişiyoruz
    public CountdownTimer countdownTimer;

    // kazanma sesi
    public AudioSource audioSource;
    public AudioClip winSound;

    // bir kere kazanınca tekrar tetiklenmesin
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        // kapıdan geçen oyuncu muydu?
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;

            if (audioSource != null && winSound != null)
                audioSource.PlayOneShot(winSound);

            // sayacı durdur
            countdownTimer.StopTimer();

            // kalan süreyi dakika:saniye formatına çevir
            float remaining = countdownTimer.GetRemainingTime();
            int minutes = Mathf.FloorToInt(remaining / 60);
            int seconds = Mathf.FloorToInt(remaining % 60);

            // kazanma yazısını göster
            winText.gameObject.SetActive(true);
            winText.text = string.Format("You Escaped!\nTime: {0:00}:{1:00}", minutes, seconds);
        }
    }
}