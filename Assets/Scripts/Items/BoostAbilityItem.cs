using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAbilityItem: Items
{
    private void Start()
    {
        for (int i = 0; i < AbilityController.abilitiesList.Count; i++)
        {
            if (AbilityController.abilitiesList[i].AbilityType == Ability.AbilityTypes.BoostType)
            {
                ItemInventoryNumber = AbilityController.abilitiesList[i].abilityNumber;
            }
        }
    }
}
