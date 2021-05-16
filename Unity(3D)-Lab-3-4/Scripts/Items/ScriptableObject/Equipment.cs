using UnityEngine;

namespace Items.ScriptableObject
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
    public class Equipment : ItemStatBase
    {
        [SerializeField] private EquipmentType equipmentType;
        [SerializeField] private Rarity rarityLvl;
        [SerializeField] private ItemStat[] additionalStats;
        
        public EquipmentType EquipmentType => equipmentType;
        public Rarity RarityLvl => rarityLvl;
        public ItemStat[] AdditionalStats => additionalStats;

        public override bool Use(InventoryCell cell)
        {
            if (cell == null) return false;

            return Owner.EquipmentController.Equipments.EquipItem(this, cell);
        }
    }
}
