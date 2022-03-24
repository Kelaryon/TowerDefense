using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public UnitPanelScript unitPanel;
    public DetailPanelScript detailPanel;
    public BuildPanel BuildPanel;

    public void Select(Dictionary<string, string> info, Sprite icon)
    {
        detailPanel.SetDetails(info, icon);
    }
    public void Select(Dictionary<string, string> info)
    {
        detailPanel.SetDetails(info);
    }
    public void TowerControl(Tower tower)
    {
        unitPanel.SetTower(tower);
        BuildPanel.gameObject.SetActive(false);
        unitPanel.gameObject.SetActive(true);
    }
    public void NoNTowerControl()
    {
        unitPanel.gameObject.SetActive(false);
        BuildPanel.gameObject.SetActive(true);
        unitPanel.Restart();
    }
    public void DestroyTower()
    {
        unitPanel.GetTower().GetWaypoint().SetPlaceble();
        Destroy(unitPanel.GetTower().gameObject);
        NoNTowerControl();
        EnableDetailPanel(false);  
    }
    public void EnableDetailPanel(bool flip)
    {
        if (flip)
        {
            detailPanel.gameObject.SetActive(true);
        }
        else
        {
            detailPanel.gameObject.SetActive(false);
        }
    }
}
