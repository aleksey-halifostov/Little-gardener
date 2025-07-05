using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleGardener.PlayerControls
{
    public static class InputManager
    {
        private static List<RaycastResult> _raycastResults = new List<RaycastResult>();

        private static bool TryGetClickPosition(out Vector2 position)
        {
            position = default;

#if UNITY_STANDALONE_WIN || UNITY_WEBGL
            if (Input.GetMouseButtonDown(0) && !IsTapOverUI(Input.mousePosition))
            {
                position = Input.mousePosition;
                return true;
            }
#endif

#if UNITY_ANDROID || UNITY_WEBGL
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !IsTapOverUI(touch.position))
                {
                    position = Input.mousePosition;
                    return true;
                }
            }
#endif

            return false;
        }

        private static bool IsTapOverUI(Vector2 pos)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = pos;

            EventSystem.current.RaycastAll(eventData, _raycastResults);

            return _raycastResults.Count > 0;
        }

        public static bool TryGetClickCollider(out Collider2D collider)
        {
            collider = null;

            if (!TryGetClickPosition(out Vector2 position))
                return false;

            Vector2 _rayStartPosition = Camera.main.ScreenToWorldPoint(position);
            RaycastHit2D hit = Physics2D.Raycast(_rayStartPosition, Vector2.zero);
            collider = hit.collider;
            return collider != null;
        }

        public static bool TryGetTapWorldPosition(out Vector2 position)
        {
            position = default;

            if (!TryGetTapPosition(out Vector2 tapPosition))
                return false;

            position = Camera.main.ScreenToWorldPoint(tapPosition);
            return true;
        }

        public static bool TryGetTapPosition(out Vector2 position)
        {
            position = default;

#if UNITY_STANDALONE_WIN || UNITY_WEBGL
            if (Input.GetMouseButton(0) && !IsTapOverUI(Input.mousePosition))
            {
                position = Input.mousePosition;
                return true;
            }
#endif

#if UNITY_ANDROID || UNITY_WEBGL
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && !IsTapOverUI(touch.position))
                {
                    position = Input.mousePosition;
                    return true;
                }
            }
#endif

            return false;
        }

        public static Vector2 GetCameraCenterWorldPosition()
        {
            return Camera.main.transform.position;
        }
    }
}
