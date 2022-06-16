using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanel : MonoBehaviour
{
    Tower selectedTower;
    public TMP_Dropdown towerDropdown;
    public DetailPanelScript detailPanel;
    public GameObject unitPanel;
    public GameObject buildPanel;
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
        detailPanel.SetDetails(info.detailList,info.icon);
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
    public void Deselect()
    {
        detailPanel.gameObject.SetActive(false);
        unitPanel.SetActive(false);
        buildPanel.SetActive(true);
        towerDropdown.ClearOptions();
    }
    public void SelectControl(string tag, GameObject obj)
    {
        switch (tag)
        {
            case "Tower":
                selectedTower = obj.GetComponent<Tower>();
                Select(selectedTower.GetInfo());
                buildPanel.SetActive(false);
                unitPanel.SetActive(true);
                //Possible Problem
                towerDropdown.ClearOptions();
                DropdownFill();
                break;
            default:
                ISelectableInterface selectable = obj.GetComponent<ISelectableInterface>();
                Select(selectable.GetInfo());
                unitPanel.gameObject.SetActive(false);
                buildPanel.gameObject.SetActive(true);
                break;
        }
        detailPanel.gameObject.SetActive(true);
    }
    public void TowerDestroyFunction()
    {
        Deselect();
        selectedTower.DestroyTower();
    }

    private void DropdownFill()
    {
        towerDropdown.AddOptions(selectedTower.GetStatusEffectList());
    }
    public void OnDropdownValueChange()
    {
        selectedTower.SetStatus(towerDropdown.value);
    }
 }
