using UnityEngine;

namespace Reshyl.Inventory
{
    public abstract class EquipmentStrategy : ScriptableObject
    {
        public virtual EquipmentStrategy GetRuntimeCopy()
        {
            return ScriptableObject.Instantiate<EquipmentStrategy>(this);
        }

        public abstract bool CanEquip(GameObject user, Item item);
        public abstract void Equip(GameObject user, Item item);
        public abstract void Unequip(GameObject user, Item item);
    }
}