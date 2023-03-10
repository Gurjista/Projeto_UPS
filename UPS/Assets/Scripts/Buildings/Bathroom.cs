using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : Room
{
    [SerializeField] private BathroomType tipoBanheiro;


    public override BuildType BuildType => BuildType.Banheiro;


    // Update is called once per frame
    void Update()
    {
        
    }

    
}

public enum BathroomType
{
    MASCULINO,
    FEMININO,
    OUTRO
}
