using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [Header("Refrences")]
    [SerializeField] private GameObject[] buildingPrefabs;

    private int SelectedBuilding = 0;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetSelectedBuilding()
    {
        return buildingPrefabs[SelectedBuilding];
    }

}
