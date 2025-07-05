using UnityEngine;
using LittleGardener.GameInventory;
using LittleGardener.ItemsBehaviour;

namespace LittleGardener.PlayerControls
{
    public class PlayerController : MonoBehaviour
    {
        private IItemUser _handSlot;

        private void Update()
        {
            if (_handSlot != null && InputManager.TryGetClickCollider(out Collider2D collider))
            {
                TryToInteract(collider);
            }
        }

        private void TryToInteract(Collider2D collider)
        {
            if (collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (_handSlot != null)
                {
                    _handSlot.UseItem(interactable);
                }
            }
        }

        public void SetHand(IItemUser activeSlot)
        {
            _handSlot = activeSlot;
        }

        public void ResetHand()
        {
            _handSlot = null;
        }
    }
}
