using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Construction
{
    [SerializeField] private List<Building> _buildings;

    public override void GetConstructionList(){
        Building[] auxBuildingsList;
        auxBuildingsList = GetComponentsInChildren<Building>();

        foreach (Building building in auxBuildingsList)
        {
            _buildings.Add(building);
        }
    }
}
