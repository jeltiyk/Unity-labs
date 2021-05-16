using System;

namespace Items.ScriptableObject
{
    [Serializable]
    public class Stat
    {
        public StatType StatType;
        public int Amount;
    }

    [Serializable]
    public class ItemStat : Stat
    {
        public IncreaserType IncreaserType;
    }
}

