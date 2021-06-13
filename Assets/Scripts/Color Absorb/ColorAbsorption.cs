using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class ColorAbsorption : MonoBehaviour
{
    #region Private Variables

    private Dictionary<Color, int> score;
    private GoalColorManager goalColorManager;
    
    #endregion

    #region Cached Variables

    [SerializeField] private SpriteRenderer sr;
    private BlobSpawner blobSpawner;
    
    #endregion
    
    // Start is called before the first frame update

    void Awake()
    {
        blobSpawner = FindObjectOfType<BlobSpawner>();
    }

    void Start()
    {
        // Set goal color manager
        GameObject goalColorManagerObject = GameObject.FindGameObjectWithTag("Goal Color Manager");
        if (goalColorManagerObject != null) goalColorManager = goalColorManagerObject.GetComponent<GoalColorManager>();

        // sr = GetComponent<SpriteRenderer>();
        Reset();
    }

    void UpdateCurrentColor()
    {
        Color c = ColorUtils.AverageColors(score);
        sr.color = c;
        UpdateSlimeColorImage();
    }

    void UpdateSlimeColorImage() {
        // Update slime color image, if possible
        if (goalColorManager != null) {
            goalColorManager.UpdateSlimeColorImage(sr.color);
        } else {
            Debug.LogWarning("No goal color manager");
        }
    }

    public void Absorb(Color color)
    {
        score[color] += 1;
        UpdateCurrentColor();
    }

    public void Reset()
    {
        Color[] levelColors = blobSpawner.ColorsToSpawn;
        score = new Dictionary<Color, int>();
        foreach (Color c in levelColors)
        {
            score.Add(c, 0);
        }
        UpdateCurrentColor();
        UpdateSlimeColorImage();
    }
    
}
