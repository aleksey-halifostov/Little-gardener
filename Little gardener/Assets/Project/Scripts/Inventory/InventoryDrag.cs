using UnityEngine;
using UnityEngine.EventSystems;
using LittleGardener.GameManagement;
using LittleGardener.ItemsBehaviour;

namespace LittleGardener.Inventory
{
    [RequireComponent(typeof(InventorySlot))]
    public class InventoryDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Canvas _originalCanvas;
        private InventorySlot _slot;

        protected Vector3 _startPosition;

        [SerializeField] private RectTransform _itemIcon;

        protected virtual void Awake()
        {
            _originalCanvas = GetComponentInParent<Canvas>();
            _slot = GetComponent<InventorySlot>();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _itemIcon.GetComponent<CanvasGroup>().blocksRaycasts = false;
            _startPosition = _itemIcon.position;
            _itemIcon.SetParent(_originalCanvas.transform, true);
            ControlsBlocker.DisablePlayerControls();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            _itemIcon.position = eventData.position;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _itemIcon.GetComponent<CanvasGroup>().blocksRaycasts = true;
            InventorySlot slot = null;

            if (eventData.pointerEnter != null)
            {
                slot = GetContainer(eventData);
            }

            if (slot != null)
            {
                DropToContainer(slot);
            }

            _itemIcon.SetParent(transform, true);
            _itemIcon.position = _startPosition;
            _itemIcon.SetSiblingIndex(0);
            ControlsBlocker.EnablePlayerControls();
        }

        private InventorySlot GetContainer(PointerEventData eventData)
        {
            if (eventData.pointerEnter.TryGetComponent<InventorySlot>(out InventorySlot slot))
            {
                return slot;
            }

            return null;
        }

        private void DropToContainer(InventorySlot detectedSlot)
        {
            if (detectedSlot == _slot) { return; }

            GameItem item = _slot.GetItem();
            int itemAmount = _slot.GetItemAmount();

            _slot.ClearSlot();

            if (!detectedSlot.IsFree)
            {
                _slot.AddItem(detectedSlot.GetItem(), detectedSlot.GetItemAmount());
                detectedSlot.ClearSlot();
            }

            detectedSlot.AddItem(item, itemAmount);
        }
    }
}
