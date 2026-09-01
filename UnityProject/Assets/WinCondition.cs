using UnityEngine;
using TMPro;

public class WinCondition : MonoBehaviour
{
    // win text
    public TMP_Text winText;

    // reference to the timer to get the remaining time
    public CountdownTimer countdownTimer;

    // win sound
    public AudioSource audioSource;
    public AudioClip winSound;

    // stops this from triggering again after winning once
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        // was it the player that went through the door?
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;

            if (audioSource != null && winSound != null)
                audioSource.PlayOneShot(winSound);

            // stop the timer
            countdownTimer.StopTimer();

            // convert the remaining time into minutes:seconds
            float remaining = countdownTimer.GetRemainingTime();
            int minutes = Mathf.FloorToInt(remaining / 60);
            int seconds = Mathf.FloorToInt(remaining % 60);

            // show the win text
            winText.gameObject.SetActive(true);
            winText.text = string.Format("You Escaped!\nTime: {0:00}:{1:00}", minutes, seconds);
        }
    }
}