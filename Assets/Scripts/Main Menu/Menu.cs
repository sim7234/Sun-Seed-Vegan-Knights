using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Button playButton;
    

    [SerializeField] GameObject[] buttons;

    public void pressedPlay()
    {
        //loads Hub scene
        SceneManager.LoadScene("Tutorial");
    }

    public void pressedQuit()
    {
            Application.Quit();    
    }

    public void pressedTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void changeButtonState()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }

}
