using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalColorManager : MonoBehaviour
{
    BlobSpawner blobSpawner;
    Color currGoalColor;
    private Dictionary<Color, int> currRGBRatios;
    private ColorAbsorption slimeColorAbsorb;
    
    [SerializeField] 
    Image goalColorImage;

    [SerializeField]
    Image slimeColorImage;

    [SerializeField] 
    private Canvas nextColorCanvas;
    
    [SerializeField]
    [Range(0.7f, 1f)]
    private float percentCutoff;

    [SerializeField]
    Text colorMatchPercentageText;

    [SerializeField] 
    private float colorTransitionDelay;
    
    private void Start()
    {
        nextColorCanvas.enabled = false;
        blobSpawner = GameObject.FindGameObjectWithTag("Blob Spawner").GetComponent<BlobSpawner>();
        slimeColorAbsorb = FindObjectOfType<ColorAbsorption>();
        GenerateGoalColor();
    }

    void GenerateGoalColor() {
        Color[] colors = blobSpawner.getColorsToSpawn();
        Dictionary<Color, int> goalColorDict = new Dictionary<Color, int>();
        foreach (Color c in colors) {
            goalColorDict.Add(c, Random.Range(3, 15));
        }
        currRGBRatios = goalColorDict;
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
            if (matchPercentage >= percentCutoff)
            {
                StartCoroutine(AdvanceToNextColor());
            }
        }
        else {
            Debug.LogWarning("No color match percentage text");
            UpdateColorMatchPercentage();
        }

    }

    IEnumerator AdvanceToNextColor()
    {
        nextColorCanvas.enabled = true;
        blobSpawner.enabled = false;
        DestroyAllBlobs();
        yield return new WaitForSeconds(colorTransitionDelay);
        nextColorCanvas.enabled = false;
        GameManager.Instance.CurrLevelInfo.CompleteColor(currGoalColor, currRGBRatios);
        slimeColorAbsorb.Reset();
        GenerateGoalColor();
        yield return new WaitForSeconds(colorTransitionDelay);
        blobSpawner.enabled = true;
        yield return null;
    }

    void DestroyAllBlobs()
    {
        GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
        foreach (GameObject blob in blobs)
        {
            Destroy(blob);
        }
    }

    
}
