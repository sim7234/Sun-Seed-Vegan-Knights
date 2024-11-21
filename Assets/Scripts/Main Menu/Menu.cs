using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Button playButton;
    Camera camera;
    

    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject backToMenu;
    private void Start()
    {
        camera = Camera.main;
    }

    public void pressedPlay()
    {
        //loads Hub scene
        SceneManager.LoadScene(0);
    }

    public void pressedSelectPlayers()
    {
        camera.transform.position = new Vector3(0, 8.5f, -10);
        changeButtonState();
        backToMenu.SetActive(true);
    }

    public void pressedQuit()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }


    public void pressedTutorial()
    {
        SceneManager.LoadScene(4);
    }

    public void pressedBackToMenu()
    {
        camera.transform.position = new Vector3(0,0,-10);
        backToMenu.SetActive(false);
        changeButtonState();
    }

    void changeButtonState()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }

}
