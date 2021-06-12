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
        float r = 0;
        float g = 0;
        float b = 0;
        int totalColors = 0;
        foreach (KeyValuePair<Color, int> pair in score)
        {
            r += pair.Key.r * pair.Value;
            g += pair.Key.g * pair.Value;
            b += pair.Key.b * pair.Value;
            totalColors += pair.Value;
            Debug.Log(pair.Key);
        }
        
        if (totalColors == 0)
        {
            sr.color = Color.white;
            return;
        }
        
        Debug.Log(r + ", " + b + ", " + g);
        Debug.Log(totalColors);
        r /= totalColors;
        g /= totalColors;
        b /= totalColors;
        float[] rgb = {r, g, b};
        float min = Mathf.Min(rgb);
        float max = Mathf.Max(rgb);

        if (max == min)
        {
            sr.color = Color.white;
            return;
        }
        
        r -= min;
        g -= min;
        b -= min;
        r /= (max - min);
        g /= (max - min);
        b /= (max - min);

        // (0.1, 0.3, 0.4)
        // (0, 0.2, 0.3)
        // (0, 0.67, 1)

        Color c = new Color(r, g, b);
        sr.color = c;
        
    }

    public void Absorb(Color color)
    {
        score[color] += 1;
        UpdateCurrentColor();
    }
    
}
