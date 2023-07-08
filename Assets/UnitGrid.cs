using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    private GameGrid _gameGrid;
    [SerializeField] MapSetup _mapSetup;
    [SerializeField] GameObject backGrTemplate;
    [SerializeField] GameObject unitTemplate;
    [SerializeField] Vector2Int size = new Vector2Int(1,1);

    // Start is called before the first frame update
    public void Start()
    {
        if (size.x < 1 || size.y < 1)
        {
            throw new ArgumentException($"size {size} : cannot be smaller than 1)");
        }
        _gameGrid = new GameGrid(size);
        var backgrObj = new GameObject();
        backgrObj.transform.parent = transform;
        backgrObj.transform.localPosition = Vector3.zero;
        for (int x = 0; x < _gameGrid.Size.x; x++)
        {
            for (int y = 0; y < _gameGrid.Size.y; y++)
            {
                var node = _gameGrid.GetNode(new Vector2Int(x, y));
                var obj = Instantiate(backGrTemplate);
                obj.transform.parent = backgrObj.transform;
                obj.transform.localPosition = new Vector3(x, y, 1);
                node.renderer = obj.GetComponent<SpriteRenderer>();
                node.renderer.enabled = false;
            }
        }

        foreach (var setupData in _mapSetup.Positions)
        {
            var unit = Instantiate(unitTemplate);
            unit.transform.parent = transform;
            unit.transform.localPosition = new Vector3(setupData.position.x, setupData.position.y, 0);
            unit.GetComponent<Unit>().setUnitData(setupData.unitData);
        }
        
    }
    public GridNode GetNodeFromWorldPos(Vector3 pos)
    {
        return _gameGrid.GetNodeFromWorldPos(pos);
    }

    public GridNode GetNode(int x, int y)
    {
        return _gameGrid.GetNode(x, y);
    }
    
    public GridNode GetNode(Vector2Int coords)
    {
        return _gameGrid.GetNode(coords);
    }

    public void setAttackSelection(Vector2Int pos, bool enabled)
    {
        var node = _gameGrid.GetNode(pos);
        _gameGrid.DisableAllRenderers();
        if (node != null)
        {
            node.renderer.enabled = enabled;
        }
    }
}
