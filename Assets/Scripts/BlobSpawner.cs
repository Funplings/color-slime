using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {
    [SerializeField] GameObject topLeft;
    [SerializeField] GameObject bottomRight;
    [SerializeField] GameObject blobPrefab;
    [SerializeField] float blobDirectionSpread;
    [SerializeField] float minSpawnDelay;
    [SerializeField] float maxSpawnDelay;
    [SerializeField] Color[] colorsToSpawn;

    [SerializeField] bool spawnAbove;
    [SerializeField] bool spawnRight;
    [SerializeField] bool spawnBelow;
    [SerializeField] bool spawnLeft;
    List<int> spawnDirections;

    bool timesUp = false;
    int colorIndex = 0;

    public Color[] ColorsToSpawn
    {
        get => colorsToSpawn;
        set => colorsToSpawn = value;
    }
    
    float minX, minY, maxX, maxY;

    private void Start() {
        // Get min, max x, y positions
        minX = topLeft.transform.position.x;
        minY = bottomRight.transform.position.y;
        maxX = bottomRight.transform.position.x;
        maxY = topLeft.transform.position.y;

        // Get spawn directions
        spawnDirections = new List<int>();
        if (spawnAbove) spawnDirections.Add(0);
        if (spawnRight) spawnDirections.Add(1);
        if (spawnBelow) spawnDirections.Add(2);
        if (spawnLeft) spawnDirections.Add(3);

        StartCoroutine("spawnBlob", 0);
    }

    IEnumerator spawnBlob(float timeToSpawn) {
        yield return new WaitForSeconds(timeToSpawn);
        if (!timesUp) {
            Vector3 blobPosition = Vector3.zero;
            Vector3 blobDirection = Vector3.zero;
            Quaternion blobDirectionRotation = Quaternion.Euler(0, 0, Random.Range(-0.5f * blobDirectionSpread, 0.5f * blobDirectionSpread));

            // Choose direction (0 = above, 1 = right, 2 = down, 3 = left)
            int side = spawnDirections[Random.Range(0, spawnDirections.Count)];

            switch (side) {
                case 0:
                    blobPosition.x = Random.Range(minX, maxX);
                    blobPosition.y = maxY;
                    blobDirection = Vector3.down;
                    break;
                case 1:
                    blobPosition.x = maxX;
                    blobPosition.y = Random.Range(minY, maxY);
                    blobDirection = Vector3.left;
                    break;
                case 2:
                    blobPosition.x = Random.Range(minX, maxX);
                    blobPosition.y = minY;
                    blobDirection = Vector3.up;
                    break;
                case 3:
                    blobPosition.x = minX;
                    blobPosition.y = Random.Range(minY, maxY);
                    blobDirection = Vector3.right;
                    break;
            }

            // Instantiate blob and set direction and color
            // int i = Random.Range(0, colorsToSpawn.Length);
            // Color color = colorsToSpawn[Random.Range(0, colorsToSpawn.Length)];
            Color color = colorsToSpawn[colorIndex];
            colorIndex = (colorIndex + 1) % colorsToSpawn.Length;

            GameObject blob = Instantiate(blobPrefab, blobPosition, Quaternion.identity);
            blob.GetComponent<Blob>().SetDirection(blobDirectionRotation * blobDirection);
            blob.GetComponent<Blob>().SetColor(color);

            // Start coroutine for next blob spawn
            StartCoroutine("spawnBlob", Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    public Color[] getColorsToSpawn() {
        return colorsToSpawn;
    }

    public void LevelOver() {
        timesUp = true;
    }
}
