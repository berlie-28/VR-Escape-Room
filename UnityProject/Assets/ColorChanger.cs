using UnityEngine;
using System.Collections;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip revealSound;
    private MeshRenderer targetRenderer;

    // text component of the code note, used for the animation
    private TMP_Text codeNoteText;

    // the actual code note text, saved at the start
    private string finalCodeText;

    // colors it cycles through
    private Color[] colors = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0f),    // orange
        Color.yellow,
        Color.green,
        new Color(0.5f, 0f, 0.5f),  // purple
        Color.white,
        Color.blue                   // target color
    };

    // current color index
    private int currentIndex = 0;

    // index of blue
    private int correctIndex = 6;

    // reference to the 3 second wait coroutine
    private Coroutine waitCoroutine;

    // message shown while waiting the 3 seconds
    public GameObject codeNote;

    // the message at the start
    public TMP_Text statusText;

    // hint text, gets hidden once the code shows up
    public GameObject hintNote;

    // reference to the keypad to unlock it once code is revealed
    public KeypadController keypadController;

    void Start()
    {
        targetRenderer = GetComponent<MeshRenderer>();
        // set it to the first color at the start
        targetRenderer.material.color = colors[currentIndex];

        // find the code note's text component and save the real text
        codeNoteText = codeNote.GetComponent<TMP_Text>();
        if (codeNoteText != null)
            finalCodeText = codeNoteText.text;
    }

    void OnMouseDown()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        // cancel the wait if it's running, since we just clicked past blue
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;

            // hide the code note again, animation got interrupted
            codeNote.SetActive(false);

            if (hintNote != null)
            hintNote.SetActive(true);
        }

        // move to the next color, loop back to the start when it reaches the end
        currentIndex = (currentIndex + 1) % colors.Length;
        targetRenderer.material.color = colors[currentIndex];

        // check if we landed on the correct color
        if (currentIndex == correctIndex)
        {
            waitCoroutine = StartCoroutine(ShowCodeAfterDelay());
        }
    }

    private IEnumerator ShowCodeAfterDelay()
    {
        // show the code note right away, but with a loading animation first
        codeNote.SetActive(true);

        // hide the hint text
        if (hintNote != null)
        hintNote.SetActive(false);

        // play the dots animation for 3 seconds
        string[] dots = { ".", ". .", ". . ." };
        float elapsed = 0f;
        int dotIndex = 0;

        while (elapsed < 3f)
        {
            // show the next dot pattern
            if (codeNoteText != null)
                codeNoteText.text = dots[dotIndex % 3];

            dotIndex++;
            yield return new WaitForSeconds(0.75f);
            elapsed += 0.75f;
        }

        // animation is done, write the real code
        if (codeNoteText != null)
            codeNoteText.text = finalCodeText;

        // play the sound once the text actually shows up
        if (audioSource != null && revealSound != null)
            audioSource.PlayOneShot(revealSound);

        // keypad can be used now
        keypadController.isCodeRevealed = true;


        // instead of fully disabling the ball, just make it invisible so audio doesn't cut off
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}