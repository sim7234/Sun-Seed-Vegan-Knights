using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    [SerializeField]
    private FocusLevel focusLevel;

    [SerializeField]
    private float depthUpdateSpeed = 5.0f;

    [SerializeField]
    private float angleUpdateSpeed = 7.0f;
 
    [SerializeField]
    private float positionUpdateSpeed = 5.0f;


    public float deapthMax = 12.0f;
    public float deapthMin = 6.0f;

    public float angleMax = 11.0f;
    public float angleMin = 11.0f;


    private float cameraEulerX;
    private Vector3 cameraPosition;

    
    [Space]
    [Header("Player info")]
    private int playerCount;

    private isTarget[] targets = new isTarget[3];

    [SerializeField]
    private List<GameObject> players = new List<GameObject>();

    private void Start()
    {
        FindTargets();
        if(focusLevel != null)
        {
            players.Add(focusLevel.gameObject);
        }
        else
        {
            Debug.Log("Missing FocusLevel");
        }
    }
    private void LateUpdate()
    {
        if (SaveData.Instance.playerAmount != playerCount)
        {
            playerCount = SaveData.Instance.playerAmount;
            FindTargets();
        }
        CalculateCameraLocation();
        MoveCamera();
    }
   
    
    private void FindTargets()
    {
        targets = FindObjectsOfType<isTarget>();

        foreach (isTarget target in targets)
        {
            if (target.gameObject != null)
            {
                players.Add(target.gameObject);
            }
        }
    }

    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != cameraPosition)
        {

            Vector3 newPosition = Vector3.zero;
            newPosition.x = Mathf.MoveTowards(position.x, cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
            newPosition.y = Mathf.MoveTowards(position.y, cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
            newPosition.x = Mathf.MoveTowards(position.z, cameraPosition.z, depthUpdateSpeed * Time.deltaTime);
        }


    }
    private void CalculateCameraLocation()
    {
        Vector3 avarageCeneter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();

        for (int i = 0; i < players.Count; i++)
        {
            Vector3 playerPositons = players[i].transform.position;
            
            if(!focusLevel.focusBounds.Contains(playerPositons))
            {
                float playerX = Mathf.Clamp(playerPositons.x, focusLevel.focusBounds.min.x, focusLevel.focusBounds.max.x);
                float playerY = Mathf.Clamp(playerPositons.y, focusLevel.focusBounds.min.y, focusLevel.focusBounds.max.y);
                float playerZ = Mathf.Clamp(playerPositons.z, focusLevel.focusBounds.min.z, focusLevel.focusBounds.max.z);
                playerPositons = new Vector3(playerX, playerY, playerZ);
            }

            totalPositions += playerPositons;
            playerBounds.Encapsulate(playerPositons);
        }

        avarageCeneter = (totalPositions / players.Count);


        float extents = (playerBounds.extents.x + playerBounds.extents.y);
        float lerpPrecent = Mathf.InverseLerp(0, (focusLevel.halfXBounds + focusLevel.halfYBounds) / 2, extents);

        float depth = Mathf.Lerp(deapthMax, deapthMin, lerpPrecent);
        float angle = Mathf.Lerp(angleMax, angleMin, lerpPrecent);

        cameraEulerX = angle;
        cameraPosition = new Vector3(avarageCeneter.x, avarageCeneter.y, -10);
    }
}
