using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    // butonun numarası (1, 2, 3)
    public string buttonValue; 
    
    // ana panelin kodu
    public KeypadController keypadController;

    // tıklandığında çalışacak yer
    public void PressButton()
    {
        Debug.Log("Butona basıldı: " + buttonValue);
        
        // sayıyı ana panele gönder
        keypadController.AddDigit(buttonValue);
    }
}