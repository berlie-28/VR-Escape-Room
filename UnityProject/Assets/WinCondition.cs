using UnityEngine;
using TMPro;

public class WinCondition : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip winSound;
    // kazanma yazısı
    public TMP_Text winText;

    // kalan süreyi almak için sayaca erişiyoruz
    public CountdownTimer countdownTimer;

    private void OnTriggerEnter(Collider other)
    {
        // kapıdan geçen oyuncu muydu?
        if (other.CompareTag("Player"))
        {
            // sayacı durdur
            countdownTimer.StopTimer();

            // kalan süreyi dakika:saniye formatına çevir
            float remaining = countdownTimer.GetRemainingTime();
            int minutes = Mathf.FloorToInt(remaining / 60);
            int seconds = Mathf.FloorToInt(remaining % 60);

            // kazanma yazısını göster
            winText.gameObject.SetActive(true);
            winText.text = string.Format("You Escaped!\nTime: {0:00}:{1:00}", minutes, seconds);
            if (audioSource != null && winSound != null)
                audioSource.PlayOneShot(winSound);
        }
    }
}