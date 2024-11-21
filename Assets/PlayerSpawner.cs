using System.Collections;
using UnityEngine;

public class SpawnPlayerInCameraCenter : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab; 
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; 
        StartCoroutine(SpawnPlayerAfterDelay(5f)); 
    }

    private IEnumerator SpawnPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

        Vector3 cameraCenter = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, mainCamera.nearClipPlane));
        cameraCenter.z = 0f;
        Instantiate(playerPrefab, cameraCenter, Quaternion.identity);
    }
}