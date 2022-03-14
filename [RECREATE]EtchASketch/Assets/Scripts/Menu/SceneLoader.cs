using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        if(sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            //Play scene change SFX
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.Log("Scene does not exist");
        }
        
    }

    public void LoadScene(string sceneName)
    {
        //Play scene change SFX
        SceneManager.LoadScene(sceneName);        
    }
}
