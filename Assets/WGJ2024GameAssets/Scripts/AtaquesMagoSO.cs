using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackDirections
{
    Left,       //HO
    Mid,        //He
    Right,      //Ha
    Left_Mid,   //Slash
    Right_Mid,  //Slash
    Left_Right //Take
}

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/AtaquesMago", order = 1)]
public class AtaquesMagoSO : ScriptableObject
{
    public List<AttackDirections> attacks = new List<AttackDirections>();
}
