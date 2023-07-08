using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    enum selectionType{none, move, attack}

    private selectionType selected = selectionType.none;
    private Vector2Int selectedPos = new Vector2Int(-1, -1);
    private GameGrid _gameGrid;
    [SerializeField] MapSetup _mapSetup;
    [SerializeField] GameObject backGrTemplate;
    [SerializeField] GameObject unitTemplate;
    [SerializeField] Vector2Int size = new Vector2Int(1,1);
    [SerializeField] List<Unit> units = new List<Unit>();

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

        int unitIndex = 0;
        foreach (var setupData in _mapSetup.Positions)
        {
            var unitObj = Instantiate(unitTemplate);
            unitObj.transform.parent = transform;
            unitObj.transform.localPosition = new Vector3(setupData.position.x, setupData.position.y, 0);
            var unitComponent = unitObj.GetComponent<Unit>();
            unitComponent.setUnitData(setupData.unitData);
            _gameGrid.GetNode(setupData.position).value = unitIndex;
            units.Add(unitComponent);
            unitIndex++;
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

    public void setAttackSelection(Vector2Int pos)
    {
        var node = _gameGrid.GetNode(pos);
        _gameGrid.DisableAllRenderers();
        if (node != null)
        {
            if (selected == selectionType.none && node.value > -1)
            {
                node.renderer.enabled = true;
                selected = selectionType.attack;
                return;
            }
            else if(selected == selectionType.attack && node.value > -1)
            {
                //attack code
                Debug.Log("ATTACKED " + pos);
                selected = selectionType.none;
                return;
            }
        }
        selected = selectionType.none;
    }
    
    public void setMoveSelection(Vector2Int pos)
    {
        var node = _gameGrid.GetNode(pos);
        _gameGrid.DisableAllRenderers();
        if (node != null)
        {
            if (selected == selectionType.none && node.value > -1)
            {
                node.renderer.enabled = true;
                selectedPos = pos;
                selected = selectionType.move;
                return;
            }
            else if(selected == selectionType.move && node.value == -1)
            {
                //movement code
                var oldNode = _gameGrid.GetNode(selectedPos);
                node.value = oldNode.value;
                oldNode.value = -1;
                units[node.value].move(pos);
                selected = selectionType.none;
                return;
            }
        }
        selected = selectionType.none;
    }
}
