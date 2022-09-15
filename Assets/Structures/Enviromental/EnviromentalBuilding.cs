using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentalBuilding : Structure
{
    public ExtractorScript[] extractors;
    [SerializeField] int collectionSize;
    public override SelectedInfo GetInfo()
    {
        return null;
    }
    private void Start()
    {
        extractors = new ExtractorScript[collectionSize];
    }
}
