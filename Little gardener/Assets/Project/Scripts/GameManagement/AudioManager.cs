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
            GameSettings.OnVolumeChanged += SetVolume;
        }

        private void OnDisable()
        {
            GameSettings.OnVolumeChanged -= SetVolume;
        }

        public void PlayEffectSound(AudioClip clip)
        {
            _effectSoundSource.PlayOneShot(clip);
        }

        public void PlayUISound(AudioClip clip)
        {
            _uiSoundSource.PlayOneShot(clip);
        }

        public void SetVolume()
        {
            _bgSoundSource.volume = BackgroundSoundVolume * GameSettings.GlobalVolumeCoeficient;
            _uiSoundSource.volume = EffectSoundVolume * GameSettings.GlobalVolumeCoeficient;
            _effectSoundSource.volume = EffectSoundVolume * GameSettings.GlobalVolumeCoeficient;
        }
    }
}
