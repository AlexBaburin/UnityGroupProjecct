using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public void OnPlayClicked()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnSettingsClicked()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void OnGoBackClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
