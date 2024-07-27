using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    protected AbilityController abilityController;
    protected bool isUsed = false;
    protected bool isCanBeToken = false;
    protected bool isToken = false;
    [SerializeField] public int itemInventoryNumber { get; protected set;}

    public static event Action<Items> isCanBeTokenAction = null;
    private void Awake()
    {
        abilityController = FindObjectOfType<AbilityController>();
    }
    public void TakeItem(InvantoryManager inventory)
    {
        if (!isToken)
        {
            isCanBeToken = true;
            inventory.SetNewItemInTheInventory(gameObject);
            transform.parent = inventory.transform;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    protected  virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!isToken)
        {
            isCanBeToken = true;
            isCanBeTokenAction?.Invoke(this);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (!isToken)
        {
            isCanBeToken = false;
            isCanBeTokenAction?.Invoke(null);
        }
    }
}
