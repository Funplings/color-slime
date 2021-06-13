using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelCompleteScreen : MonoBehaviour
{
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Text numToPassText;
    [SerializeField] private RectTransform levelCompletePanel;
    private Button nextLevelButton;
    private Button retryButton;
    private LevelManager levelManager;

    void Start()
    {
        this.levelManager = FindObjectOfType<LevelManager>();
        levelCompletePanel.gameObject.SetActive(true);
        retryButton = GetComponentInChildren<RestartButton>().GetComponent<Button>();
        nextLevelButton = GetComponentInChildren<NextLevelButton>().GetComponent<Button>();
        levelCompletePanel.gameObject.SetActive(false);
    }

    public void ShowScreen(bool passed, LevelInfo levelInfo)
    {
        levelCompletePanel.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(passed);
        retryButton.gameObject.SetActive(!passed);
        scoreText.text = levelInfo.NumColorsTotal().ToString() + " colors made";
        numToPassText.text = levelManager.NumColorsToPass + " colors needed to pass";

    }

    public void NextLevel()
    {
        levelManager.NextLevel();
    }
    
}
