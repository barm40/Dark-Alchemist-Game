using System;
using Abilities;
using Infra.SOTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Type", menuName = "Items/Ability Item")]
public class ItemAbilityTypeContainer : AStatContainer<ItemAbility>
{
    [SerializeField]
    public ItemAbility itemAbility;
}

[Serializable]
public struct ItemAbility : IStats
{
    public Ability.AbilityTypes abilityType;
    public Sprite abilityIcon;
    public RuntimeAnimatorController animator;
    
    public ItemAbility (ItemAbility itemAbility, Sprite itemImage, RuntimeAnimatorController animatorController)
    {
        abilityType = itemAbility.abilityType;
        abilityIcon = itemImage;
        animator = animatorController;
    }
}
