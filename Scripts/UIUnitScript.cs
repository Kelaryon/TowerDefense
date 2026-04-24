using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIUnitScript : MonoBehaviour
{
    [SerializeField] List<Button> buttonList;
    //[SerializeField] List<TooltipTrigger> tooltipList;

    public void ButtonReset()
    {
        foreach(Button btn in buttonList)
        {
            btn.onClick.RemoveAllListeners();
            btn.gameObject.SetActive(false);
        }
    }
    public void AddActionList(UnityAction[] actionList)
    {
        for(int i =0; i < actionList.Length; i++)
        {
            UnityAction act = actionList[i];

            if(act != null)
            {
                Button btn = buttonList[i];
                btn.gameObject.SetActive(true);
                btn.onClick.AddListener(act);
                btn.GetComponentInChildren<TextMeshProUGUI>().SetText("Hello");
            }
        }
    }
    public void AddActionList(MethodsInfo[] actionList)
    {
        if (actionList == null) {
            Debug.Log("It's null");
            return;
        }
        for (int i = 0; i < actionList.Length; i++)
        {
            //UnityAction action = actionList[i].method;
            if (actionList[i] != null)
            {
                Button btn = buttonList[i];
                btn.gameObject.SetActive(true);
                btn.onClick.AddListener(actionList[i].method);
                //btn.GetComponentInChildren<TextMeshProUGUI>().SetText("Hello");
                btn.GetComponent<TooltipTrigger>().SetText(actionList[i].header, actionList[i].content);
                if (actionList[i].isInteracteble == false)
                {
                    btn.interactable = false;
                }
            }
        }
    }

    public void ResetActionList()
    {

    }
}
