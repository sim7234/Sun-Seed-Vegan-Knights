using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    GameObject[] targets = new GameObject[2];

    NavMeshAgent agent;

    bool targetExsists;

    enum TargetNames {Player, Objective};
    int arraySelectedPos = 0;

    int target = 0;


    // Start is called before the first frame update
    void Start()
    {

        if (checkIfTarget(TargetNames.Player) == true)
        {
            targets[arraySelectedPos] = GameObject.FindWithTag("Player");
            arraySelectedPos++;
        }

        if (checkIfTarget(TargetNames.Objective) == true)
        {
            targets[arraySelectedPos] = GameObject.FindWithTag("Objective");
            arraySelectedPos++;
        }


        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    /// <summary>
    /// If selected enum TargetNames is null return false.
    /// </summary>
    /// <param name="selectedEnum"></param>
    /// <returns></returns>
    bool checkIfTarget(TargetNames selectedEnum)
    {
        if (selectedEnum == TargetNames.Player)
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        if (selectedEnum == TargetNames.Objective)
        {
            if (GameObject.FindWithTag("Objective") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targets[0] != null && targets[1] != null)
        {

            float distenceToPlayer;
            float distenceToObjective;

            Vector2 playerDistence = targets[0].transform.position - transform.position;
            Vector2 objectiveDistence = targets[1].transform.position - transform.position;

            distenceToPlayer = Vector2.Distance(playerDistence, transform.position);
            distenceToObjective = Vector2.Distance(objectiveDistence, transform.position);

            if (distenceToPlayer > (distenceToObjective * 2))
            {
                target = 1;
            }
            else
            {
                target = 0;
            }


            
            
            
        }

        agent.SetDestination(targets[target].transform.position);
        Debug.Log(targets[target].transform.position.ToString());
    }
}
