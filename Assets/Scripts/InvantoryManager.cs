using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvantoryManager : MonoBehaviour
{
    bool[] isItemReached = new bool[5];
    Image[] itemImageList = new Image[5];
    

    public void SetNewItemInTheInventory(GameObject item)
    {
        
        int itemNumber = item.GetComponent<Items>().itemInventoryNumber;
        if (isItemReached[itemNumber -1])
        {
            Debug.Log($"Not enough place in the inventory for item: {item.name}");
        }
        else
        {
            isItemReached[itemNumber - 1] = true;
            itemImageList[itemNumber - 1].color = new Color(255, 255, 255);
        }
    }

    public void RemoveItemBecauseItsUsed(Items item)
    {
        int itemNumber = item.GetComponent<Items>().itemInventoryNumber;
        isItemReached[itemNumber - 1] = false;
        itemImageList[item.itemInventoryNumber - 1].color = new Color(84, 84, 84);
    }

    public bool HaveTheItemInInventory(Items item)
    {
        if(isItemReached[item.itemInventoryNumber - 1])
        {
            return true;
        }
        return false;
    }
}
