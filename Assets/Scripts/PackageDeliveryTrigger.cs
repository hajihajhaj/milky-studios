using UnityEngine;

public class PackageDeliveryTrigger : MonoBehaviour
{
    private bool hasDelivered = false;

    void OnMouseDown()
    {
        if (hasDelivered) return;

        PlayerPackages player = FindObjectOfType<PlayerPackages>();
        if (player != null && player.currentPackages > 0)
        {
            player.DeliverPackage();
            hasDelivered = true;

            // Optional: Show dialogue if using that too
            Dialogue dialogue = FindObjectOfType<Dialogue>();
            if (dialogue != null)
                dialogue.StartDialogue();
        }
    }
}