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

    private bool pauseCooldown = false;

    private const string FilterTogglePrefKey = "FilterToggle";
    private const string VolumePrefKey = "VolumeLevel";
    private void Start()
    {
        //pauseMenuUI.SetActive(false);
        //settingsMenuUI.SetActive(false);

        if (filterToggle != null)
        {
            filterToggle.isOn = PlayerPrefs.GetInt(FilterTogglePrefKey, 0) == 1;
            filterToggle.onValueChanged.AddListener(OnFilterToggle);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChange);
        }
    }
    public void PauseGame()
    {
        if (isPaused || pauseCooldown) return;

        isPaused = true;
        pauseCooldown = true;
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0f;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuUI.GetComponentInChildren<Selectable>().gameObject);

        SwitchActionMap("PauseMenu");

        StartCoroutine(PauseCooldownCoroutine());
    }

    private IEnumerator PauseCooldownCoroutine()
    {
        yield return new WaitForSeconds(0.5f); 
        pauseCooldown = false;
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
            PlayerPrefs.SetInt(FilterTogglePrefKey, isOn ? 1 : 0);
            PlayerPrefs.Save();
        }

    public void OnVolumeSliderChange(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}