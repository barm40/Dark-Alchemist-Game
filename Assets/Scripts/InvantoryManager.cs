using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvantoryManager : MonoBehaviour
{
    [SerializeField] Dictionary<GameObject, Sprite> itemsInInventory = new Dictionary<GameObject, Sprite>();
    [SerializeField] List<Button> buttonsList = new List<Button>();


    public void SetNewItemInTheInventory(GameObject item)
    {
        for (int i = 0; i < buttonsList.Count; i++)
        {
            if (buttonsList[i].image.sprite == null)
            {
                Debug.Log(item.gameObject.name);
                Sprite itemSprite = item.GetComponent<SpriteRenderer>().sprite;
                //Debug.Log(itemSprite.name);
                itemsInInventory.Add(item, itemSprite);
                buttonsList[i].image.sprite = itemsInInventory.GetValueOrDefault(item, null);
                break;
            }
            else
            {
                Debug.Log($"Not enough place in the inventory for item: {item}");
            }
        }
    }
}
