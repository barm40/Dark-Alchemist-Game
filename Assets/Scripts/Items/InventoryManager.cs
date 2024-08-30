using System.Collections.Generic;
using System.Linq;
using _managers;
using Abilities;
using Infra;
using Infra.Channels;
using Infra.Patterns;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

using itemState = Items.Item.ItemState;

namespace Items
{
    public class InventoryManager : TrueSingleton<InventoryManager> /*, AEventChannelListener<ItemChannel, ItemEvent>*/
    {
        // PlayerInput channel ref
        [Header("Event Channels")] [SerializeField, Tooltip("Choose player input channel Scriptable Object")]
        private PlayerInputChannel playerInputChannel;
        
        // Stat manager ref
        // [SerializeField, Tooltip("Choose stats manager Scriptable Object")]
        // private StatsManager statsManager;

        // Serializable Dictionary of avail. items + bool inInventory
        [Header("Inventory Management")]
        [SerializeField, Tooltip("Add all available items")]
        public ItemAbilityTypeContainer[] inventory;
        
        public SerializableDictionary<ItemAbilityTypeContainer, itemState> inventoryTracker = new();
        
        [SerializeField, Tooltip("Choose Inventory Image Display to Populate")]
        private Image[] itemImages;
        
        [Header("Item Colors")]
        [SerializeField,Tooltip("Color for unavailable items")]
        private Color inactiveColor = new (84,84,84);
        [SerializeField,Tooltip("Color for available items")]
        private Color activeColor = new (255,255,255);
        
        private InventoryState _inventoryState = InventoryState.Init;

        private List<Animator> _inventoryItemAnimators;

        [Range(0, 2)] private int _selectedItems;

        private bool _abilityIntention;
        private bool _isAbilityPerforming;
        

        // subscribe/unsubscribe to Input channel selection and activation
        private void OnEnable()
        {
            Item.CanBeTakenAction += PickUpItem;
            playerInputChannel.SelectAbilityEvent += SelectAbility;
            playerInputChannel.PerformAbilityEvent += AbilityIntent;
            Ability.AbilityEnded += ResetAbility;
        }

        private void OnDisable()
        {
            Item.CanBeTakenAction -= PickUpItem;
            playerInputChannel.SelectAbilityEvent -= SelectAbility;
            playerInputChannel.PerformAbilityEvent -= AbilityIntent;
            Ability.AbilityEnded -= ResetAbility;
        }

        protected override void Awake()
        {
            // Singleton awake function
            base.Awake();
            
            // Place inventory items in tracker and place relevant images
            InitInventoryItems();
        }

        private void Update()
        {
            if (_abilityIntention)
                PerformAbility();
        }

        // Init display item images
        private void InitImages()
        {
            _inventoryItemAnimators = new List<Animator>();
            for (var i = 0; i < inventoryTracker.Count; i++)
            {
                // Set the image of each inventory slot to the correct one according to item
                itemImages[i].sprite = inventoryTracker.ElementAt(i).Key.itemAbility.abilityIcon;
                itemImages[i].color = inactiveColor;
                _inventoryItemAnimators.Add(itemImages[i].GetOrAddComponent<Animator>());
            }
        }

        private void InitInventoryItems()
        {
            foreach (var item in inventory)
            {
                inventoryTracker.TryAdd(item, itemState.NoItem);
            }

            InitImages();
        }
        
        // Init visibility of inventory
        private void InitInventoryHUD()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            if (transform.position.x != 0)
                transform.position = new Vector3(0, 0, 0);
            _inventoryState = InventoryState.Active;
        }
        
        // Select ability
        private void SelectAbility(int abilityNum)
        {
            if (_isAbilityPerforming) return;
            
            Debug.Log($"Selected ability number {abilityNum}");
            
            if (abilityNum < 1 || abilityNum > inventoryTracker.Count) return;

            var selectAbilityIndex = abilityNum - 1;
            var selectAbilityKey = inventoryTracker.Keys.ToList()[selectAbilityIndex];
            
            if (inventoryTracker[selectAbilityKey] is itemState.Available)
            {
                if (_selectedItems >= 2) return;
                inventoryTracker[selectAbilityKey] = itemState.Selected;
                _inventoryItemAnimators[selectAbilityIndex].SetBool("isChoosed", true);
                _selectedItems++;
            }
            else if (inventoryTracker[selectAbilityKey] is itemState.Selected)
            {
                inventoryTracker[selectAbilityKey] = itemState.Available;
                _inventoryItemAnimators[selectAbilityIndex].SetBool("isChoosed", false);
                _selectedItems--;
            }
        }
        
        // Activate ability
        private void PerformAbility()
        {
            foreach (var item in inventoryTracker.ToList())
            {
                if (item.Value is not itemState.Selected) continue;
                
                // Activate ability
                inventoryTracker[item.Key] = itemState.Active;
                StartCoroutine(AbilityController.Instance.Abilities[item.Key.itemAbility.abilityType].Perform());
                // reset color to unavailable
                itemImages[inventoryTracker.Keys.ToList().IndexOf(item.Key)].color = inactiveColor;
                // lock new ability activity
                _isAbilityPerforming = true;
            }
        }
        
        // Set Intention to perform ability
        private void AbilityIntent(bool intent)
        {
            _abilityIntention = intent;
        }

        // Reset ability item after done performing (on event)
        private void ResetAbility(Ability.AbilityTypes type)
        {
            // Iterate over all items in inventory
            foreach (var item in inventoryTracker.ToList())
            {
                // skip if not the correct type
                if (item.Key.itemAbility.abilityType != type) continue;
                
                // if item inactive, return
                if (inventoryTracker[item.Key] != itemState.Active) return;
                
                Debug.Log($"Ability {item.Key.itemAbility.abilityType} has ended");
                // remove from inventory
                inventoryTracker[item.Key] = itemState.NoItem;
                // reset animation
                _inventoryItemAnimators[inventoryTracker.Keys.ToList().IndexOf(item.Key)].SetBool("isChoosed", false);
            }
            // unlock abilities
            _isAbilityPerforming = false;
        }

        // Item pick up on event
        private void PickUpItem(Item itemToPickup)
        {
            // Check item not null
            if (itemToPickup is null) return;
            
            // If first item, init
            if (_inventoryState == InventoryState.Init)
                InitInventoryHUD();

            // Check if item should be available to pick up at all
            if (!inventoryTracker.TryGetValue(itemToPickup.itemAbilityType, out var itemState)) return;
            
            // Check if item already in inventory
            if (itemState != itemState.NoItem) return;
            
            // Pick up item, set to available and active color
            itemToPickup.PickUp();
            inventoryTracker[itemToPickup.itemAbilityType] = itemState.Available;
            itemImages[inventoryTracker.Keys.ToList().IndexOf(itemToPickup.itemAbilityType)].color = activeColor;
        }
        
        private enum InventoryState
        {
            Init,
            Active,
        }
    }
}
