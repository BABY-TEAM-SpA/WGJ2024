using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [Header("Scenes")]
    public string gameSceneName = "Scenes/SampleScene";
    
    [Header("Windows")]
    public string creditsWindowName = "CreditsWindow";

    [Header("Buttons - Main Menu")]
    public string startButtonSelector = "StartButton";
    public string creditsButtonSelector = "CreditsButton";
    public string exitButtonSelector = "ExitButton";

    [Header("Buttons - Credits")]
    public string creditsBackButtonSelector = "CreditsBackButton";

    [CanBeNull] private Button _playButton;
    [CanBeNull] private Button _creditsButton;
    [CanBeNull] private Button _creditsBackButton;
    [CanBeNull] private Button _exitButton;
    [CanBeNull] private VisualElement _creditsWindow;

    void Awake()
    {
        UIDocument uiDocument;
        try
        {
             uiDocument = GetComponent<UIDocument>();
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Error: UIDocument not found in MainMenu. Skipping button setup.");
            return;
        }

        _playButton = uiDocument.rootVisualElement.Q<Button>(startButtonSelector);
        _creditsButton = uiDocument.rootVisualElement.Q<Button>(creditsButtonSelector);
        _creditsBackButton = uiDocument.rootVisualElement.Q<Button>(creditsBackButtonSelector);
        _exitButton = uiDocument.rootVisualElement.Q<Button>(exitButtonSelector);

        if (_playButton != null)
        {
            _playButton.clicked += StartGame;
        }
        else
        {
            Debug.LogWarning("Error: StartButton not found in MainMenu. Start button not setup.");
        }

        if (_creditsButton != null)
        {
            _creditsButton.clicked += ShowCredits;
        }
        else
        {
            Debug.LogWarning("Error: CreditsButton not found in MainMenu. Credits button not setup.");
        }

        if (_exitButton != null)
        {
            _exitButton.clicked += ExitGame;
        }
        else
        {
            Debug.LogWarning("Error: ExitButton not found in MainMenu. Exit button not setup.");
        }

        if (_creditsBackButton != null)
        {
            _creditsBackButton.clicked += BackToMainMenu;
        }
        else
        {
            Debug.LogWarning("Error: CreditsBackButton not found in MainMenu. Credits back button not setup.");
        }

        _creditsWindow = uiDocument.rootVisualElement.Q<VisualElement>(creditsWindowName);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void ShowCredits()
    {
        if (_creditsWindow != null)
        {
            _creditsWindow.style.display = DisplayStyle.Flex;
        }
    }

    private void BackToMainMenu()
    {
        if (_creditsWindow != null)
        {
            _creditsWindow.style.display = DisplayStyle.None;
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (_playButton != null)
        {
            _playButton.clicked -= StartGame;
        }

        if (_creditsButton != null)
        {
            _creditsButton.clicked -= ShowCredits;
        }

        if (_exitButton != null)
        {
            _exitButton.clicked -= ExitGame;
        }

        if (_creditsBackButton != null)
        {
            _creditsBackButton.clicked -= BackToMainMenu;
        }
    }
}
