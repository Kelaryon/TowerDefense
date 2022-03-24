using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPanelScript : MonoBehaviour
{
    Tower tower;
    // Start is called before the first frame update
     public void SetTower(Tower tower)
    {
        this.tower = tower;
    }
    public Tower GetTower()
    {
        return tower;
    }
    public void Restart()
    {
        tower = null;
    }

}
