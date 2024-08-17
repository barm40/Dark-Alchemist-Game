using Items;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemInteractableManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Items.Items _itemToTake;

    private InventoryState _inventoryState = InventoryState.Init;

    private void OnEnable()
    {
        Items.Items.CanBeTakenAction += SetItemToTake;
    }

    private void OnDisable()
    {
        Items.Items.CanBeTakenAction -= SetItemToTake;
    }

    private void Start()
    {
        if (inventoryManager == null)
            inventoryManager = GameObject.FindGameObjectWithTag("inventory").GetComponent<InventoryManager>();    
    }

    public void TakeItem(InputAction.CallbackContext context)
    {
        if (context is { started: false, performed: false } || _itemToTake is null) return;
        
        if (_inventoryState == InventoryState.Init)
            ActivateInventory();
        _itemToTake.TakeItem(inventoryManager);
    }

    private void ActivateInventory()
    {
        if (!inventoryManager.gameObject.activeSelf)
            inventoryManager.gameObject.SetActive(true);
        if (inventoryManager.transform.position.x != 0)
            inventoryManager.transform.position = new Vector3(0, 0, 0);
        _inventoryState = InventoryState.Active;
    }

    private void SetItemToTake(Items.Items itemToTake)
    {
        _itemToTake = itemToTake;
    }

    private enum InventoryState
    {
        Init,
        Active,
    }
}
