using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnInterval = 3f;

    // Control how low/high birds spawn near top of the screen
    [Range(0.5f, 1f)]
    public float minYViewport = 0.7f;  // Start higher in the camera view
    public float maxYViewport = 0.95f;

    void Start()
    {
        InvokeRepeating("SpawnBirdAtCameraEdge", 1f, spawnInterval);
    }

    void SpawnBirdAtCameraEdge()
    {
        // Get world space of camera edges
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, maxYViewport, Camera.main.nearClipPlane));
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, minYViewport, Camera.main.nearClipPlane));

        // Randomize Y near the top part only
        float randomY = Random.Range(topLeft.y, topRight.y);
        float spawnX = topRight.x + 1f;  // Slightly off-screen

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
        Instantiate(birdPrefab, spawnPos, Quaternion.identity);
    }
}
