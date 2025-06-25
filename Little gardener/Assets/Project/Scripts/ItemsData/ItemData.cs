using UnityEngine;

namespace LittleGardener.ItemsData
{
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private int _maximumAmount;
        [SerializeField] private int _defaultPrice;
        [SerializeField] private bool _isHandle;
        [SerializeField] private bool _hasDurability;
        [SerializeField] private Sprite _uiSprite;

        public string ItemName => _itemName;
        public int MaximumAmount => _maximumAmount;
        public int DefaultPrice => _defaultPrice;
        public bool IsHandle => _isHandle;
        public bool HasDurability => _hasDurability;
        public Sprite UISprite => _uiSprite;
    }
}
