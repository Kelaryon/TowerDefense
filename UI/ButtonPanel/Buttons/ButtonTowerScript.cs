using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTowerScript : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] HolographicTower hTower;
    Bank bankControl;
    private void Awake()
    {
        bankControl = FindObjectOfType<Bank>();
    }
    public void SelectTower()
    {
        bankControl.towerPrefab = tower;
        if (hTower != null)
        {
            bankControl.SetHTower(Instantiate(hTower, transform.position,Quaternion.identity));
        }
        //bankControl.SetHTower(Tower HoloTower = Instantiate(tower,this.transform));

    }
    //public void CreateTower(Vector3 position)
    ////{
    ////     tower.CreateTower(tower, position);
    ////}

}
