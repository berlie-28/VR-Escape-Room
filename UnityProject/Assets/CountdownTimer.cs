using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    // starting time in seconds (180 = 3 minutes)
    public float totalTime = 180f;

    // text showing the timer
    public TMP_Text timerText;

    // text that shows up when time runs out
    public GameObject timeUpText;

    // time left
    private float timeRemaining;

    // is the timer still running
    private bool timerRunning = true;

    void Start()
    {
        // set the remaining time to the total at the start
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (!timerRunning) return;

        // subtract the time that passed from the remaining time
        timeRemaining -= Time.deltaTime;

        // don't let it go below zero
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            TimerEnded();
        }

        // update the text on screen
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // convert seconds into minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        // show the timer as zero
        timerText.text = "00:00";

        // show the "time's up" text
        if (timeUpText != null)
            timeUpText.SetActive(true);

    }

    // lets other scripts stop the timer
    public void StopTimer()
    {
        timerRunning = false;
    }

    // lets other scripts read the remaining time
    public float GetRemainingTime()
    {
        return timeRemaining;
    }

} 