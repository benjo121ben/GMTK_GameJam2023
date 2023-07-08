using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "mapSetup", menuName = "ScriptableObjects/mapSetup", order = 1)]
public class MapSetup : ScriptableObject
{
    public Vector2Int size = new Vector2Int(1,1);
    public List<UnitSetup> Positions = new List<UnitSetup>();
}
