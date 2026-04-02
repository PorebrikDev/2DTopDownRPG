using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{
    public ItemSO item;
    private GameObject _itemObj;

    private void Start()
    {
        _itemObj = gameObject;
    }
    public void Interact()
    {
        Debug.Log("Предмет получен");
        Inventory.Instance.PutInEmptySlot(item, _itemObj);
        gameObject.SetActive(false);

    }
}
