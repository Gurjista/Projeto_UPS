using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConstructionAtributtes : ScriptableObject {
    
    public string Name;
    public List<string> Nicknames;
    public Mesh BuildingModel;
    public Mesh outlineBuildingModel;
    public Transform contructionLocation;

}
