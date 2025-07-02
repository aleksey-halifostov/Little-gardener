using LittleGardener.GameManagement;
using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour 
{ 
    public class ToolItem : GameItem
    {
        private ToolData _toolData;

        protected readonly ItemDurabilityController _durabilityController;

        public int CurrentDurability => _durabilityController.CurrentDurability;
        public int MaxDurability => _toolData.MaxDurability;

        protected void UpdateDurability(AudioManager audioManager)
        {
            _durabilityController.DecreaseDurability();
            audioManager.PlayEffectSound(_toolData.ToolSound);
        }

        public ToolItem(ToolData data) : base(data)
        {
            _durabilityController = new ItemDurabilityController(data.MaxDurability);
            _toolData = data;
        }
    }
}