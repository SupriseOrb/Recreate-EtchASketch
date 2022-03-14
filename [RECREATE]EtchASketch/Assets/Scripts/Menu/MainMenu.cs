using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SceneLoader
{   
    protected virtual void Start()
    {
        //Load settings
        //Play music
    }
    
    public void OpenCanvas(Canvas canvas)
    {
        //Play open UI SFX
        canvas.enabled = true;
    }

    public void CloseCanvas(Canvas canvas)
    {
        //Play close UI SFX
        canvas.enabled = false;
    }

    public void Quit()
    {
        //Play quit game SFX
        Application.Quit();
    }

    
}