using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorUtils
{
    
    

    /**
     * rgb are float values from 0 to 1
     */
    private static Color NormalizeRGB(float[] rgb)
    {
        
        float min = Mathf.Min(rgb);
        float max = Mathf.Max(rgb);

        if (max == min)
        {
            return Color.white;
        }

        float r = rgb[0];
        float g = rgb[1];
        float b = rgb[2];

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
        return c;
    }

    public static Color AverageColors(Dictionary<Color, int> amounts)
    {
        float r = 0;
        float g = 0;
        float b = 0;
        int totalColors = 0;
        foreach (KeyValuePair<Color, int> pair in amounts)
        {
            r += pair.Key.r * pair.Value;
            g += pair.Key.g * pair.Value;
            b += pair.Key.b * pair.Value;
            totalColors += pair.Value;
        }
        
        if (totalColors == 0)
        {
            return Color.white;
        }

        r /= totalColors;
        g /= totalColors;
        b /= totalColors;

        Color c = ColorUtils.NormalizeRGB(new[] {r, g, b});
        return c;
        
    }
    
    
    
}
