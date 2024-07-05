using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button resume;
    public Button quit;
    PlayerControls controls;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        ui.style.display = DisplayStyle.None;
    }

    private void Start()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().controls;

        resume = ui.Q<Button>("ResumeButton");
        resume.clicked += OnResumeButtonClicked;

        quit = ui.Q<Button>("QuitButton");
        quit.clicked += OnQuitButtonClicked;


        controls.Grounded.Pause.performed += ctx =>
        {
            ui.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
            AudioListener.pause = true;
            controls.Grounded.Disable();
            controls.UI.Enable();
        };
        controls.UI.Resume.performed += ctx =>
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            controls.Grounded.Enable();
            controls.UI.Disable();
            ui.style.display = DisplayStyle.None;
        };
    }

    private void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        controls.Grounded.Enable();
        controls.UI.Disable();
        ui.style.display = DisplayStyle.None;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
