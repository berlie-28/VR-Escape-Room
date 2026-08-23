using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    // butonun numarası (1, 2, 3)
    public string buttonValue; 
    
    // ana panelin kodu
    public KeypadController keypadController;


    // tık sesini çalacak ses kaynağı
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PressButton()
    {
        Debug.Log("Butona basıldı: " + buttonValue);
        
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        // sayıyı ana panele gönder
        keypadController.AddDigit(buttonValue);
    }
}
