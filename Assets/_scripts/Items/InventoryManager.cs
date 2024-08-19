using Abilities;
using Infra;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class InventoryManager : TrueSingleton<InventoryManager>
    {
        bool[] _isItemReached = new bool[5];
        [SerializeField] Image[] itemImageList;
        Color _defaultColor;
        private static readonly int IsChosen = Animator.StringToHash("isChoosed");

        private short _chosenItems;

        protected override void Awake()
        {
            base.Awake();
            
            _defaultColor = itemImageList[0].color;
        
            // In case of load, change image to correct color
            // CorrectItemStatus();
        }

        public void SetNewItemInTheInventory(GameObject item)
        {
            int itemNumber = item.GetComponent<Items>().ItemInventoryNumber;
            Debug.Log($"Item number in inventory: {itemNumber}");
            if (_isItemReached[itemNumber])
            {
                Debug.Log($"Not enough place in the inventory for item: {item.name}");
            }
            else
            {
                _isItemReached[itemNumber] = true;
                itemImageList[itemNumber].color = new Color(255, 255, 255);
            }
        }

        public bool IsTheItemInInventory(int abilityNumber)
        {
            if (!_isItemReached[abilityNumber]) return false;
            if (_chosenItems < 2)
                ChooseAbility(abilityNumber);
            return true;
        }

        public void UseAbilityItem(int abilityNumber)
        {
            _isItemReached[abilityNumber] = false;
            itemImageList[abilityNumber].color = _defaultColor;
            DisableAbilityItemAnimation(abilityNumber);
        }

        private void DisableAbilityItemAnimation(int abilityNumberInInventory)
        {
            Animator itemAnimation = itemImageList[abilityNumberInInventory].GetComponent<Animator>();
            itemAnimation.SetBool("isChoosed", false);
            _chosenItems--;
        }

        private void ChooseAbility(int abilityNumberInInventory)
        {
            Animator itemAnimation = itemImageList[abilityNumberInInventory].GetComponent<Animator>();
            itemAnimation.SetBool("isChoosed", !itemAnimation.GetBool("isChoosed"));
            _chosenItems++;
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
