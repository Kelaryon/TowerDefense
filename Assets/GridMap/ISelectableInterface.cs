using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISelectableInterface 
{
    abstract SelectedInfo GetInfo();
    abstract ISelectableInterface GetSelected();
    abstract void Activate();
    abstract void Deactivate();
    abstract UnityAction[] GetActionList();
}

public class SelectedInfo{
    public Dictionary<string, string> detailList;
    public Sprite icon;
    public MethodsInfo[] methodList;

    public SelectedInfo(Dictionary<string, string> detailList, Sprite icon, MethodsInfo[] methodList)
    {
        this.detailList = detailList;
        this.icon = icon;
        this.methodList = methodList;
    }
    public SelectedInfo(Dictionary<string, string> detailList, Sprite icon)
    {
        this.detailList = detailList;
        this.icon = icon;
        methodList = null;
    }
}
public class MethodsInfo{
    public UnityAction method;
    public string header;
    public string content;
    public bool isInteracteble;
    public MethodsInfo(UnityAction method, string header, string content, bool isInteracteble)
    {
        this.method = method;
        this.header = header;
        this.content = content;
        this.isInteracteble = isInteracteble;
    }
    public MethodsInfo(UnityAction method)
    {
        this.method = method;
    }
}
