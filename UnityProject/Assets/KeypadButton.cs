using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    // the button's number (1, 2, 3)
    public string buttonValue;

    // reference to the main panel script
    public KeypadController keypadController;


    // sound source for the click sound
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PressButton()
    {
        Debug.Log("Button pressed: " + buttonValue);

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        // send the digit to the main panel
        keypadController.AddDigit(buttonValue);
    }
}
