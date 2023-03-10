using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectRoom : Room
{
    [Header("informações projeto")]
    [SerializeField] private string _projectName;
    
    public override BuildType BuildType => BuildType.Sala_de_projeto;

    // Update is called once per frame
    void Update()
    {
        
    }
}
