using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnInterval = 3f;     // Change this in Inspector for spawn rate
    public float birdSpeed = 2f;         // Change this in Inspector for bird speed

    [Range(0f, 1f)]
    public float minYViewport = 0.4f;
    public float maxYViewport = 0.6f;

    void Start()
    {
        InvokeRepeating("SpawnBirdAtCameraEdge", 1f, spawnInterval);
    }

    void SpawnBirdAtCameraEdge()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, minYViewport, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, maxYViewport, Camera.main.nearClipPlane));

        float randomY = Random.Range(bottomLeft.y, topRight.y);
        float spawnX = topRight.x + 1f;

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
        GameObject bird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);

        Bird birdScript = bird.GetComponent<Bird>();
        if (birdScript != null)
        {
            birdScript.SetSpeed(birdSpeed);
        }
    }
}
