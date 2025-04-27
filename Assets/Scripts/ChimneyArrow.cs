using UnityEngine;

public class ChimneyArrow : MonoBehaviour
{
    public GameObject dialogueObject; // The object with the Dialogue script attached
    private Dialogue dialogueScript;

    void Start()
    {
        // Get the Dialogue component from the dialogueObject
        dialogueScript = dialogueObject.GetComponent<Dialogue>();
    }

    void Update()
    {
        // Detect if the player clicks the arrow
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            if (IsMouseOverArrow())
            {
                // Start the dialogue when the arrow is clicked
                dialogueScript.StartDialogue();
            }
        }
    }

    bool IsMouseOverArrow()
    {
        // Raycast from the camera to check if the mouse is over the arrow
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePosition);

        return hit != null && hit.CompareTag("ChimneyArrow"); // Check if the arrow is clicked
    }
}