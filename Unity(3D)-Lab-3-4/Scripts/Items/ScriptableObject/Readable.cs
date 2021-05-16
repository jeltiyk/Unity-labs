using UnityEngine;

namespace Items.ScriptableObject
{
    [CreateAssetMenu(fileName = "Readable", menuName = "Item/Readable")]
    public class Readable : Item
    {
        [SerializeField] private string text;

        public string Text => text;
    }
}
