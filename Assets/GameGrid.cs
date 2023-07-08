using System;
using System.Collections.Generic;
using UnityEngine;


public class GameGrid
{
    GameObject template;
    public Dictionary<Vector2Int, GridNode> gridMap { get; private set; }
    public Vector2Int Size;

    public GameGrid(Vector2Int size)
    {
        Size = size;
        gridMap = new Dictionary<Vector2Int, GridNode>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                var node = new GridNode();
                node.value = -1;
                gridMap.Add(coord, node);
            }
        }
    }

    public void Clear()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                gridMap[coord].value = -1;
            }
        }
    }
    
    public void DisableAllRenderers()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                gridMap[coord].renderer.enabled = false;
            }
        }
    }

    public static Vector2Int FloorPos(Vector3 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }
    
    public GridNode GetNodeFromWorldPos(Vector3 pos)
    {
        return GetNode(FloorPos(pos));
    }

    public GridNode GetNode(int x, int y)
    {
        return GetNode(new Vector2Int(x, y));
    }
    
    public GridNode GetNode(Vector2Int coords)
    {
        return gridMap.ContainsKey(coords) ? gridMap[coords] : null;
    }
    
}
