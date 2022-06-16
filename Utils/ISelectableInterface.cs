using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectableInterface 
{
    abstract SelectedInfo GetInfo();
}

public class SelectedInfo{
    public Dictionary<string, string> detailList;
    public Sprite icon;

    public SelectedInfo(Dictionary<string, string> detailList, Sprite icon)
    {
        this.detailList = detailList;
        this.icon = icon;
    }
}
