using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTowerScript : MonoBehaviour
{
    [SerializeField] Building building;
    [SerializeField] HolographicTower hTower;
    Controller bank;
    private void Awake()
    {
        if(bank == null)
        {
            bank = FindAnyObjectByType<Controller>();
        }
    }
    public void SelectTower()
    {
        bank.ChangeToBuildPhase(building,hTower);
    }
}
