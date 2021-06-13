using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalColorManager : MonoBehaviour
{
    BlobSpawner blobSpawner;
    Color currGoalColor;

    [SerializeField]
    Image goalColorImage;

    [SerializeField]
    Image slimeColorImage;

    [SerializeField]
    Text colorMatchPercentageText;

    private void Start() {
        blobSpawner = GameObject.FindGameObjectWithTag("Blob Spawner").GetComponent<BlobSpawner>();
        GenerateGoalColor();
    }

    void GenerateGoalColor() {
        Color[] colors = blobSpawner.getColorsToSpawn();
        Dictionary<Color, int> goalColorDict = new Dictionary<Color, int>();
        foreach (Color c in colors) {
            goalColorDict.Add(c, Random.Range(3, 15));
        }
        currGoalColor =  ColorUtils.AverageColors(goalColorDict);
        UpdateGoalColorImage();
    }

    void UpdateGoalColorImage() {
        if (goalColorImage != null) {
            goalColorImage.color = currGoalColor;
            UpdateColorMatchPercentage();
        } else {
            Debug.LogWarning("No goal colar image");
        }
    }

    public void UpdateSlimeColorImage(Color slimeColor) {
        if (slimeColorImage != null) {
            slimeColorImage.color = slimeColor;
            UpdateColorMatchPercentage();
        } else {
            Debug.LogWarning("No slime color image");
        }
    }

    void UpdateColorMatchPercentage() {
        if (colorMatchPercentageText != null) {
            float matchPercentage = ColorUtils.CompareColors(goalColorImage.color, slimeColorImage.color);
            colorMatchPercentageText.text = string.Format("{0}%", (100 * matchPercentage).ToString("F2"));
        }
        else {
            Debug.LogWarning("No color match percentage text");
            UpdateColorMatchPercentage();
        }
        
    }
}
