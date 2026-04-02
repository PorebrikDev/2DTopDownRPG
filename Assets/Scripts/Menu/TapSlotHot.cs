using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapSlotHot : MonoBehaviour
{
    private Button button;

    [SerializeField] private InventorySlot inventorySlot;
    [SerializeField] private int buttonIndex;
    [SerializeField] private ItemSO xxx;

    private void Awake()
    {
        button = GetComponent<Button>();



    }
    private void OnEnable()
    {
        GameInput.Instance.OnNumberKeyStarted += ClickHotSlot;
    }
    private void Start()
    {
        button.onClick.AddListener(() =>  ClickHotSlot(null, buttonIndex));
    }

    private void ClickHotSlot(object sender, int index)
    {
        if (index != buttonIndex)
            return;

        if (inventorySlot == null || inventorySlot.slotItem == null)
            return;

        ItemSO item = inventorySlot.slotItem;
        xxx = item;
        UseOfItems.Instance.Use(item);
    }
    private void OnDisable()
    {
        GameInput.Instance.OnNumberKeyStarted -= ClickHotSlot;
    }

}
