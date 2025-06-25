using LittleGardener.GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleGardener.Inventory
{
    [RequireComponent(typeof(QuickSlot))]
    public class QuickSlotDrag : InventoryDrag, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private QuickSlot _quickSlot;
        private bool _hasDraggedFarEnough = false;

        protected override void Awake()
        {
            _quickSlot = GetComponent<QuickSlot>();
            base.Awake();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _quickSlot.ActivateSlot();
            base.OnPointerDown(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (!_hasDraggedFarEnough)
            {
                float dragDistance = Vector2.Distance(eventData.position, _startPosition);

                if (dragDistance > 5f)
                {
                    _hasDraggedFarEnough = true;
                    _quickSlot.Deactivate();
                }
                else
                {
                    return;
                }
            }

            base.OnDrag(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _hasDraggedFarEnough = false;

            if (_quickSlot.IsActive)
            {
                ControlsBlocker.DisableCameraMover();
            }
        }
    }
}
