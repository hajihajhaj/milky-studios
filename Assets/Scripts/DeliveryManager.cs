using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

    private int currentDeliveryIndex = 0;

    void Start()
    {
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