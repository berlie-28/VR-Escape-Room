using UnityEngine;
using TMPro;

public class NoteReaderUI : MonoBehaviour
{
    // panel where the text shows up (background box)
    public GameObject panel;

    // text field where the note's text goes
    public TMP_Text noteDisplayText;

    void Start()
    {
        // keep the panel closed at the start
        panel.SetActive(false);
    }

    // gets called when a note is clicked, shows the text
    public void Show(string text)
    {
        panel.SetActive(true);
        noteDisplayText.text = text;
    }

    // gets called when the close button is pressed
    public void Hide()
    {
        panel.SetActive(false);
    }
}