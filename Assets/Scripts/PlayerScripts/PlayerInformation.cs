using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInformation : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefab;
    [HideInInspector]
    public string controlScheme;
    [HideInInspector]
    public string actionMap;
    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public InputDevice device;

    public void SetControllSceme(GameObject aPrefab, string aControlScheme, string aActionMap, string aPlayerName, InputDevice aDevice)
    {
        prefab =  aPrefab;
        controlScheme = aControlScheme;
        actionMap = aActionMap;
        playerName = aPlayerName;
        device = aDevice;
        Debug.Log("saved");
    }

}
