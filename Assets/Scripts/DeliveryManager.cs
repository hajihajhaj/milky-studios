using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [System.Serializable]
    public class Delivery
    {
        public int npcID;
        public Sprite customerFace;
        public bool completed = false;
    }

    public PhoneUIController phoneUI;
    public List<Delivery> deliveries = new List<Delivery>();

    public List<NPCUIController> npcUIControllers; // Assign NPC UI controllers in Inspector

    private int currentDeliveryIndex = 0;

    void Start()
    {
        // Subscribe to all Dialogue components' close events
        Dialogue[] dialogues = FindObjectsOfType<Dialogue>();
        foreach (Dialogue dialogue in dialogues)
        {
            dialogue.onDialogueClosedWithID.AddListener(OnDialogueClosed);
        }

        if (deliveries.Count > 0)
        {
            StartDelivery(currentDeliveryIndex);
        }
    }

    public void OnDialogueClosed(int npcID)
    {
        if (currentDeliveryIndex < deliveries.Count)
        {
            if (deliveries[currentDeliveryIndex].npcID == npcID)
            {
                deliveries[currentDeliveryIndex].completed = true;
                phoneUI.CompleteDelivery();

                // Hide the NPC UI for this npcID
                foreach (var npcUI in npcUIControllers)
                {
                    if (npcUI.npcID == npcID)
                    {
                        npcUI.HideUI();
                        break;
                    }
                }

                currentDeliveryIndex++;

                if (currentDeliveryIndex < deliveries.Count)
                {
                    StartDelivery(currentDeliveryIndex);
                }
                else
                {
                    Debug.Log("All deliveries done!");
                }
            }
        }
    }

    private void StartDelivery(int index)
    {
        phoneUI.StartNewDelivery(deliveries[index].customerFace);

        // Optionally show the NPC UI again if you want
        foreach (var npcUI in npcUIControllers)
        {
            if (npcUI.npcID == deliveries[index].npcID)
            {
                npcUI.ShowUI();
                break;
            }
        }
    }

    public void SkipCurrentDelivery()
    {
        currentDeliveryIndex++;

        if (currentDeliveryIndex < deliveries.Count)
        {
            StartDelivery(currentDeliveryIndex);
        }
        else
        {
            Debug.Log("All deliveries done!");
        }
    }
}