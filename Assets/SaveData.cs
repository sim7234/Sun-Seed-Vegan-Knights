using UnityEngine;
using UnityEngine.InputSystem;
public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }
    public int playerAmount;

    public WeaponType seedType;
    public WeaponType seedType2;
    public WeaponType seedType3;
    public WeaponType seedType4;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}