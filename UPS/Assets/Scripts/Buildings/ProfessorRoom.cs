using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorRoom : Room
{
    [Header("informações professor")]
    [SerializeField] private string _professorName;
    [SerializeField] private string _professorEmail;
    [SerializeField] private string _description;

    public override BuildType BuildType => BuildType.Sala_de_professor;

    // Update is called once per frame
    void Update()
    {
        
    }
}
