using System;
using _managers;
using Abilities;
using UnityEngine;
using UnityEngine.Serialization;
using Infra;
using Infra.Patterns;

namespace Items
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public abstract class Items : Singleton<Items>
    {
        [SerializeField, Header("Item Type"), Tooltip("Item Type Scriptable Object")]
        public ItemAbilityTypeContainer itemAbilityType;

        [Header("GUID for Save Data")]
        // create unique ids for items to store which items were collected already
        [SerializeField] private string id;
        [ContextMenu("Generate guid")]
        private void GenerateGuid()
        {
            id = Guid.NewGuid().ToString();
        }
        
        private SpriteRenderer _itemSprite;
        private Animator _animator;
        
        // private void OnEnable()
        // {
        //     // Subscribe to ability events
        // }
        //
        // private void OnDisable()
        // {
        //     // Unsubscribe from ability events
        // }
        
        protected bool IsUsed;
        protected bool CanBeTaken;
        public int ItemInventoryNumber { get; protected set;}
        private static int _amountOfAbilities;

        public static event Action<Items> CanBeTakenAction;

        protected override void Awake()
        {
            base.Awake();
            
            _itemSprite = gameObject.GetOrAddComponent<SpriteRenderer>();
            _itemSprite.sprite = itemAbilityType.itemAbility.abilityIcon;

            _animator = gameObject.GetOrAddComponent<Animator>();
            _animator.runtimeAnimatorController = itemAbilityType.itemAbility.animator;
        }
        
        private void Start()
        {
            foreach (var ability in AbilityController.Instance.Abilities.Values)
            {
                if (ability.AbilityType != itemAbilityType.itemAbility.abilityType) continue;
                
                ItemInventoryNumber = _amountOfAbilities;
                _amountOfAbilities++;
                Debug.Log($"Set Ability Type {ability.AbilityType} with number {ItemInventoryNumber}");
            }
        }
    
        public void TakeItem(InventoryManager inventory)
        {
            if (!CanBeTaken) return;
            Debug.Log($"Item {gameObject.name} is taken");
            
            inventory.SetNewItemInTheInventory(gameObject);
            transform.parent = inventory.transform;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {
            CanBeTaken = true;
            CanBeTakenAction?.Invoke(this);
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            CanBeTaken = false;
            CanBeTakenAction?.Invoke(null);
        }

        //public void LoadData(GameData data)
        //{
        //    data.itemsCollected.TryGetValue(id, out IsTaken);
        
        //    if (IsTaken)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}

        //public void SaveData(GameData data)
        //{
        //    if (data.itemsCollected.ContainsKey(id))
        //    {
        //        data.itemsCollected.Remove(id);
        //    }

        //    data.itemsCollected.Add(id, IsTaken);
        //}
    }

}
