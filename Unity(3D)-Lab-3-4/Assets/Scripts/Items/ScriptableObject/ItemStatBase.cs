using UnityEngine;

namespace Items.ScriptableObject
{
    public abstract class ItemStatBase : Item
    {
        [SerializeField] private int requiredLvl;
        [SerializeField] private ItemStat[] primaryStats;

        public int RequiredLvl => requiredLvl;
        public ItemStat[] PrimaryStats => primaryStats;
    }
}
