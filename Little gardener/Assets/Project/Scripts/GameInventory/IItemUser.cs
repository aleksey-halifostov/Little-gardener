using LittleGardener.ItemsBehaviour;

namespace LittleGardener.GameInventory
{
    public interface IItemUser
    {
        public void UseItem(IInteractable interactable);
    }
}