using UnityEngine;

public class KeyDrag : MonoBehaviour
{
    // drag the camera in manually
    public Camera testCamera;

    private Rigidbody rb;

    // key's collider, turn it into a trigger while holding so it doesn't push the player
    private Collider col;

    // are we currently holding the key
    private bool isHolding = false;

    // how far the key should stay from the camera while held
    private float holdDistance;

    // stops the key from getting dropped on the same frame it was picked up
    private bool justPickedUp = false;

    // sound for picking up the key
    public AudioSource audioSource;
    public AudioClip pickupSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // kinematic from the start so if a nearby object (like Safe_Front) gets
        // disabled, the key doesn't fall through the floor from gravity
        rb.isKinematic = true;
    }

    void OnMouseDown()
    {
        if (!isHolding)
        {
            // pick up the key: stop physics, save the distance
            isHolding = true;
            rb.isKinematic = true;

            if (audioSource != null && pickupSound != null)
                audioSource.PlayOneShot(pickupSound);

            // make the collider a trigger while holding so it doesn't push stuff, but still gets detected by the lock
            if (col != null) col.isTrigger = true;

            holdDistance = Mathf.Min(Vector3.Distance(testCamera.transform.position, transform.position), 1.5f);

            // don't let it get dropped on this same frame
            justPickedUp = true;
        }
    }

    void Update()
    {
        if (!isHolding) return;

        // skip the drop check on the frame we just picked it up
        if (justPickedUp)
        {
            justPickedUp = false;
            return;
        }

        // left click to drop
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = false;
            rb.isKinematic = false;
            if (col != null) col.isTrigger = false;
            return;
        }

        Ray ray = testCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 newPos = ray.GetPoint(holdDistance);

        // offset the key a bit right and down so it looks more natural
        newPos += testCamera.transform.right * 0.1f;
        newPos -= testCamera.transform.up * 0.2f;

        // if there's a wall/door in the way, stop the key in front of it (ignore its own body)
        Vector3 dir = newPos - testCamera.transform.position;
        float dist = dir.magnitude;
        if (Physics.Raycast(testCamera.transform.position, dir.normalized, out RaycastHit hit, dist, ~0, QueryTriggerInteraction.Ignore)
            && hit.rigidbody != rb)
        {
            newPos = hit.point - dir.normalized * 0.1f;
        }

        transform.position = newPos;
    }
}