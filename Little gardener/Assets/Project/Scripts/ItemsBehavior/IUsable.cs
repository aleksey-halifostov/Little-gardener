using LittleGardener.GameManagement;
using LittleGardener.Garden;
using LittleGardener.GameInventory;

namespace LittleGardener.ItemsBehaviour
{
    public interface IUsable
    {
        public bool Use(IInteractable interactable);
    }
}