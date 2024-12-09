using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject[] enemyTypes;

    public int basicAmount;
    public int köttbulleAmount;
    public int rangedAmount;
    public int dashAmount;

    public float spawnTime;

    public bool starSpawning;
    bool allEnemiesSpawend;

    int totalEnemies;
    float spawnDelay;

    Camera camera;
    float cameraWidth;
    float cameraHeight;

    [SerializeField] GameObject EmptyTransform;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        cameraHeight = camera.orthographicSize * 2;  
        cameraWidth = cameraHeight * camera.aspect;
        

        totalEnemies = basicAmount + köttbulleAmount + rangedAmount + dashAmount;
        if (totalEnemies > spawnTime)
        {
            spawnDelay = spawnTime + 0.0f / totalEnemies;
        }
        else
        {
            spawnDelay = totalEnemies / spawnTime + 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (starSpawning == true)
        {
            for (int i = 0; i < basicAmount; i++)
            {
                GetRandomSpawn();
                Instantiate(enemyTypes[0], EmptyTransform.transform.position, Quaternion.identity);
            }




        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GetRandomSpawn();
        }

        Vector2 distenceToCamera = camera.WorldToViewportPoint(EmptyTransform.transform.position);
        Debug.Log(distenceToCamera);

    }


    void GetRandomSpawn()
    {
        int zeroOrOne = Random.Range(0, 2);
        Vector2 cameraPosition = camera.GetComponentInParent<Transform>().position;

        //TODO: enemies spawn on whole screen after camera moves, this is because it goes between very large values based on parents transform.
        float randomY = Random.Range(-cameraHeight - camera.orthographicSize, cameraHeight + camera.orthographicSize);
        Debug.Log(randomY);
        

        if (camera.transform.position.y <= 0)
        {
            EmptyTransform.transform.position = new Vector2(cameraPosition.x, (cameraPosition.y + randomY));
        }
        else
        {
            EmptyTransform.transform.position = new Vector2(cameraPosition.x, (cameraPosition.y - randomY));
        }

        Vector2 distenceToCamera = camera.WorldToViewportPoint(EmptyTransform.transform.position);

        //checks if EmptyTransforms location Y value is close to camera center.
        if (distenceToCamera.y < -0.5f || distenceToCamera.y > 1.5f)
        {
            float randomX = Random.Range(-cameraWidth, cameraWidth);

            if (camera.transform.position.x <= 0)
            {
                EmptyTransform.transform.position = new Vector2(cameraPosition.x + randomX, cameraPosition.y + randomY);
            }
            else
            {
                EmptyTransform.transform.position = new Vector2(cameraPosition.x - randomX, cameraPosition.y - randomY);
            }         
        }
        else
        {  
            zeroOrOne = Random.Range(0, 2);

            if (zeroOrOne == 0)
            {
                EmptyTransform.transform.position = new Vector2(cameraPosition.x -cameraWidth - 5, randomY);
            }
            else
            {
                EmptyTransform.transform.position = new Vector2(cameraPosition.x + cameraWidth + 5, randomY);
            }
        } 
    }
}
