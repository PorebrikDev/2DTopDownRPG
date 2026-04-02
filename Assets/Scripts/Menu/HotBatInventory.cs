using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBatInventory : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    [SerializeField] private Image[] hotbarIcons;


    private void Awake()
    {
        inventory.OnInventoryChanged += UpdateHotbar;
    }

    private void OnDestroy()
    {
        inventory.OnInventoryChanged -= UpdateHotbar;
    }
    private void UpdateHotbar(object sender, EventArgs e)
    {
        for (int i = 0; i < hotbarIcons.Length; i++)
        {
            var slot = inventory.inventorySlots[i];
            if (slot.slotItem != null)
            {
                hotbarIcons[i].sprite = slot.slotItem.icon;
                hotbarIcons[i].enabled = true;
            }
            else 
            {
                hotbarIcons[i].sprite = null;
                hotbarIcons[i].enabled = false;
            }
        }
    }
}
