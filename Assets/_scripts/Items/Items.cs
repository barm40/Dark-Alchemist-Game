using System;
using _managers;
using UnityEngine;

namespace Items
{
    public abstract class Items : MonoBehaviour
    {
        // create unique ids for items to store which items were collected already
        [SerializeField] private string id;
        [ContextMenu("Generate guid")]
        private void GenerateGuid()
        {
            id = Guid.NewGuid().ToString();
        }
    
        protected AbilityController abilityController;
    
        protected bool IsUsed;
        protected bool CanBeTaken;
        protected bool IsTaken;
        [SerializeField] public int ItemInventoryNumber { get; protected set;}

        public static event Action<Items> CanBeTakenAction;
        private void Awake()
        {
            abilityController = FindObjectOfType<AbilityController>();
            Debug.Log("ability controller is: " + abilityController);
        }
    
        public void TakeItem(InventoryManager inventory)
        {
            if (!IsTaken)
            {
                CanBeTaken = true;
                inventory.SetNewItemInTheInventory(gameObject);
                transform.parent = inventory.transform;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        protected  virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsTaken)
            {
                CanBeTaken = true;
                CanBeTakenAction?.Invoke(this);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!IsTaken)
            {
                CanBeTaken = false;
                CanBeTakenAction?.Invoke(null);
            }
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
