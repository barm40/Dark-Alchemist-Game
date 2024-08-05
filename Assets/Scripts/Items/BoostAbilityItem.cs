using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAbilityItem: Items
{
    private void Start()
    {
        for (int i = 0; i < abilityController.AbilitiesList.Count; i++)
        {
            if (abilityController.AbilitiesList[i].AbilityType == Ability.AbilityTypes.BoostType)
            {
                ItemInventoryNumber = abilityController.AbilitiesList[i].AbilityNumber;
            }
        }
    }
}
