using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;

    private void OnMouseDown()
    {
        // Start the dialogue
        if (dialogueScript != null)
        {
            dialogueScript.StartDialogue();
        }

        // Deliver the package
        PlayerPackages player = FindObjectOfType<PlayerPackages>();
        if (player != null)
        {
            player.DeliverPackage();
        }
    }
}
