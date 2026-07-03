using UnityEngine;

public class KeyDrag : MonoBehaviour
{
    // kamerayı elle bağlıyoruz
    public Camera testCamera;

    void OnMouseDrag()
    {
        if (testCamera == null) return;

        // anahtarın kameraya olan derinlik mesafesini koruyoruz
        float zDistance = testCamera.WorldToScreenPoint(transform.position).z;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance);

        // fareyi koordinatlara çevirip objeyi eşitliyoruz
        transform.position = testCamera.ScreenToWorldPoint(mousePos);
    }
}