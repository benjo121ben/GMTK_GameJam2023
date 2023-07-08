using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private SpriteRenderer _renderer;
    [SerializeField]private UnitData data;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (data != null)
        {
            _renderer.sprite = data.sprite;
        }
    }

    public void setUnitData(UnitData data)
    {
        this.data = Instantiate(data);
        if (_renderer != null)
        {
            _renderer.sprite = this.data.sprite;
        }
    }
    
    public void move(Vector2 pos)
    {
        Debug.Log("moveCalled");
        transform.localPosition = new Vector3(pos.x, pos.y, transform.localPosition.z);
    }
}
