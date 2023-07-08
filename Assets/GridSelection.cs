using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelection : MonoBehaviour
{
    UnitGrid unitGrid;
    [SerializeField] Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        unitGrid = GetComponent<UnitGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var point = FloorPos(ray.GetPoint((unitGrid.transform.position.z - ray.origin.z) / ray.direction.z));
            unitGrid.setAttackSelection(point, true);
        }
    }
    
    public static Vector2Int FloorPos(Vector3 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }
}
