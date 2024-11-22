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

    [SerializeField]
    public bool epelepticFilterOn;


    public Vector2 position1;
    public Vector2 position2;
    public Vector2 position3;
    public Vector2 position4;

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
                player1.GetComponent<HPBar>().parent.transform.position = position1;
                break;
            case 1:
                player2 = player;
                player2.GetComponent<HPBar>().parent.transform.position = position1;
                break;
            case 2:
                player3 = player;
                player3.GetComponent<HPBar>().parent.transform.position = position1;
                break; 
            case 3:
                player4 = player;
                player4.GetComponent<HPBar>().parent.transform.position = position1;
                break;
        }
        DontDestroyOnLoad(player);
    }

}