using UnityEngine;

namespace Items.ScriptableObject
{
    public abstract class ItemStatBase : Item
    {
        [SerializeField] private int requiredLvl;
        [SerializeField] private StatType[] primaryStats;

        public int RequiredLvl => requiredLvl;
        public StatType[] StatTypes => primaryStats;
    }
}
