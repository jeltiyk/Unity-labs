namespace Items.ScriptableObject
{
    public enum Items
    {
        Default = 0,
        Scrap = 1,
        Shard = 2,
        SimpleSword = 2001,
        MetalSword = 2003,
        MetalBattleaxe = 2004,
        SpikedHalberd = 2005,
        WoodenShield = 2006,
        SpikedHammer = 2007
    }

    public enum EquipmentType
    {
        Default = 0,
        Weapon = 1,
        Shield = 2,
        Bag = 3,
        Helmet = 4,
        Chest = 5,
        Belt = 6,
        Boots = 7
    }
    
    public enum StatType
    {
        Default = 0,
        Attack = 1,
        Defence = 2,
        CriticalHitChance = 3,
        CriticalHitPower = 4,
    }

    public enum Rarity
    {
        Normal = 1,
        Unusual = 2,
        Rare = 3,
        Epic = 4
    }
    
    public enum IncreaseType
    {
        Value,
        Percent
    }
}
