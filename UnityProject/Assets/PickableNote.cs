using UnityEngine;

public class PickableNote : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pickupSound;

    // this note's content, gets set differently for each note in the Inspector
    [TextArea(3, 6)]
    public string noteText = "Write the note's text here...";

    // reference to the one NoteReaderUI in the scene
    public NoteReaderUI reader;

    void OnMouseDown()
    {
        if (audioSource != null && pickupSound != null)
            audioSource.PlayOneShot(pickupSound);

        reader.Show(noteText);

        // note has been read, hide it and stop it from being clicked again
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}