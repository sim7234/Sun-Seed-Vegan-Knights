using UnityEngine;

public class NaturalWater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WaterSystem>() != null)
        {
            other.GetComponent<WaterSystem>().ChangeWaterRefillRate(0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<WaterSystem>() != null)
        {
            Debug.Log("Left water area");
            //0 resets to default value
            other.GetComponent<WaterSystem>().ChangeWaterRefillRate(0);
        }
    }
}
