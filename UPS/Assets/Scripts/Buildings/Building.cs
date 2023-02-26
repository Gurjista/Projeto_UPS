using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Construction
{
    private List<Room> _rooms;


    // Update is called once per frame
    void Update()
    {
        
    }

    public override BuildType BuildType => BuildType.Predio;

    public override void GetConstructionList(){
        Room[] auxRoomsList;
        auxRoomsList = GetComponentsInChildren<Room>();

        foreach (Room room in auxRoomsList)
        {
            _rooms.Add(room);
        }
    }
}
