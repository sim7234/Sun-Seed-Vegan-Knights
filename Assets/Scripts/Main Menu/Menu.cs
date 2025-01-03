using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Button playButton;

    [HideInInspector] public bool hasDoneTutorial = false;

    [SerializeField] GameObject[] buttons;

    void Start()
    {
        if (PlayerPrefs.GetInt("HasDoneTutorial") == 1)
        {
            hasDoneTutorial = true;
        }
        else
        {
            hasDoneTutorial = false;
        }
    }

    public void pressedPlay()
    {
        if (hasDoneTutorial == false)
        {
            PlayerPrefs.SetInt("HasDoneTutorial", 1);
            hasDoneTutorial = true;
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            //loads Hub scene
            SceneManager.LoadScene("True Hub");
        }
    }

    public void pressedQuit()
    {
            Application.Quit();    
    }

    public void pressedTutorial()
    {
        PlayerPrefs.SetInt("HasDoneTutorial", 1);
        hasDoneTutorial = true;
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
