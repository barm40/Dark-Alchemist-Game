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
    public class Item : MonoBehaviour
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
        
        private bool _canBeTaken;

        public int ItemInventoryNumber { get; protected set;}
        public static event Action<Item> CanBeTakenAction;

        private void Awake()
        {
            _itemSprite = gameObject.GetOrAddComponent<SpriteRenderer>();
            _itemSprite.sprite = itemAbilityType.itemAbility.abilityIcon;

            _animator = gameObject.GetOrAddComponent<Animator>();
            _animator.runtimeAnimatorController = itemAbilityType.itemAbility.animator;
        }
    
        public void PickUp()
        {
            if (!_canBeTaken) return;
            Debug.Log($"Item {gameObject.name} is taken");
            
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {
            _canBeTaken = true;
            CanBeTakenAction?.Invoke(this);
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            _canBeTaken = false;
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
        
        public enum ItemState
        {
            NoItem,
            Available,
            Selected,
            Active
        }
    }

}
