using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    [SerializeField]
    private FocusLevel focusLevel;


    private float depthUpdateSpeed = 5.0f;

   
    private float angleUpdateSpeed = 7.0f;
 
    [SerializeField]
    private float positionUpdateSpeed = 5.0f;

    [HideInInspector]
    public float deapthMax = 12.0f;
    [HideInInspector]
    public float deapthMin = 6.0f;
    [HideInInspector]
    public float angleMax = 11.0f;
    [HideInInspector]
    public float angleMin = 11.0f;


    private float cameraEulerX;
    private Vector3 cameraPosition;

    
    [Space]
    [Header("Player info")]
    [HideInInspector] public int playerCount;

    private isTarget[] targets = new isTarget[3];

    public List<GameObject> players = new List<GameObject>();

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
        if (SaveData.playerAmount != playerCount)
        {
            playerCount = SaveData.playerAmount;
            FindTargets();
        }
        CalculateCameraLocation();
        MoveCamera();
    }
   
    
    public void FindTargets()
    {
        targets = FindObjectsOfType<isTarget>();

        foreach (isTarget target in targets)
        {
            if (target.gameObject != null)
            {
                if(target.isActiveAndEnabled)
                {
                    if (players.Contains(target.gameObject) == false)
                    {
                        players.Add(target.gameObject);
                    }
                }
            }
        }
    }

    public void AddTemporaryTarget(GameObject target)
    {
        players.Add(target);
    }
    public void RemoveTemporaryTarget(GameObject target)
    {
        players.Remove(target);
    }
    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != cameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(position.z, cameraPosition.z, depthUpdateSpeed * Time.deltaTime);
            gameObject.transform.position = targetPosition;
        }

        //Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
        //if (localEulerAngles.x != cameraEulerX)
        //{
        //    Vector3 targetEulerAngeles = new Vector3(cameraEulerX, localEulerAngles.y, localEulerAngles.z);
        //    gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngeles, angleUpdateSpeed * Time.deltaTime);
        //}
    }
    private void CalculateCameraLocation()
    {
        Vector3 avarageCeneter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                Vector3 playerPositons = players[i].transform.position;

                if (!focusLevel.focusBounds.Contains(playerPositons))
                {
                    float playerX = Mathf.Clamp(playerPositons.x, focusLevel.focusBounds.min.x, focusLevel.focusBounds.max.x);
                    float playerY = Mathf.Clamp(playerPositons.y, focusLevel.focusBounds.min.y, focusLevel.focusBounds.max.y);
                    float playerZ = Mathf.Clamp(playerPositons.z, focusLevel.focusBounds.min.z, focusLevel.focusBounds.max.z);
                    playerPositons = new Vector3(playerX, playerY, playerZ);
                }

                totalPositions += playerPositons;
                playerBounds.Encapsulate(playerPositons);
            }
            
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
