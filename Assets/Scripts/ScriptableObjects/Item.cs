using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string Name = "";
    public string Description = "Описание предмета";
    public Sprite icon = null;

    public bool isHealing;
    public int healingPower;

    public bool isTool;
    public int toolIndex;

}
