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
    public void AddPlayer(GameObject player, int index)
    {
        switch (index)
        {
            case 0:
                player1 = player;
                break;
            case 1:
                player2 = player;
                break;
            case 2:
                player3 = player;
                break;
            case 3:
                player4 = player;
                break;
        }
        DontDestroyOnLoad(player);
    }

}