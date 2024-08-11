using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace WGJ2024GameAssets.UI.MainMenu
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Scenes")]
        public string menuSceneName = "Scenes/MainMenu";

        [Header("Elements Selectors")]
        public string pauseWindowSelector = "PauseWindow";
        public string volumeSliderSelector = "VolumeSlider";
        public string resumeButtonSelector = "ResumeButton";
        public string exitButtonSelector = "ExitButton";

        [CanBeNull] private UIDocument _uiDocument;

        [Header("Events")]
        [CanBeNull] public AudioSource audioSource;

        public static bool IsPaused => Time.timeScale <= 0.0001f;

        [CanBeNull] private Slider _volumeSlider;
        [CanBeNull] private Button _resumeButton;
        [CanBeNull] private Button _exitButton;
        [CanBeNull] private VisualElement _pauseWindow;
        

        public void Start()
        {
            try
            {
                _uiDocument = GetComponent<UIDocument>();
            }
            catch (NullReferenceException)
            {
                Debug.LogError("Error: UIDocument not found in PauseMenu. Skipping button setup.");
                return;
            }

            _volumeSlider = _uiDocument?.rootVisualElement.Q<Slider>(volumeSliderSelector);
            if (_volumeSlider != null)
            {
                if (audioSource)
                {
                    _volumeSlider.value = audioSource.volume * (_volumeSlider.highValue - _volumeSlider.lowValue) + _volumeSlider.lowValue;
                }
                _volumeSlider.RegisterValueChangedCallback(evt =>
                {
                    if (!audioSource) return;

                    float volume = (evt.newValue - _volumeSlider.lowValue) / (_volumeSlider.highValue - _volumeSlider.lowValue);
                    audioSource.volume = volume;
                });
            }
            else
            {
                Debug.LogWarning("Error: VolumeSlider not found in PauseMenu. Volume slider not setup.");
            }

            _resumeButton = _uiDocument?.rootVisualElement.Q<Button>(resumeButtonSelector);
            if (_resumeButton != null)
            {
                _resumeButton.clicked += () => ResumeGame();
            }
            else
            {
                Debug.LogWarning("Error: ResumeButton not found in PauseMenu. Resume button not setup.");
            }

            _exitButton = _uiDocument?.rootVisualElement.Q<Button>(exitButtonSelector);
            if (_exitButton != null)
            {
                _exitButton.clicked += () => ExitToMenu();
            }
            else
            {
                Debug.LogWarning("Error: ExitButton not found in PauseMenu. Exit button not setup.");
            }

            _pauseWindow = _uiDocument?.rootVisualElement.Q<VisualElement>(pauseWindowSelector);
            if (_pauseWindow != null)
            {
                _pauseWindow.style.display = DisplayStyle.None;
            }
            else
            {
                Debug.LogWarning("Error: PauseWindow not found in PauseMenu. Pause window not setup.");
            }
        }

        private void Update()
        {
            switch (IsPaused)
            {
                case true when Input.GetKeyUp(KeyCode.Escape):
                    ResumeGame();
                    break;
                case false when Input.GetKeyUp(KeyCode.Escape):
                    PauseGame();
                    break;
            }
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            if (audioSource) audioSource.UnPause();

            if (_pauseWindow != null) _pauseWindow.style.display = DisplayStyle.None;
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            if (audioSource) audioSource.Pause();

            if (_pauseWindow != null)
            {
                _pauseWindow.style.display = DisplayStyle.Flex;
            }
        }

        private void ExitToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(menuSceneName);
        }
    }
}