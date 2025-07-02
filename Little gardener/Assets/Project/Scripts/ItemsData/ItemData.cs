using UnityEngine;

namespace LittleGardener.ItemsData
{
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField, Min(1)] private int _maximumAmount;
        [SerializeField, Min(0)] private int _defaultPrice;
        [SerializeField] private bool _isHandle;
        [SerializeField] private Sprite _uiSprite;
        [SerializeField] private ItemType _itemType;

        public string ItemName => _itemName;
        public int MaximumAmount => _maximumAmount;
        public int DefaultPrice => _defaultPrice;
        public bool IsHandle => _isHandle;
        public Sprite UISprite => _uiSprite;
        public ItemType ItemType => _itemType;
    }
}
