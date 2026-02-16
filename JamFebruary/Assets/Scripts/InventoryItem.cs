using UnityEngine;

[CreateAssetMenu(
    fileName = "New Inventory Item",
    menuName = "Inventory/Inventory Item"
)]
public class InventoryItem : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
}