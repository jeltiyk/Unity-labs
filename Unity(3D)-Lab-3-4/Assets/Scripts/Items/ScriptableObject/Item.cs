using UnityEngine;

namespace Items.ScriptableObject
{
    public abstract class Item : UnityEngine.ScriptableObject
    {
        [SerializeField] private Items id;
        [SerializeField] private new string name;
        [SerializeField] private string description;
        [SerializeField] private int stack;
        [SerializeField] private Sprite inventoryIcon;
        [SerializeField] private Player owner;

        public Items ID => id;
        public string Name => name;
        public string Description => description;
        public int Stack => stack;
        public Sprite InventoryIcon => inventoryIcon;
        public Player Owner => owner;

        public virtual bool SetOwner(Player player)
        {
            if (player == null) return false;
            
            owner = player;
            return true;
        }
        
        public virtual bool Use(InventoryCell inventoryCell)
        {
            Debug.Log("Item used");
            return true;
        }

        public virtual bool Drop(InventoryCell cell)
        {
            if (cell == null) return false;

            return Owner.InventoryController.Inventory.RemoveItem(cell, true);
        }
    }
}
