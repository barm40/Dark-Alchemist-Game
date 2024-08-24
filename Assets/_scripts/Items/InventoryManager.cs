using System.Collections.Generic;
using Abilities;
using Infra;
using Infra.Patterns;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Items
{
    public class InventoryManager : TrueSingleton<InventoryManager>
    {
        bool[] _isItemReached = new bool[5];
        [SerializeField] Image[] itemImageList;
        [SerializeField] private SerializableDictionary<Image, bool> itemReachedList;
        
        Color _defaultColor;
        
        private Item _itemManagerToTake;

        private InventoryState _inventoryState = InventoryState.Init;
        
        private short _chosenItems;

        protected override void Awake()
        {
            base.Awake();
            
            _defaultColor = itemReachedList.keys[0].color;
        
            // In case of load, change image to correct color
            // CorrectItemStatus();
        }

        // public void SetNewItemInTheInventory(GameObject item)
        // {
        //     var itemNumber = item.GetOrAddComponent<Item>().ItemInventoryNumber;
        //     Debug.Log($"Item number in inventory: {itemNumber}");
        //     if (itemReachedList.values[itemNumber])
        //     {
        //         Debug.Log($"Not enough place in the inventory for item: {item.name}");
        //     }
        //     else
        //     {
        //         itemReachedList.values[itemNumber] = true;
        //         itemReachedList.keys[itemNumber].color = new Color(255, 255, 255);
        //     }
        // }
        //
        // public bool IsTheItemInInventory(int abilityNumber)
        // {
        //     if (!itemReachedList.values[abilityNumber]) return false;
        //     
        //     ChooseAbility(abilityNumber);
        //     return true;
        // }
        //
        // public void UseAbilityItem(int abilityNumber)
        // {
        //     itemReachedList.values[abilityNumber] = false;
        //     itemReachedList.keys[abilityNumber].color = _defaultColor;
        //     DisableAbilityItemAnimation(abilityNumber);
        // }
        //
        // private void DisableAbilityItemAnimation(int abilityNumberInInventory)
        // {
        //     Animator itemAnimation = itemReachedList.keys[abilityNumberInInventory].GetComponent<Animator>();
        //     itemAnimation.SetBool("isChoosed", false);
        //     _chosenItems--;
        // }
        //
        // private void ChooseAbility(int abilityNumberInInventory)
        // {
        //     Animator itemAnimation = itemReachedList.keys[abilityNumberInInventory].GetComponent<Animator>();
        //     if (itemAnimation.GetBool("isChoosed"))
        //     {
        //         _chosenItems--;
        //     }
        //     else
        //     {
        //         if (_chosenItems >= 2) return;
        //         _chosenItems++;
        //     }
        //     itemAnimation.SetBool("isChoosed", !itemAnimation.GetBool("isChoosed"));
        // }
        //
        // private void OnEnable()
        // {
        //     Item.CanBeTakenAction += SetItemToTake;
        // }
        //
        // private void OnDisable()
        // {
        //     Item.CanBeTakenAction -= SetItemToTake;
        // }
        //
        //
        // // public void TakeItem(InputAction.CallbackContext context)
        // // {
        // //
        // // }
        //
        // private void ActivateInventory()
        // {
        //     if (!gameObject.activeSelf)
        //         gameObject.SetActive(true);
        //     if (transform.position.x != 0)
        //         transform.position = new Vector3(0, 0, 0);
        //     _inventoryState = InventoryState.Active;
        // }
        //
        // private void SetItemToTake(Item targetItemManager)
        // {
        //     if (targetItemManager is null || !itemReachedList.values[(int)targetItemManager.itemAbilityType.itemAbility.abilityType]) return;
        //
        //     if (_inventoryState == InventoryState.Init)
        //         ActivateInventory();
        //     // targetItemManager.PickUp(this);
        // }

        private enum InventoryState
        {
            Init,
            Active,
        }

        // public void LoadData(GameData data)
        // {
        //     _isItemReached = data.isItemReached;
        //     
        //     // In case of load, change image to correct color
        //     CorrectItemStatus();
        // }
        //
        // public void SaveData(GameData data)
        // {
        //     data.isItemReached = _isItemReached;
        // }

        // used to correct the inventory to match the loaded data
    
        // private void CorrectItemStatus()
        // {
        //     var rIndex = 0;
        //     foreach (var isReached in _isItemReached)
        //     {
        //         if (isReached)
        //         {
        //             itemImageList[rIndex].color = new Color(255, 255, 255);
        //         }
        //         rIndex++;
        //     }
        // }
    }
}
