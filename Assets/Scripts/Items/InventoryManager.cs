using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    bool[] _isItemReached = new bool[5];
    [SerializeField] Image[] itemImageList = new Image[5];
    Color _defaultColor;
    private static readonly int IsChosen = Animator.StringToHash("isChoosed");

    private void Awake()
    {
        _defaultColor = itemImageList[0].color;
        
        // In case of load, change image to correct color
        CorrectItemStatus();
    }

    public void SetNewItemInTheInventory(GameObject item)
    {

        int itemNumber = item.GetComponent<Items>().ItemInventoryNumber;
        if (_isItemReached[itemNumber])
        {
            Debug.Log($"Not enough place in the inventory for item: {item.name}");
        }
        else
        {
            _isItemReached[itemNumber] = true;
            itemImageList[itemNumber].color = new Color(255, 255, 255);
        }
    }

    public bool IsTheItemInInventory(int abilityNumber)
    {
        if (!_isItemReached[abilityNumber]) return false;
        
        ChooseAbility(abilityNumber);
        return true;
    }

    public void UseAbilityItem(int abilityNumber)
    {
        int abilityNumberInInventory = abilityNumber;
        _isItemReached[abilityNumberInInventory] = false;
        itemImageList[abilityNumberInInventory].color = _defaultColor;
        DisableAbilityItemAnimation(abilityNumberInInventory);
    }

    private void DisableAbilityItemAnimation(int abilityNumberInInventory)
    {
        Animator itemAnimation = itemImageList[abilityNumberInInventory].GetComponent<Animator>();
        itemAnimation.SetBool("isChoosed", false);
    }

    private void ChooseAbility(int abilityNumberInInventory)
    {
        Animator itemAnimation = itemImageList[abilityNumberInInventory].GetComponent<Animator>();
        bool isPlay = itemAnimation.GetBool("isChoosed");
        itemAnimation.SetBool("isChoosed", !isPlay);
    }

    // public void LoadData(GameData data)
    // {
    //     _isItemReached = data.isItemReached;
    //     
    //     // In case of load, change image to correct color
    //     CorrectItemStatus();
    // }
    //
    // public void SaveData(GameData data)
    // {
    //     data.isItemReached = _isItemReached;
    // }

    // used to correct the inventory to match the loaded data
    
    private void CorrectItemStatus()
    {
        var rIndex = 0;
        foreach (var isReached in _isItemReached)
        {
            if (isReached)
            {
                itemImageList[rIndex].color = new Color(255, 255, 255);
            }
            rIndex++;
        }
    }
}
