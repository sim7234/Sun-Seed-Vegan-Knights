using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    Button playButton;

    public void pressedPlay()
    {
        //loads Game scene
        SceneManager.LoadScene(2);
    }

    public void pressedSelectPlayers()
    {
        Debug.Log("Load Select Player Scene");
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


}
