using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{

    protected bool isUsed = false;
    protected bool isCanBeToken = false;
    protected bool isToken = false;

    public static event Action<Items> isCanBeTokenAction = null;

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

    protected abstract void UseItem();

    protected  virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name} is in the trigger");
        if (!isToken)
        {
            Debug.Log($"{gameObject.name} can be token");
            isCanBeToken = true;
            isCanBeTokenAction?.Invoke(this);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"The player is too far");
        if (!isToken)
        {
            isCanBeToken = false;
            isCanBeTokenAction?.Invoke(null);
        }
    }
}
