using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityItems : Items
{
    private void Start()
    {
        for (int i = 0; i < abilityController.abilitiesList.Count; i++)
        {
            if (abilityController.abilitiesList[i].AbilityType == Ability.AbilityTypes.DashType)
            {
                itemInventoryNumber = abilityController.abilitiesList[i].abilityNumber;
            }
        }
    }
}
