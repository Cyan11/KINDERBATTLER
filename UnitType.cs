using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitClass", menuName = "ScriptableObjects/UnitClass")]
public class UnitType : ScriptableObject
{
    public int Atk;
    public int Hp;
    public int Def;
    public int Spd;
    public int Sight;
    public string Type;
    public int TurnCost;
    public int MaintanceCost;
    public int SpawnCost;
    public Sprite sprite;
    public Sprite alt;


}
