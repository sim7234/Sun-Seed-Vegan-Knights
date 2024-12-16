using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGoal : MonoBehaviour
{
    private float delay = 2f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            //Destroy(player);
        }

        SceneManager.LoadScene("True Hub");
    }
}
