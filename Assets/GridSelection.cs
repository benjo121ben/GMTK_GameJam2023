using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelection : MonoBehaviour
{
    Grid unitGrid;
    [SerializeField] Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        unitGrid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var point = ray.GetPoint((unitGrid.transform.position.z - ray.origin.z) / ray.direction.z);
            Debug.Log(unitGrid.GetNodeFromWorldPos(point));
        }
    }
}
