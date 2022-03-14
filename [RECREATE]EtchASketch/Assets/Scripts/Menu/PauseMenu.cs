using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MainMenu
{
    public void ResumeGame()
    {   
        // If the game is paused, resume the game
    }

    public void PauseGame()
    {
        // If game is not already pause, pause the game
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*
        In the future, don't reload the scene. Instead have it so that when players shake the screen
        or press the reset button, the pixels turn back to the original canvas color.
        Basically simulate the Etch A Sketch feel.
        */
    }
    
}
