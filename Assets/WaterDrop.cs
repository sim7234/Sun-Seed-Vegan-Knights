using DG.Tweening;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    int randomX;
    int randomY;


    // Start is called before the first frame update
    void Start()
    {
        randomX = 0;
        randomY = 0;
        do
        {
            if (randomX == 0)
            {
                randomX = Random.Range(-2, 3);
            }
            if (randomY == 0)
            {
                randomY = Random.Range(-2, 3);
            }

        } while (randomX == 0 || randomY == 0);

        DOTween.To(() => transform.position, x => transform.position = x,
            new Vector3(transform.position.x + randomX, transform.position.y + randomY, 0), 0.5f);
    }
    //TODO Effectors
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<WaterSystem>() == null)
                return;

            if (other.GetComponent<WaterSystem>().currentWater < other.GetComponent<WaterSystem>().maxWater)
            {
                other.GetComponent<WaterSystem>().currentWater += 10f;
                other.GetComponent<WaterSystem>().DisplayDropForTime();
            }

            Destroy(gameObject);
        }
    }

}
