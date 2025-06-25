using UnityEngine;
using LittleGardener.PlayerControls;

namespace LittleGardener.GameManagement
{
    public class GameManager : MonoBehaviour
    {

        private void Awake()
        {
            ControlsBlocker.Init(FindFirstObjectByType<PlayerController>(), FindFirstObjectByType<CameraMover>());
        }
    }
}
