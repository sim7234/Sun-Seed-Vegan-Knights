using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetPoints : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> targetPoints = new List<GameObject>();

    public GameObject GetTargetPoint(int index)
    {
        return targetPoints[index];
    }
}