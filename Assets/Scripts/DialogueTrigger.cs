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
    }
}