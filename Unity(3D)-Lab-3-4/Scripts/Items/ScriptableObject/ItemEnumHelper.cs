namespace Items.ScriptableObject
{
    public enum Items
    {
        Default = 0,
        Scrap = 1,
        Shard = 2,
        WoodenSword = 3,
        IronSword = 4,
        SilverSword = 5,
        GoldenSword = 6
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
        Agility = 1,
        AttackSpeed = 2,
        Defence = 3,
        Damage = 4,
        Strength = 5
    }

    public enum Rarity
    {
        Normal = 1,
        Unusual = 2,
        Rare = 3,
        Epic = 4
    }
    
    public enum IncreaserType
    {
        Value,
        Percent
    }
}
