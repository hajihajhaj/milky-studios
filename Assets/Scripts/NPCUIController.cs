using UnityEngine;

public class NPCUIController : MonoBehaviour
{
    public GameObject npcImageObject;    // Assign the NPC photo GameObject in Inspector
    public GameObject deliverButtonObject;  // Assign the deliver button GameObject in Inspector

    public int npcID;  // The ID for this NPC, assign in Inspector

    public void ShowUI()
    {
        if (npcImageObject != null) npcImageObject.SetActive(true);
        if (deliverButtonObject != null) deliverButtonObject.SetActive(true);
    }

    public void HideUI()
    {
        if (npcImageObject != null) npcImageObject.SetActive(false);
        if (deliverButtonObject != null) deliverButtonObject.SetActive(false);
    }
}