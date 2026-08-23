using UnityEngine;

public class PickableNote : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pickupSound;

    // bu notun içeriği, Inspector'dan her not için farklı yazılacak
    [TextArea(3, 6)]
    public string noteText = "Buraya notun metnini yaz...";

    // sahnedeki tek NoteReaderUI'ye referans
    public NoteReaderUI reader;

    void OnMouseDown()
    {
        if (audioSource != null && pickupSound != null)
            audioSource.PlayOneShot(pickupSound);

        reader.Show(noteText);

        // not okundu, artık görünmesin ve tekrar tıklanamasın
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}