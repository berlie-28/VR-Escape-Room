using UnityEngine;

public class KeypadController : MonoBehaviour
{
    // açma şifresi
    public string correctPassword = "132"; 
    
    // basılan tuşları burada tutuyoz
    private string currentInput = ""; 

    // açılacak olan kapak
    public GameObject safeLid; 

    // sayıyı ekleme yeri
    public void AddDigit(string digit)
    {
        // yeni sayıyı sona ekle (1 yanına 2 gelince 12 olsun diye)
        currentInput += digit;
        Debug.Log("Şu anki durum: " + currentInput);

        // 3 basamak olduysa kontrol et
        if (currentInput.Length >= 3)
        {
            CheckPassword();
        }
    }

    void CheckPassword()
    {
        // şifre doğruysa
        if (currentInput == correctPassword)
        {
            Debug.Log("ŞİFRE DOĞRU!");
            // kapağı yana doğru döndür aç
            safeLid.transform.localRotation = Quaternion.Euler(0, 90, 0); 
        }
        else
        {
            Debug.Log("YANLIŞ ŞİFRE!");
            // yanlışsa sıfırla baştan yazsın
            currentInput = ""; 
        }
    }
}