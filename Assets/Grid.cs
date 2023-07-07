using System;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public Dictionary<Vector2Int, int> gridMap { get; private set; }
    [SerializeField] Vector2Int size = new Vector2Int(1,1);

    public void Start()
    {
        if (size.x < 1 || size.y < 1)
        {
            throw new ArgumentException($"size {size} : cannot be smaller than 1)");
        }
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        gridMap = new Dictionary<Vector2Int, int>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                gridMap.Add(coord, 0);
            }
        }
    }

    public Vector2Int worldPosToGridPos(Vector3 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }
    
    public int GetNodeFromWorldPos(Vector3 pos)
    {
        return GetNode(worldPosToGridPos(pos));
    }

    public int GetNode(int x, int y)
    {
        return GetNode(new Vector2Int(x, y));
    }
    
    public int GetNode(Vector2Int coords)
    {
        return gridMap.ContainsKey(coords) ? gridMap[coords] : -1;
    }
    
}
