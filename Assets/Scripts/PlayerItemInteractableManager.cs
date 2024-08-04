using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerItemInteractableManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Items _itemToTake;

    private void OnEnable()
    {
        Items.CanBeTakenAction += SetItemToTake;
    }

    private void OnDisable()
    {
        Items.CanBeTakenAction -= SetItemToTake;
    }

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(ControlsManager.Instance.Controls["collect"]) && _itemToTake != null)
        {
            if (inventoryManager.transform.position.x != 0)
                inventoryManager.transform.position = new Vector3(0, 0, 0);
            _itemToTake.TakeItem(inventoryManager);
        }
    }

    public void SetItemToTake(Items itemToTake)
    {
        _itemToTake = itemToTake;
    }
}
