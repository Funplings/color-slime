using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private int numColorsToPass;
    public int NumColorsToPass
    {
        get => numColorsToPass;
        set => numColorsToPass = value;
    }
    
    [SerializeField] private Canvas timeUpScreen;
    [SerializeField] private float timeUpScreenDelay = 1f;
    private LevelInfo currLevelInfo;
    private LevelCompleteScreen levelCompleteScreen;

    void Awake()
    {
        currLevelInfo = new LevelInfo();
        timeUpScreen.enabled = false;
        levelCompleteScreen = FindObjectOfType<LevelCompleteScreen>();
    }

    public void TimeUp()
    {
        StartCoroutine(TimeUpNextScreen());
    }

    IEnumerator TimeUpNextScreen()
    {
        timeUpScreen.enabled = true;
        yield return new WaitForSeconds(timeUpScreenDelay);
        timeUpScreen.enabled = false;
        bool passed = currLevelInfo.NumColorsTotal() >= numColorsToPass;
        levelCompleteScreen.ShowScreen(passed, currLevelInfo);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void AddColorCompleted(Color color, Dictionary<Color, int> ratios)
    {
        currLevelInfo.CompleteColor(color, ratios);
    }
    
}
