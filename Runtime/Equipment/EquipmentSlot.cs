using UnityEngine;

namespace Reshyl.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Equipment Slot")]
    public class EquipmentSlot : ScriptableObject
    {
        public string id = "slot-id";
        public string displayName = "Slot Name";
        [TextArea] public string description;
        public Sprite icon;
        public Color color = Color.white;
    }
}