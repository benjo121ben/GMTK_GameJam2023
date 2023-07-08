using System;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    public enum Axis
    {
        X, Y, Z
    }

    public Axis axis = Axis.X;
    [SerializeField] Camera _mainCam;
    UnitGrid grid;
    SpriteRenderer _srenderer;
    private bool ortographic = false;

    private void Start()
    {
        _srenderer = GetComponent<SpriteRenderer>();
        grid = GetComponentInParent<UnitGrid>();
        if (_mainCam == null)
        {
            throw new Exception($"{gameObject.name} does not have a camera assigned in UpdatePositionOnPlane");
        }

        if (_mainCam.orthographic)
        {
            ortographic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = getPosOnPlane();
        var node = grid.GetNodeFromWorldPos(pos);
        if (node != null)
        {
            var gridPos = GameGrid.FloorPos(pos);
            transform.position = new Vector3(gridPos.x + 0.5f, gridPos.y + 0.5f, pos.z);
            _srenderer.enabled = true;
            
        }
        else
        {
            _srenderer.enabled = false;
        }
    }

    //deprecated, will be refactored eventually
    Vector3 getPosOnPlane()
    {
        if (!ortographic)
        {
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            switch (axis)
            {
                case Axis.X:
                {
                    float t = (transform.position.x - ray.origin.x) / ray.direction.x;
                    Vector3 pos = ray.GetPoint(t);
                    pos.x = transform.position.x;
                    return pos;
                }
                case Axis.Y:
                {
                    float t = (transform.position.y - ray.origin.y) / ray.direction.y;
                    Vector3 pos = ray.GetPoint(t);
                    pos.y = transform.position.y;
                    return pos;
                }
                case Axis.Z:
                {
                    float t = (transform.position.z - ray.origin.z) / ray.direction.z;
                    Vector3 pos = ray.GetPoint(t);
                    pos.z = transform.position.z;
                    return pos;
                }
            }
        }
        else
        {
            var worldPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            switch (axis)
            {
                case Axis.X:
                {
                    worldPos.x = transform.position.x;
                    return worldPos;
                }
                case Axis.Y:
                {
                    worldPos.y = transform.position.y;
                    return worldPos;
                }
                case Axis.Z:
                {
                    worldPos.z = transform.position.z;
                    return worldPos;
                }
            }
        }

        throw new Exception("can never arrive at end of switch");
    }
}
