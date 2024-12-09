using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mapPices = new List<GameObject>();

    private int completedMissions;

    [SerializeField]
    private GameObject missionFlag;

    [SerializeField]
    private List<GameObject> flagPossitions = new List<GameObject>();
    private void Start()
    {
        MapUpdateByLinarSystem();
    }
    public void AddMapPices(int mapIndex)
    {
        completedMissions++;
        mapPices[mapIndex].SetActive(true);
    }

    public void MapUpdateByLinarSystem()
    {
        completedMissions = SaveData.Instance.completedMission;
        MoveMissionFlag();
        for (int i = 0; i < completedMissions; i++)
        {
            mapPices[i].SetActive(true);
        }
    }

    public void MoveMissionFlag()
    {
        missionFlag.transform.position = flagPossitions[completedMissions].transform.position;
    }


}