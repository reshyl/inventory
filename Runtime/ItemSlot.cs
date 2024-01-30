using System.Collections.Generic;

namespace Reshyl.Inventory
{
    public class ItemSlot
    {
        public string itemID;
        public int stackLimit;
        public bool isUsable;
        public bool isEquippable;

        private Item itemTemplate;
        private List<Item> items;
        private bool setup = false;

        /// <summary>
        /// Create a new slot and call Setup() automatically.
        /// </summary>
        /// <param name="itemTemplate">The item that this slot will keep track of.</param>
        /// <param name="initialAmount">How many copies should be made of the itemTemplate.</param>
        public ItemSlot(Item itemTemplate, int initialAmount)
        {
            Setup(itemTemplate, initialAmount);
        }

        /// <summary>
        /// Initilize this slot with a given item. Must be called before using any other functions on this slot.
        /// Note: If this slot is already setup, calling this will replace
        /// the current contents.
        /// </summary>
        /// <param name="itemTemplate">The item that this slot will keep track of. Ideally this is the original 
        /// ScriptableObject of the item instead of a runtime copy.</param>
        /// <param name="initialAmount">How many copies should be made of the given item.</param>
        public void Setup(Item itemTemplate, int initialAmount)
        {
            if (setup)
                Reset();

            this.itemTemplate = itemTemplate;
            itemID = itemTemplate.id;
            stackLimit = itemTemplate.stackLimit;
            isUsable = itemTemplate.isUsable;
            isEquippable = itemTemplate.isEquippable;

            if (items == null)
                items = new List<Item>();

            for (int i = 0; i < initialAmount; i++)
            {
                var item = itemTemplate.GetRuntimeCopy();

                if (i < stackLimit)
                    items.Add(item);
            }

            setup = true;
        }

        /// <summary>
        /// Clear this slot completely. Can be setup with a different item later by calling Setup()
        /// </summary>
        public void Reset()
        {
            itemTemplate = null;
            itemID = string.Empty;
            stackLimit = 0;
            isUsable = false;
            isEquippable = false;
            items.Clear();

            setup = false;
        }

        public int GetItemCount()
        {
            if (!setup)
                return 0;

            return items.Count;
        }

        /// <summary>
        /// Get a reference to the base item of this slot. This is the original ScriptableObject whose
        /// copies are in this slot. Do not change any properties on this object.
        /// </summary>
        public Item InspectItem()
        {
            if (!setup)
                return null;

            return itemTemplate;
        }

        /// <summary>
        /// Create additional copies of the item in this slot. Slot must be Setup() with an item first.
        /// </summary>
        /// <returns>How many copies weren't created on account of hitting the item's stackLimit. 
        /// 0 if all were added succesfully.</returns>
        public int AddItem(int amount = 1)
        {
            if (!setup)
                return amount;

            var remaining = amount;

            for (int i = 0; i < amount; i++)
            {
                if (items.Count < stackLimit)
                {
                    var item = itemTemplate.GetRuntimeCopy();
                    items.Add(item);
                    remaining--;
                }
            }

            return remaining;
        }

        /// <summary>
        /// Add a specific runtime copy of an item to this slot. Slot must be setup before use.
        /// </summary>
        /// <returns>False if the given item is different from what's saved in this slot, 
        /// or the stackLimit has been reached.</returns>
        public bool AddItem(Item item)
        {
            if (!setup)
                return false;

            if (itemID != item.id)
                return false;

            if (items.Count >= stackLimit)
                return false;

            items.Add(item);
            return true;
        }
    }
}