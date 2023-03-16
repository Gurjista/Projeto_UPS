using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePoint : Construction
{
        [SerializeField] private List<Building> _buildings;

    public override BuildType BuildType => BuildType.Ponto_De_Referencia;

    public override void GetConstructionList(){

    }
}
