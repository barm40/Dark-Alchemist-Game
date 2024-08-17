using Abilities;

namespace Items
{
    public class LightImmuneItem : Items
    {
    
        private void Start()
        {
            for (int i = 0; i < abilityController.AbilitiesList.Count; i++)
            {
                if (abilityController.AbilitiesList[i].AbilityType == Ability.AbilityTypes.ImmuneType)
                {
                    ItemInventoryNumber = abilityController.AbilitiesList[i].AbilityNumber;
                }
            }
        }
    }
}
