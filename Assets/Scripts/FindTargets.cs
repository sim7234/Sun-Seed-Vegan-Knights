using UnityEngine;

public class FindTargets : MonoBehaviour
{

    Pathfinding pathfinding;
    GameObject[] targets = new GameObject[3];

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
        targets = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject target in targets)
        {
            pathfinding.totalTargets++;
            pathfinding.target.Add(target);
        }
    }

    //for bug testing.
    public void GetTargets()
    {
        Debug.Log("GetTargets");
        targets = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject target in targets)
        {
            pathfinding.totalTargets++;
            pathfinding.target.Add(target);
        }
    }
}
