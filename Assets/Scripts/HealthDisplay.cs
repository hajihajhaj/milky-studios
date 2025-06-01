using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite fullBox;
    public Sprite emptyBox;
    public Sprite deliveredBox;
    public Image[] boxes;

    public PlayerPackages playerPackages;

    void Update()
    {
        int delivered = playerPackages.packagesDelivered;
        int remaining = playerPackages.currentPackages;

        for (int i = 0; i < boxes.Length; i++)
        {
            if (i < delivered)
                boxes[i].sprite = deliveredBox;
            else if (i < delivered + remaining)
                boxes[i].sprite = fullBox;
            else
                boxes[i].sprite = emptyBox;
        }
    }
}