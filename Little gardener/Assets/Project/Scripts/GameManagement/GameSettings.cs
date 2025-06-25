using System;

namespace LittleGardener.GameManagement
{
    public static class GameSettings
    {
        private static float _globalVolumeCoeficient = 1f;

        public static float GlobalVolumeCoeficient => _globalVolumeCoeficient;
        public static event Action OnVolumeChanged;

        public static void ChangeGlobalVolume(float volume)
        {
            _globalVolumeCoeficient = volume;
            OnVolumeChanged?.Invoke();
        }
    }
}