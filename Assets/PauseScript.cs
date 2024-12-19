using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; 
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private Toggle filterToggle; 
    [SerializeField] private Slider volumeSlider;
    private PlayerInputManager playerInputManager;

    private bool isPaused = false;

    private void Start()
    {
        //pauseMenuUI.SetActive(false);
        //settingsMenuUI.SetActive(false);

        if (SaveData.Instance != null && filterToggle != null)
        {
            filterToggle.isOn = SaveData.Instance.epelepticFilterOn;
            filterToggle.onValueChanged.AddListener(OnFilterToggle);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume; 
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChange);
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0f;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuUI.GetComponentInChildren<Selectable>().gameObject);

        SwitchActionMap("PauseMenu");
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;

        SwitchActionMap("ControlActions1");
    }

    private void SwitchActionMap(string actionMap)
    {
        PlayerInput[] players = FindObjectsOfType<PlayerInput>();
        foreach (PlayerInput player in players)
        {
            player.SwitchCurrentActionMap(actionMap);
        }
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsMenuUI.GetComponentInChildren<Selectable>().gameObject);
    }

    public void CloseSettings()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuUI.GetComponentInChildren<Selectable>().gameObject);
    }
    public void OnFilterToggle(bool isOn)
    {
        if (SaveData.Instance != null)
        {
            SaveData.Instance.epelepticFilterOn = isOn;
        }
    }

    public void OnVolumeSliderChange(float volume)
    {
        AudioListener.volume = volume; 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}