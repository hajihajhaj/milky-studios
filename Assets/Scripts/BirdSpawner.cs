using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnInterval = 3f;

    // Adjusted to spawn more around the UPPER MIDDLE area
    [Range(0f, 1f)]
    public float minYViewport = 0.4f;  // Lowered from 0.7f to 0.4f
    public float maxYViewport = 0.6f;  // Lowered from 0.95f to 0.6f

    void Start()
    {
        InvokeRepeating("SpawnBirdAtCameraEdge", 1f, spawnInterval);
    }

    void SpawnBirdAtCameraEdge()
    {
        // Get world space of camera edges
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, minYViewport, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, maxYViewport, Camera.main.nearClipPlane));

        // Randomize Y in upper-middle band
        float randomY = Random.Range(bottomLeft.y, topRight.y);
        float spawnX = topRight.x + 1f;  // Slightly off-screen

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
        Instantiate(birdPrefab, spawnPos, Quaternion.identity);
    }
}
