using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image objectImage;
    Bank Bank;
    public TextMeshProUGUI detailfield;


    private void Awake()
    {
        Bank = FindObjectOfType<Bank>();
    }
    public void SetDetails(Dictionary<string,string> info,Sprite image)
    {
        string combine = null;
        foreach(KeyValuePair<string,string> infoDetail in info)
        {
            combine += infoDetail.Key + ": " + infoDetail.Value + "\n";
        }
        objectImage.overrideSprite = image;
        detailfield.SetText(combine);
    }
    public void SetDetails(Dictionary<string, string> info)
    {
        string combine = null;
        foreach (KeyValuePair<string, string> infoDetail in info)
        {
            combine += infoDetail.Key + ": " + infoDetail.Value + "\n";
        }
        detailfield.SetText(combine);
    }

}
