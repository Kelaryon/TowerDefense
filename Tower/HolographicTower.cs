using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolographicTower : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,CellPosition(Utils.GetMouseWorldPoint()),0.05f);
    }
    Vector3 CellPosition(Vector3 poz)
    {
        poz.x = Mathf.RoundToInt(poz.x/10)*10;
        poz.z = Mathf.RoundToInt(poz.z/10)*10;
        poz.y = 0;
        return poz;
    }
}
