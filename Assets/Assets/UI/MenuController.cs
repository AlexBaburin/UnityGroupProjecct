using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button resume;
    public Button quit;
    public GameObject gameOverScreen;
    PlayerControls controls;
    HealthWithBlock health;
    float deathTimer = 0f;
    float timeUntilDeath = 1f;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        ui.style.display = DisplayStyle.None;
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthWithBlock>();
        gameOverScreen = GameObject.FindGameObjectWithTag("gameover");
        gameOverScreen.SetActive(false);
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
            if (health.health <= 0)
                return;
            ui.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
            AudioListener.pause = true;
            controls.Grounded.Disable();
            controls.UI.Enable();
        };
        controls.UI.Resume.performed += ctx =>
        {
            if (health.health <= 0)
                return;
            Time.timeScale = 1;
            AudioListener.pause = false;
            controls.Grounded.Enable();
            controls.UI.Disable();
            ui.style.display = DisplayStyle.None;
        };
    }

    private void Update()
    {
        if (health.health <= 0)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= timeUntilDeath)
            {
                gameOverScreen.SetActive(true);
                deathTimer = 0;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        controls.Grounded.Enable();
        controls.UI.Disable();
        ui.style.display = DisplayStyle.None;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

        AudioListener.pause = false;
    }

    private void OnQuitButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

        AudioListener.pause = false;
    }
}
