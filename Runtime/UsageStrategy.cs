using UnityEngine;

namespace Reshyl.Inventory
{
    public abstract class UsageStrategy : ScriptableObject
    {
        public virtual UsageStrategy GetRuntimeCopy()
        {
            return ScriptableObject.Instantiate<UsageStrategy>(this);
        }

        public abstract bool CanUse(GameObject user, Item item);
        public abstract void Use(GameObject user, Item item);
    }
}