using Reshyl.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [Header("Meta Data")]
        public string id = "item-id";
        public string displayName = "Item Name";
        [TextArea] public string description;
        public Sprite icon;
        public Color color = Color.white;

        [Header("Properties")]
        [SerializeField] protected StatsContainer stats;
        [SerializeField] public int stackLimit = 1;
        [SerializeField] protected StatField weight;
        [SerializeField] protected StatField price;

        [Header("Usage")]
        public bool isUsable = false;
        public bool consumeOnUse = true;
        [SerializeField] protected UsageStrategy useStrategy;

        [Header("Equipment")]
        public bool isEquippable = false;
        public EquipmentSlot equipmentSlot;
        [SerializeField] protected EquipmentStrategy equipStrategy;

        [Header("Crafting")]
        public bool isCraftable = false;
        public List<ItemCollection> craftingRequirements;
        public bool isDismantlable = false;
        public List<ItemCollection> dismanteResults;

        public StatsContainer Stats { get; protected set; }
        public UsageStrategy UsageStrategy { get; protected set; }
        public EquipmentStrategy EquipmentStrategy { get; protected set; }

        public float Price => price.GetValue(Stats);
        public float Weight => weight.GetValue(Stats);

        /// <summary>
        /// Is this a runtime instance of the Item? Automatically set when GetRuntimeCopy() is used.
        /// </summary>
        public bool Instanced {  get; protected set; }

        public virtual Item GetRuntimeCopy()
        {
            var item = ScriptableObject.Instantiate<Item>(this);

            if (stats != null)
                item.Stats = stats.GetRuntimeCopy();
            
            if (isUsable)
                item.UsageStrategy = useStrategy.GetRuntimeCopy();
            
            if (isEquippable)
                item.EquipmentStrategy = equipStrategy.GetRuntimeCopy();

            item.Instanced = true;
            return item;
        }
    }
}