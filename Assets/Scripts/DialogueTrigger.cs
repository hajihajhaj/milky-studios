using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;

    private void OnMouseDown()
    {
        if (dialogueScript != null)
        {
            dialogueScript.StartDialogue();
        }

        // ? REMOVE this section:
        /*
        PlayerPackages player = FindObjectOfType<PlayerPackages>();
        if (player != null)
        {
            player.DeliverPackage();
        }
        */
    }
}
