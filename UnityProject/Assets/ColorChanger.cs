using UnityEngine;
using UnityEngine.InputSystem; // Yeni girdi kütüphanesini projeye dahil ettik

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer targetRenderer;

    void Start()
    {
        targetRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Yeni Girdi Sisteminde klavyeden 'C' tuşuna basılıp basılmadığını kontrol etme yöntemi:
        if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChangeToRandomColor();
        }
    }

    public void ChangeToRandomColor()
    {
        targetRenderer.material.color = Random.ColorHSV();
    }
}