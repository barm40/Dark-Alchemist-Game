using UnityEngine;
using UnityEngine.UI;

public class InvantoryManager : MonoBehaviour
{
    bool[] isItemReached = new bool[5];
    [SerializeField] Image[] itemImageList = new Image[5];
    Color defaultColor = new Color();
    private void Start()
    {
        defaultColor = itemImageList[0].color;
    }


    public void SetNewItemInTheInventory(GameObject item)
    {

        int itemNumber = item.GetComponent<Items>().itemInventoryNumber;
        if (isItemReached[itemNumber])
        {
            Debug.Log($"Not enough place in the inventory for item: {item.name}");
        }
        else
        {
            isItemReached[itemNumber] = true;
            itemImageList[itemNumber].color = new Color(255, 255, 255);
        }
    }

    public bool IsTheItemInInventory(int abilityNumber)
    {
        int abilityNumberInInventory = abilityNumber;
        if (isItemReached[abilityNumberInInventory])
        { 
            ChooseAbility(abilityNumberInInventory);
            return true;
        }
        return false;
    }

    public void useAbilityItem(int abilityNumber)
    {
        int abilityNumberInInventory = abilityNumber;
        isItemReached[abilityNumberInInventory] = false;
        itemImageList[abilityNumberInInventory].color = defaultColor;
        //itemImageList[abilityNumberInInvantory].enabled = false;
        ChooseAbility(abilityNumberInInventory);
    }

    public void useAbilityItem(int abilityNumber, int abilityNumber2)
    {
        int abilityNumberInInventory = abilityNumber;
        useAbilityItem(abilityNumberInInventory);
        abilityNumberInInventory = abilityNumber2;
        useAbilityItem(abilityNumberInInventory);
    }

    private void ChooseAbility(int abilityNumberInInventory)
    {
        Animator itemAnimation = itemImageList[abilityNumberInInventory].GetComponent<Animator>();
        bool isPlay = itemAnimation.GetBool("isChoosed");
        itemAnimation.SetBool("isChoosed", !isPlay);
        return;
    }
}
