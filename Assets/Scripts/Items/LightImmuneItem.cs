using System.Collections;
using UnityEngine;


public class LightImmuneItem : Items
{
    
    private void Start()
    {
        for (int i = 0; i < abilityController.abilitiesList.Count; i++)
        {
            if (abilityController.abilitiesList[i].AbilityType == Ability.AbilityTypes.ImmuneType)
            {
                ItemInventoryNumber = abilityController.abilitiesList[i].abilityNumber;
            }
        }
    }
}
