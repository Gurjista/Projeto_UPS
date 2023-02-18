using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laboratory : Room
{
    [Header("informações laboratório")]
    [SerializeField] private string _laboratorySubject;
    public override BuildType BuildType => BuildType.Laboratorio;

    // Update is called once per frame
    void Update()
    {
        
    }
}
