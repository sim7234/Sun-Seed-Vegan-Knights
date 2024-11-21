using UnityEngine;

public class PathfindingDebug : MonoBehaviour
{
    FindTargets findTargetScript;
    private void Start()
    {
        findTargetScript = GetComponent<FindTargets>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            findTargetScript.GetTargets();
        }
    }
}
