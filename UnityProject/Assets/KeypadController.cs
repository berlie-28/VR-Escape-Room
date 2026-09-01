using UnityEngine;
using System.Collections;
using TMPro;



public class KeypadController : MonoBehaviour
{
    // correct/wrong password sounds
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;// the safe password

    public string correctPassword = "132";

    // keeps track of the digits pressed so far
    private string currentInput = "";

    // the lid that opens
    public GameObject safeLid;

    // reference to reach the door's code
    public DoorController doorControl;

    // for changing the panel color
    public MeshRenderer panelRenderer;

    // saves the panel's starting color
    private Color originalColor;

    // screen that shows the digits as you press them
    public TMP_Text displayText;

    // becomes true once the color puzzle is solved
    [HideInInspector] public bool isCodeRevealed = false;

    void Start()
    {
        // save the original color at the start
        if (panelRenderer != null)
            originalColor = panelRenderer.material.color;

        // keep the display empty at the start
        if (displayText != null)
            displayText.text = "";
    }

    // called when a digit gets added
    public void AddDigit(string digit)
    {
         // pressing buttons shouldn't do anything before the code is revealed
        if (!isCodeRevealed) return;

        // add the new digit at the end
        currentInput += digit;

        // show it on the display
        if (displayText != null)
            displayText.text = currentInput;

        Debug.Log("Current input: " + currentInput);

        // check the password once we have 3 digits
        if (currentInput.Length >= 3)
        {
            StartCoroutine(CheckPasswordWithDelay());
        }
    }

    // show the last digit on screen, wait a bit, then check the password
    private IEnumerator CheckPasswordWithDelay()
    {
        yield return new WaitForSeconds(0.4f);
        CheckPassword();
    }
    
    void CheckPassword()
    {
        if (currentInput == correctPassword)
        {
            Debug.Log("CORRECT PASSWORD!");
            safeLid.SetActive(false);

            if (doorControl != null)
                doorControl.isSafeOpened = true;

            if (audioSource != null && correctSound != null)
                audioSource.PlayOneShot(correctSound);

            // flash the panel green
            StartCoroutine(FlashColor(Color.green));
        }
        else
        {
            Debug.Log("WRONG PASSWORD!");

            if (audioSource != null && wrongSound != null)
                audioSource.PlayOneShot(wrongSound);

            // flash the panel red
            StartCoroutine(FlashColor(Color.red));
        }

        // reset the display and input
        currentInput = "";
        if (displayText != null)
            displayText.text = "";
    }

    // flash the panel to a color then go back to the original one
    private IEnumerator FlashColor(Color flashColor)
    {
        panelRenderer.material.color = flashColor;

        // wait half a second
        yield return new WaitForSeconds(0.5f);

        panelRenderer.material.color = originalColor;
    }
}