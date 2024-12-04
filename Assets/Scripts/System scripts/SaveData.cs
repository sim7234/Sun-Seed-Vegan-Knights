using UnityEngine;
using UnityEngine.InputSystem;
public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }
    public int playerAmount;

    public int playerDeathsBeforeGameOver;

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


    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;

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
        Debug.Log("Addplayer");
        switch (index)
        {
            case 0:
                player1 = player;
                player.GetComponent<HPBar>().parent.transform.position = position1.transform.position;
                Debug.Log("Move to pos: " + position1);
                break;
            case 1:
                player2 = player;
                player.GetComponent<HPBar>().parent.transform.position = position2.transform.position;
                Debug.Log("Move to pos: " + position2);
                break;
            case 2:
                player3 = player;
                player.GetComponent<HPBar>().parent.transform.position = position3.transform.position;
                Debug.Log("Move to pos: " + position3);
                break; 
            case 3:
                player4 = player;
                player.GetComponent<HPBar>().parent.transform.position = position4.transform.position;
                Debug.Log("Move to pos: " + position4);
                break;
        }
        DontDestroyOnLoad(player);
    }
    public void FixHud(GameObject player, int index)
    {
        switch (index)
        {
            case 0:
   
                player.GetComponent<HPBar>().parent.transform.position = position1.transform.position;

                break;
            case 1:
     
                player.GetComponent<HPBar>().parent.transform.position = position2.transform.position;
           
                break;
            case 2:
          
                player.GetComponent<HPBar>().parent.transform.position = position3.transform.position;
    
                break;
            case 3:
              
                player.GetComponent<HPBar>().parent.transform.position = position4.transform.position;
           
                break;
        }
        DontDestroyOnLoad(player);
    }
}