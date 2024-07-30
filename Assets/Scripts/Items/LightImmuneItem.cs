using System.Collections;
using UnityEngine;


public class LightImmuneItem : Items
{
    
    private void Start()
    {
        for (int i = 0; i < AbilityController.abilitiesList.Count; i++)
        {
            if (AbilityController.abilitiesList[i].AbilityType == Ability.AbilityTypes.ImmuneType)
            {
                ItemInventoryNumber = AbilityController.abilitiesList[i].abilityNumber;
            }
        }
    }
}
