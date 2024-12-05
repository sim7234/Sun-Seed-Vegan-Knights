using UnityEngine;

public class NaturalWater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WaterSystem>() != null)
        {
            other.GetComponent<WaterSystem>().ChangeWaterRefillRate(0.5f);
            other.GetComponent<WaterSystem>().waterRefillTimer = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<WaterSystem>() != null)
        {
            //0 resets to default value
            other.GetComponent<WaterSystem>().ChangeWaterRefillRate(0);
        }
    }
}
