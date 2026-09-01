using UnityEngine;
using System.Collections;


public class DoorController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

// needs a reference to the door's main body
    public GameObject doorObject;
    public float openHeight = 3.5f;   // how many meters the door slides up
    public float openDuration = 1.5f; // how long it takes to open

    // keeps track of whether the door is open
    private bool isDoorOpened = false;

    // tracks whether the safe has been opened
    [HideInInspector] public bool isSafeOpened = false;

    // Unity's built-in trigger function
    private void OnTriggerEnter(Collider other)
    {
        // needs to be tagged "Key", door not open yet, and the code already entered
        if (other.CompareTag("Key") && !isDoorOpened && isSafeOpened)
        {
            isDoorOpened = true;
            // start the open animation
            StartCoroutine(OpenTheDoor());
        }
        // warn if the key touches the door before the code is entered
        else if (other.CompareTag("Key") && !isSafeOpened)
        {
            Debug.Log("This lock doesn't work until the safe is opened!");
        }
    }

    private IEnumerator OpenTheDoor()
    {
        if (audioSource != null && doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);

        // save the start and target position
        Vector3 startPos = doorObject.transform.position;
        Vector3 targetPos = startPos + new Vector3(0, openHeight, 0);

        // counts how much time has passed
        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            // add the time passed this frame
            elapsed += Time.deltaTime;

            // progress value going from 0 to 1
            float t = elapsed / openDuration;

            // door opens fast at first, then slows down near the end
            t = t * t * (3f - 2f * t);

            // calculate the current position between start and target
            doorObject.transform.position = Vector3.Lerp(startPos, targetPos, t);

            // wait for the next frame
            yield return null;
        }

        // snap exactly to the target position
        doorObject.transform.position = targetPos;
        Debug.Log("System verified: door opened!");
    }
}