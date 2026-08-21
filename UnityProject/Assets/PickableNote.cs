using UnityEngine;

public class PickableNote : MonoBehaviour
{
    // bu notun içeriği, Inspector'dan her not için farklı yazılacak
    [TextArea(3, 6)]
    public string noteText = "Buraya notun metnini yaz...";

    // sahnedeki tek NoteReaderUI'ye referans
    public NoteReaderUI reader;

    void OnMouseDown()
    {
        reader.Show(noteText);
    }
}