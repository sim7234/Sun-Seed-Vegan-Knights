using UnityEngine;

public class FindTargets : MonoBehaviour
{
    Pathfinding pathfinding;
    isTarget[] targets = new isTarget[3];

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
        targets = FindObjectsOfType<isTarget>();

        foreach (isTarget target in targets)
        {
            if(target.gameObject != null)
            {
                pathfinding.totalTargets++;
                pathfinding.target.Add(target.gameObject);
            }
        }
    }

    //for bug testing.
    public void GetTargets()
    {
        pathfinding.target.Clear();
        pathfinding.totalTargets = 0;

        foreach (isTarget target in targets)
        {
            pathfinding.totalTargets++;
            pathfinding.target.Add(target.gameObject);
        }
        Debug.Log("GetTargets");
    }
}
