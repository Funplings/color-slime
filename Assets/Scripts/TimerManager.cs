using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {
    [SerializeField] float totalTime;
    [SerializeField] Text timerText;
    [SerializeField] GameObject levelCompletePanel;
    GameObject blobSpawner;
    GameObject player;
    float timeRemaining;
    bool levelOver = false;

    private void Awake() {
        timeRemaining = totalTime;

        // Get: blobSpwaner, player
        player = GameObject.FindGameObjectWithTag("Player");
        blobSpawner = GameObject.FindGameObjectWithTag("Blob Spawner");
    }

    private void Update() {
        if (!levelOver) {
            timeRemaining = Mathf.Max(0f, timeRemaining - Time.deltaTime);
            UpdateTimerText();
            if (timeRemaining <= 0) {
                levelOver = true;
                EndLevel();
            }
        }
    }

    void UpdateTimerText() {
        if (timerText != null) {
            string timeRemainingText = timeRemaining.ToString("F1");
            string[] times = timeRemainingText.Split('.');
            timerText.text = times[0] + ":" + times[1] + "0";
        }
        else {
            Debug.LogWarning("No timer text");
        }
    }

    void EndLevel() {
        // Turn on level complete darkened panel
        if (levelCompletePanel != null) {
            levelCompletePanel.SetActive(true);
        } else {
            Debug.LogWarning("No level complete panel");
        }

        // Stop blob spawning
        if (blobSpawner != null) {
            blobSpawner.GetComponent<BlobSpawner>().LevelOver();
        }
        else {
            Debug.LogWarning("No blob spwaner");
        }

        // Stop player movement
        if (player != null) {
            player.GetComponent<Player>().LevelOver();
        }
        else {
            Debug.LogWarning("No player");
        }

    }
}
