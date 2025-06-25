using UnityEngine;
using LittleGardener.GameManagement;

namespace LittleGardener.PlayerControls
{
    public class CameraMover : MonoBehaviour
    {
        private Vector2 _previousTouchPosition = Vector2.zero;
        private Camera _camera;
        private float _cameraZ = -10f;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (TryGetMoveVector(out Vector2 moveVector))
            {
                MoveCamera(moveVector);
            }
        }

        private void MoveCamera(Vector2 moveVector)
        {
            transform.Translate(moveVector);

            transform.position = WorldLimiter.ClampToWorldBounds(transform.position.x, transform.position.y, _cameraZ);
        }

        private bool TryGetMoveVector(out Vector2 moveVector)
        {
            moveVector = default;

            if (!InputManager.TryGetTouch(out Touch touch))
                return false;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _previousTouchPosition = touch.position;
                    return false;
                case TouchPhase.Moved:
                    moveVector = _camera.ScreenToWorldPoint(_previousTouchPosition) - _camera.ScreenToWorldPoint(touch.position);
                    _previousTouchPosition = touch.position;
                    return true;
                default:

                    return false;
            }
        }
    }
}
