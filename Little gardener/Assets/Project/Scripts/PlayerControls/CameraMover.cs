using UnityEngine;
using LittleGardener.GameManagement;

namespace LittleGardener.PlayerControls
{
    public class CameraMover : MonoBehaviour
    {
        private Vector2 _previousTapPosition;
        private Camera _camera;
        private bool _isDragging = false;
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

            if (!InputManager.TryGetTapPosition(out Vector2 position))
            {
                _isDragging = false;
                return false;
            }
            else if (_isDragging)
            {
                moveVector = _camera.ScreenToWorldPoint(_previousTapPosition) - _camera.ScreenToWorldPoint(position);
                _previousTapPosition = position;
                return true;
            }

            _previousTapPosition = position;
            _isDragging = true;
            return false;
        }
    }
}
