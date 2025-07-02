using UnityEngine;

namespace LittleGardener.GameManagement
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgSoundSource;
        [SerializeField] private AudioSource _uiSoundSource;
        [SerializeField] private AudioSource _effectSoundSource;

        public const float BackgroundSoundVolume = .5f;
        public const float EffectSoundVolume = .8f;

        private void OnEnable()
        {
            GameSettings.OnVolumeChanged += UpdateVolume;
            UpdateVolume();
        }

        private void OnDisable()
        {
            GameSettings.OnVolumeChanged -= UpdateVolume;
        }

        public void PlayEffectSound(AudioClip clip)
        {
            _effectSoundSource.PlayOneShot(clip);
        }

        public void PlayUISound(AudioClip clip)
        {
            _uiSoundSource.PlayOneShot(clip);
        }

        public void UpdateVolume()
        {
            _bgSoundSource.volume = BackgroundSoundVolume * GameSettings.GlobalVolumeCoefficient;
            _uiSoundSource.volume = EffectSoundVolume * GameSettings.GlobalVolumeCoefficient;
            _effectSoundSource.volume = EffectSoundVolume * GameSettings.GlobalVolumeCoefficient;
        }
    }
}
