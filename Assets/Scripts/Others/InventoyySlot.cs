using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemSO slotItem;
    public Image icon;
    public Button button;
    public GameObject ItemObj;

    private void Awake()
    {
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ShowInfo);
    }
    public void PutInSlot(ItemSO item, GameObject obj)
    {
        icon.sprite = item.icon;
        slotItem = item;
        icon.enabled = true;
        ItemObj = obj;
        Inventory.Instance?.NotifyInventoryChanged();
    }
    public void ShowInfo()
    {
        if (slotItem != null)
        {
            Iteminfo.Instance.Open(slotItem, ItemObj, this);
        }
        else { Iteminfo.Instance.Close(); }
    }
    public void ClearSlot()
    {
        slotItem = null;
        ItemObj = null;
        icon.sprite = null;
        icon.enabled = false;
        Inventory.Instance?.NotifyInventoryChanged();
    }


}


