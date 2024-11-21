using System.Collections;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{

    [SerializeField] Transform spawnLocation;

    bool hasTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnLocation != null)
        {
            hasTransform = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasTransform)
            {
                other.transform.position = spawnLocation.position;
            }
            else
            {
                other.transform.position = new Vector2(0, 0);
            }
        }
    }
}
