using UnityEngine;
using UnityEngine.InputSystem;
public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }

    public PlayerInformation player1;
    public PlayerInformation player2;
    public PlayerInformation player3;
    public PlayerInformation player4;

    public int playerAmount;

    public WeaponType seedType;

    public void spawnPlayers()
    {        
        if(playerAmount >= 1)
        {
            MultiplayerManager.Instance.SpawnPlayer(player1.prefab, player1.controlScheme, player1.actionMap, player1.playerName, player1.device);
        }
        if(playerAmount >= 2)
        {
            MultiplayerManager.Instance.SpawnPlayer(player2.prefab, player2.controlScheme, player2.actionMap, player2.playerName, player2.device);
        }
        if (playerAmount >= 3)
        {
            MultiplayerManager.Instance.SpawnPlayer(player3.prefab, player3.controlScheme, player3.actionMap, player3.playerName, player3.device);
        }
        if (playerAmount >= 4)
        {
            MultiplayerManager.Instance.SpawnPlayer(player4.prefab, player4.controlScheme, player4.actionMap, player4.playerName, player4.device);
        }
    }

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