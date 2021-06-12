using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class ColorAbsorption : MonoBehaviour
{
    #region Private Variables

    private Dictionary<Color, int> score;
    
    #endregion

    #region Cached Variables

    private SpriteRenderer sr;
    private BlobSpawner blobSpawner;
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        blobSpawner = FindObjectOfType<BlobSpawner>();
        Color[] levelColors = blobSpawner.ColorsToSpawn;
        score = new Dictionary<Color, int>();
        foreach (Color c in levelColors)
        {
            score.Add(c, 0);   
        }
        UpdateCurrentColor();
    }

    void UpdateCurrentColor()
    {
        Color c = ColorUtils.AverageColors(score);
        sr.color = c;

    }

    public void Absorb(Color color)
    {
        score[color] += 1;
        UpdateCurrentColor();
    }
    
}
