using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{

    private class ColorData
    {

        private Color color;
        private Dictionary<Color, int> ratios;
        
        public ColorData(Color color, Dictionary<Color, int> ratios)
        {
            this.color = color;
            this.ratios = ratios;
        }
    }

    private List<ColorData> colorDatas;

    public LevelInfo()
    {
        colorDatas = new List<ColorData>();
    }

    public void CompleteColor(Color color, Dictionary<Color, int> ratios)
    {
        colorDatas.Add(new ColorData(color, ratios));
    }

    public int NumColorsTotal()
    {
        return this.colorDatas.Count;
    }

}
