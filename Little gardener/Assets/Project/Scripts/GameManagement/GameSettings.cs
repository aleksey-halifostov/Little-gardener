using System;
using UnityEngine;

namespace LittleGardener.GameManagement
{
    public static class GameSettings
    {  
        private static float _globalVolumeCoefficient = 1f;

        public static float GlobalVolumeCoefficient => _globalVolumeCoefficient;
        public static event Action OnVolumeChanged;


        public static void Init(SettingsSetter setter)
        {
            if (setter == null)
                throw new ArgumentNullException(nameof(setter));

            OnVolumeChanged?.Invoke();
            setter.SetInitialSettings(GlobalVolumeCoefficient);
        }

        public static void ChangeGlobalVolume(float volume)
        {
            _globalVolumeCoefficient = Mathf.Clamp01(volume);
            OnVolumeChanged?.Invoke();
        }
    }
}