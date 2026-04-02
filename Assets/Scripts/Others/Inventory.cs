using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public Transform slotsParent;
    public InventorySlot[] inventorySlots = new InventorySlot[14];
    private bool _isInventory;
    private InventorySlot CurrentSlot;
    public event EventHandler OnInventoryChanged;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        IsInventory = true;
        InventoryOpenClose();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = slotsParent.GetChild(i).GetComponent<InventorySlot>();
        }
    }
    public void PutInEmptySlot(ItemSO item, GameObject obj)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].slotItem == null)
            {
                inventorySlots[i].PutInSlot(item, obj);

                NotifyInventoryChanged();
                return;
            }
        }
    }
    public void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool IsInventory
    {
        get { return _isInventory; }
        set
        {
            _isInventory = value;
            if (_isInventory)
            {
                gameObject.SetActive(true);
                GameInput.Instance.DisableCombat();

            }
            else
            {
                gameObject.SetActive(false);
                GameInput.Instance.EnableCombat();
            }
        }
    }

    public void InventoryOpenClose()
    {
        if (IsInventory == true)
        {
            IsInventory = false;
            if (Iteminfo.Instance != null)
            {
                Iteminfo.Instance.Close();
            }
        }
        else
        {
            IsInventory = true;
        }
    }
}

