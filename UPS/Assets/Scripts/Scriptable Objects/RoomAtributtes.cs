using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnumRoomAtributtes
{
    SALADEAULA, LABORATORIO, SALADEPROFESSOR, SALADEPROJETO, SALADEADMINISTRACAO, BANHEIRO, LOCALDEESTUDO
}

[CreateAssetMenu(fileName = "RoomAtributtes", menuName = "UPS/RoomAtributtes", order = 0)]
public class RoomAtributtes : ConstructionAtributtes {
    public EnumRoomAtributtes RoomType;
}
