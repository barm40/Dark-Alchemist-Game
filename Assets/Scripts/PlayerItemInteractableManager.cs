using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteractableManager : MonoBehaviour
{
    [SerializeField] InvantoryManager invantoryManager;
    [SerializeField] private Items itemToTake;

    private void OnEnable()
    {
        Items.isCanBeTokenAction += SetItemToTake;
    }

    private void OnDisable()
    {
        Items.isCanBeTokenAction -= SetItemToTake;
    }

    private void Start()
    {
        invantoryManager = FindObjectOfType<InvantoryManager>();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(ControlsManager.Controls["collect"]) && itemToTake != null)
        {
            itemToTake.TakeItem(invantoryManager);
        }
    }

    public void SetItemToTake(Items itemToTake)
    {
        this.itemToTake = itemToTake;
    }
}
