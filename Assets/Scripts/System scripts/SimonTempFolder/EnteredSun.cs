using UnityEngine;

public class EnteredSun : MonoBehaviour
{
    [HideInInspector] public bool inSun;

    [SerializeField] GameObject inSunEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IsSun>() != null)
        {
            inSun = true;
            Debug.Log("In Sun");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IsSun>() != null)
        {
            inSun = false;
        }
    }

    private void Update()
    {
        if (inSun)
        {
            inSunEffect.SetActive(true);
        }
        else
        {
            inSunEffect.SetActive(false);
        }
    }

}


