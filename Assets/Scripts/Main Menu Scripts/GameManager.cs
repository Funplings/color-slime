using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    /* instantiate this whenever you load a new level,
     instantiated here for debugging */
    private LevelInfo currLevelInfo = new LevelInfo();
    public LevelInfo CurrLevelInfo
    {
        get => currLevelInfo;
        set => currLevelInfo = value;
    }

    #region Unity_functions
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions
    public void Main()
    {
        SceneManager.LoadScene("MainMenu"); //very specific names! make sure they match in unity :D
    }
    public void Play()
    {
        currLevelInfo = new LevelInfo();
        SceneManager.LoadScene("CHANGE"); //very specific names! make sure they match in unity :D
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits"); //very specific names! make sure they match in unity :D
    }
    #endregion

    #region button_functions
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
