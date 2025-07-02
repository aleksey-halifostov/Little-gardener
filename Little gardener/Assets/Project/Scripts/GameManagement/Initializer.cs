using UnityEngine;
using LittleGardener.GameInventory;
using LittleGardener.PlayerControls;

namespace LittleGardener.GameManagement
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Inventory _container;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private SettingsSetter _setter;

        private void Awake()
        {
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            ControlsBlocker.Init(_playerController, _cameraMover);
            GameSettings.Init(_setter);
            ItemFactory.Init(_audioManager, _container);
        }
    }
}
