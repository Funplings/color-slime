using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.Play(); // enter game scene function  
    }

    public void GoBackToMain()
    {
        GameManager.Instance.Main(); // go back to main menu if needed (ex: if you pressed a popup etc.)
    }

    public void Credits()
    {
        GameManager.Instance.Credits();  // to pop up credits (or to load scene with credits pop-up)
    }
}
