using System.Collections;
using LittleGardener.PlayerControls;
using UnityEngine;

namespace LittleGardener.GameManagement
{
    public static class ControlsBlocker
    {
        private static PlayerController _playerController;
        private static CameraMover _cameraMover;
        
        public static void Init(PlayerController playerController, CameraMover cameraMover)
        {
            if (playerController == null)
                throw new System.ArgumentNullException(nameof(playerController));
            if (cameraMover == null)
                throw new System.ArgumentNullException(nameof(cameraMover));

            _playerController = playerController;
            _cameraMover = cameraMover;
        }

        public static IEnumerator EnableCameraAfterTouch()
        {
            while (Input.touchCount > 0) { yield return null; }

            _cameraMover.enabled = true;
        }

        public static void EnableControls()
        {
            _playerController.enabled = true;
            _cameraMover.enabled=true;
        }

        public static void DisableControls()
        {
            _playerController.enabled = false;
            DisableCamera();
        }

        public static void DisableCamera()
        {
            _cameraMover.enabled= false;
        }
    }
}
