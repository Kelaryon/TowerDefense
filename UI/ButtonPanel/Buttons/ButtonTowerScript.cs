using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTowerScript : MonoBehaviour
{
    [SerializeField] Tower tower;
    Bank bankControl;
    private void Awake()
    {
        bankControl = FindObjectOfType<Bank>();
    }
    public void SelectTower()
    {
        bankControl.towerPrefab = tower;
    }

    //public void CreateTower(Vector3 position)
    ////{
    ////     tower.CreateTower(tower, position);
    ////}

}
