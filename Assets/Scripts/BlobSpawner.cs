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

    public Color[] ColorsToSpawn
    {
        get => colorsToSpawn;
        set => colorsToSpawn = value;
    }
    
    float minX, minY, maxX, maxY;

    private void Start() {
        minX = topLeft.transform.position.x;
        minY = bottomRight.transform.position.y;
        maxX = bottomRight.transform.position.x;
        maxY = topLeft.transform.position.y;
        StartCoroutine("spawnBlob", 0);
    }

    IEnumerator spawnBlob(float timeToSpawn) {
        yield return new WaitForSeconds(timeToSpawn);
        Vector3 blobPosition = Vector3.zero;
        Vector3 blobDirection = Vector3.zero;
        Quaternion blobDirectionRotation = Quaternion.Euler(0, 0, Random.Range(-0.5f * blobDirectionSpread, 0.5f * blobDirectionSpread));

        // Choose direction (0 = above, 1 = right, 2 = down, 3 = left)
        int side = Random.Range(0, 4);
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
        int i = Random.Range(0, colorsToSpawn.Length);
        Color color = colorsToSpawn[Random.Range(0, colorsToSpawn.Length)];
        GameObject blob = Instantiate(blobPrefab, blobPosition, Quaternion.identity);
        blob.GetComponent<Blob>().SetDirection(blobDirectionRotation * blobDirection);
        blob.GetComponent<Blob>().SetColor(color);

        // Start coroutine for next blob spawn
        StartCoroutine("spawnBlob", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    public Color[] getColorsToSpawn() {
        return colorsToSpawn;
    }
}
