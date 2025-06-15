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
    public List<NPCUIController> npcUIControllers;

    private int currentDeliveryIndex = 0;

    void Start()
    {
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
        if (currentDeliveryIndex < deliveries.Count)
        {
            // Hide the NPC UI for the skipped delivery
            int skippedNPCID = deliveries[currentDeliveryIndex].npcID;

            foreach (var npcUI in npcUIControllers)
            {
                if (npcUI.npcID == skippedNPCID)
                {
                    npcUI.HideUI();
                    break;
                }
            }

            // Mark as completed so it won't be delivered later
            deliveries[currentDeliveryIndex].completed = true;

            // Move to the next delivery
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

    // Used by Dialogue.cs to validate delivery target
    public int GetCurrentDeliveryNPCID()
    {
        if (currentDeliveryIndex < deliveries.Count)
        {
            return deliveries[currentDeliveryIndex].npcID;
        }
        return -1;
    }
}