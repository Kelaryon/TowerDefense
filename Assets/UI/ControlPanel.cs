using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanel : MonoBehaviour
{
    //Structure selectedTower;
    ISelectableInterface selectedTower;
    //public TMP_Dropdown towerDropdown;
    public DetailPanelScript detailPanel;
    public UIUnitScript unitPanel;
    public GameObject buildPanel;
    SelectedInfo selectedInfo;
    public void Select(Dictionary<string, string> info, Sprite icon)
    {
        detailPanel.SetDetails(info, icon);
    }
    public void Select(Dictionary<string, string> info)
    {
        detailPanel.SetDetails(info);
    }
    public void Select(SelectedInfo info)
    {
        //Debug.Log(selectedInfo);
        detailPanel.SetDetails(info.detailList,info.icon);
        detailPanel.gameObject.SetActive(true);
        unitPanel.AddActionList(info.methodList);
    }
    public void Deselect()
    {
        //if (selectedTower != null)
        //{
        //    selectedTower.ActivateRangeCircle(false);
        //}
        if (selectedTower != null)
        {
            selectedTower.Deactivate();
        }
        selectedTower = null;
        detailPanel.gameObject.SetActive(false);
        ShowPannel(false);
        //towerDropdown.ClearOptions();
    }
    //public void ObjectSelect(Tower tower)
    //{
    //    if(selectedTower != null)
    //    {
    //        selectedTower.ActivateRangeCircle(false);
    //    }
    //    selectedTower = tower;
    //    selectedTower.ActivateRangeCircle(true);
    //    selectedInfo = tower.GetInfo();
    //    Select(selectedInfo);
    //    ShowPannel(true);
    //}
    //public void ObjectSelect(Structure structure)
    //{
    //    selectedTower = structure;
    //    selectedInfo = structure.GetInfo();
    //    Select(selectedInfo);
    //    ShowPannel(true);
    //}
    //public void ObjectSelect(Waypoint waypoint)
    //{
    //    if (selectedTower != null)
    //    {
    //        selectedTower.ActivateRangeCircle(false);
    //        selectedTower = null;
    //    }
    //    Select(waypoint.GetInfo());
    //    ShowPannel(false);
    //}
    private void ShowPannel(bool ver)
    {
        unitPanel.gameObject.SetActive(ver);
        buildPanel.SetActive(!ver);
    }

    //Major Modifications

    public void ObjectSelect(ISelectableInterface selected)
    {
        if(selectedTower != null)
        {
            selectedTower.Deactivate();
        }
        unitPanel.ButtonReset();
        selectedTower = selected;
        selectedTower.Activate();
        selectedInfo = selectedTower.GetInfo();
        Select(selectedInfo);
        if (selectedInfo.methodList == null)
        {
            ShowPannel(false);
        }
        else
        {
            ShowPannel(true);
        }
    }
    public void ReloadInterface()
    {
        unitPanel.ButtonReset();
        selectedInfo = selectedTower.GetInfo();
        Select(selectedInfo);
    }
}
