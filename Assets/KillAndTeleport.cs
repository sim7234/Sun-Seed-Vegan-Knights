using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAndTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bloodExplosion != null)
            {
                GameObject newExplosion = Instantiate(bloodExplosion, collision.transform.position, Quaternion.identity);
                Destroy(newExplosion, 1f);
            }
            collision.GetComponent<Health>().TakeDamage(1000);
            collision.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, collision.transform.position.z);
           
        }
    }
}
