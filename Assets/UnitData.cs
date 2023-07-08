using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unitData", menuName = "ScriptableObjects/UnitData", order = 0)]
public class UnitData : ScriptableObject
{
    public enum RangeType
    {
        radius, cube, line, curve
    }
    public int speed;
    public int initiative;
    public int range;
    public int damage;
    public int health;
    public RangeType rangeType;
    public Sprite sprite;
}
