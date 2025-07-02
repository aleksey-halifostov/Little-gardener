using LittleGardener.UI;
using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.GameManagement
{
    public class SettingsSetter : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private SettingsUI _settingsUI;

        private void Awake()
        {
            _slider.maxValue = 1;
            _slider.minValue = 0;
        }

        public void SetInitialSettings(float value)
        {
            _slider.value = Mathf.Clamp01(value);
            _settingsUI.SetVolumeSprite(value);
        }

        public void SetGlobalVolume()
        {
            GameSettings.ChangeGlobalVolume(_slider.value);
            _settingsUI.SetVolumeSprite(_slider.value);
        }
    }
}