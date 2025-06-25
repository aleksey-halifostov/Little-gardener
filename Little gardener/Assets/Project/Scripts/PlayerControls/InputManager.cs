using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleGardener.PlayerControls
{
    public static class InputManager
    {
        private static List<RaycastResult> _raycastResults = new List<RaycastResult>();

        public static bool IsTouchOverUI(Vector2 pos)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = pos;

            EventSystem.current.RaycastAll(eventData, _raycastResults);

            return _raycastResults.Count > 0;
        }

        public static bool TryGetTouchCollider(out Collider2D collider)
        {
            collider = null;

            if (!TryGetTouch(out Touch touch))
                return false;

            if (touch.phase != TouchPhase.Began)
                return false;

            Vector2 _rayStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(_rayStartPosition, Vector2.zero);
            collider = hit.collider;
            return collider != null;
        }

        public static bool TryGetTouchWorldPosition(out Vector2 position)
        {
            position = default;

            if (!TryGetTouch(out Touch touch))
                return false;

            position = Camera.main.ScreenToWorldPoint(touch.position);
            return true;
        }

        public static bool TryGetTouch(out Touch touch)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (!IsTouchOverUI(touch.position))
                {
                    return true;
                }
            }

            touch = default;
            return false;
        }

        public static Vector2 GetCameraCenterWorldPosition()
        {
            return Camera.main.transform.position;
        }
    }
}
