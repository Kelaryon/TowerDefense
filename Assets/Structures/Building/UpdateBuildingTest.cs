using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBuildingTest : Building
{
    public override SelectedInfo GetInfo()
    {
        return new SelectedInfo(
            new Dictionary<string, string>
            {
                { "Cost", cost.ToString() },
                { "FlavourText", "Update Building Test" }
            },
            null,
            bank.updateSystem.building1
        );
    }
}
