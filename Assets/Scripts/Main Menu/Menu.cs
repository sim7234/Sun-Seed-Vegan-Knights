using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Button playButton;
    Camera camera;
    

    [SerializeField] GameObject[] buttons;

    public void pressedPlay()
    {
        //loads Hub scene
        SceneManager.LoadScene(0);
    }

    public void pressedQuit()
    {
            Application.Quit();    
    }

    public void pressedTutorial()
    {
        SceneManager.LoadScene(4);
    }

    void changeButtonState()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }

}
