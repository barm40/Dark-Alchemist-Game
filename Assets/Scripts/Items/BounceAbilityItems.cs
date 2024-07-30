using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAbilityItems : Items
{
    private void Start()
    {
        for (int i = 0; i < AbilityController.abilitiesList.Count; i++)
        {
            if (AbilityController.abilitiesList[i].AbilityType == Ability.AbilityTypes.BounceType)
            {
                ItemInventoryNumber = AbilityController.abilitiesList[i].abilityNumber;
            }
        }
    }
}
