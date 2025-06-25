using System.Collections;
using LittleGardener.PlayerControls;
using UnityEngine;


namespace LittleGardener.GameManagement
{
    public static class ControlsBlocker
    {
        private static PlayerController _playerController;
        private static CameraMover _cameraMover;

        public static IEnumerator EnableCameraMover()
        {
            while (Input.touchCount > 0) { yield return null; }
            
            _cameraMover.enabled = true;
        }

        public static void Init(PlayerController player, CameraMover camera)
        {
            _playerController = player;
            _cameraMover = camera;
        }

        public static void EnablePlayerControls()
        {
            _playerController.enabled = true;
            _cameraMover.enabled=true;
        }

        public static void DisablePlayerControls()
        {
            _playerController.enabled = false;
            DisableCameraMover();
        }

        public static void DisableCameraMover()
        {
            _cameraMover.enabled= false;
        }
    }
}
