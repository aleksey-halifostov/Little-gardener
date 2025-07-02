using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.UI
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Image _volumeImage;

        [SerializeField] private Sprite _onVolumeSprite;
        [SerializeField] private Sprite _offVolumeSprite;

        public void SetVolumeSprite(float currentVolume)
        {
            if (currentVolume == 0)
                _volumeImage.overrideSprite = _offVolumeSprite;
            else
                _volumeImage.overrideSprite = _onVolumeSprite;
        }
    }
}