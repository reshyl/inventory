using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Equipment")]
    public class Equipment : ScriptableObject
    {
        [SerializeField] protected List<EquipmentSlot> slots;
        protected Item[] equippedItems;

        public GameObject User { get; protected set; }

        public Equipment GetRuntimeCopy(GameObject user)
        {
            var copy = ScriptableObject.Instantiate(this);
            copy.equippedItems = new Item[slots.Count];
            User = user;

            return copy;
        }

        public int GetSlotCount()
        {
            return slots.Count;
        }

        public EquipmentSlot GetSlotAt(int index)
        {
            return slots[index];
        }

        public bool Equip(Item item)
        {
            if (!item.Instanced)
                return false;

            if (!item.isEquippable)
                return false;

            if (!slots.Contains(item.equipmentSlot))
                return false;

            var index = slots.IndexOf(item.equipmentSlot);

            if (equippedItems[index] != null)
                return false;

            if (item.EquipmentStrategy.CanEquip(User, item))
            {
                item.EquipmentStrategy.Equip(User, item);
                equippedItems[index] = item;
                return true;
            }

            return false;
        }

        public bool Unequip(EquipmentSlot slot)
        {
            if (!slots.Contains(slot))
                return false;

            var index = slots.IndexOf(slot);
            var item = equippedItems[index];

            if (item == null)
                return false;

            item.EquipmentStrategy.Unequip(User, item);
            return true;
        }

        public bool Unequip(int slotIndex)
        {
            var slot = slots[slotIndex];
            return Unequip(slot);
        }

        public bool Unequip(Item item)
        {
            var slot = item.equipmentSlot;
            return Unequip(slot);
        }
    }
}