using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Iteminfo : MonoBehaviour
{
    public static Iteminfo Instance;
    private Image BackGround;
    private TMP_Text title;
    private TMP_Text description;
    private Image Icon;
    private Button closeButton;
    private Button useButton;
    private Button dropButton;
    [SerializeField] private ItemSO currentItem;
    [SerializeField] private GameObject itemObj;
    private InventorySlot CurrentSlot;

    private void Awake()
    {
        Instance = this;
        BackGround = GetComponent<Image>();
        title = transform.GetChild(0).GetComponent<TMP_Text>();
        description = transform.GetChild(1).GetComponent<TMP_Text>();
        Icon = transform.GetChild(2).GetComponent<Image>();
        closeButton = transform.GetChild(3).GetComponent<Button>();
        useButton = transform.GetChild(4).GetComponent<Button>();
        dropButton = transform.GetChild(5).GetComponent<Button>();

        closeButton.onClick.AddListener(Close);
        useButton.onClick.AddListener(Use);
        dropButton.onClick.AddListener(Drop);
    }
    public void ChangeInfo(ItemSO item)
    {
      
        title.text = item.Name;
        description.text = item.Description;
        Icon.sprite = item.icon;
    }
    public void Use()
    {
        UseOfItems.Instance.Use(currentItem);
    }
    public void Drop()
    {
        if (currentItem.isTool == true && (int)ActiveWeapon.Instance.CurrentToolType == currentItem.toolIndex)
        {
            UIActiveWeapon.Instance.ZeroIcone();
            WeaponZero();
        }

        Vector2 DropPos = (Vector2)Player.Instance.transform.position + Player.Instance.LastMoveDirection;
        itemObj.SetActive(true);
        itemObj.transform.position = DropPos;
        CurrentSlot.ClearSlot();
        Close();
    }
    public void WeaponZero()
    {
        ActiveWeapon.Instance.SetActiveWeapon(3);
    }
    public void Open(ItemSO item, GameObject itemObject, InventorySlot _currentSlot)
    {
        
        currentItem = item;
        itemObj = itemObject;
        ChangeInfo(item);
        gameObject.SetActive(true);
        CurrentSlot = _currentSlot;
      

    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
