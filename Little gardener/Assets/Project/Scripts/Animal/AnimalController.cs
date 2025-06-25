using System;
using LittleGardener.Garden;
using LittleGardener.GameManagement;
using UnityEngine;
using LittleGardener.SpriteSorting;

namespace LittleGardener.Animal
{
    public class AnimalController : MonoBehaviour
    {
        private AnimalState _state;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private AudioSource _audioSource;
        private DefaultYSorter _sorter;

        [SerializeField] private int _speed;
        [SerializeField] private AudioClip _eatClip;

        public float Speed { get => _speed; }
        public Vector2 CurrentDirection { get; private set; }

        public static event Action<AnimalController> OnAnimalDestroyed;

        private void Update()
        {
            _state?.Do();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Fence>() != null)
            {
                FlipDirection();
            }
            else if (collision.TryGetComponent<GardenBed>(out GardenBed bed))
            {
                if (!bed.IsFree)
                {
                    SetState(new AnimalWaitForEatState(this, bed));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Fence>() != null)
            {
                ChangeDirection();
            }
        }

        private void OnDestroy()
        {
            GameSettings.OnVolumeChanged -= SetVolume;
            OnAnimalDestroyed?.Invoke(this);
        }

        private void FlipDirection()
        {
            CurrentDirection *= -1;
        }

        private void ChangeDirection()
        {
            Vector2[] directions = { Vector2.down, Vector2.up,  Vector2.right, Vector2.left};
            Vector2 newDir = CurrentDirection;

            newDir = directions[UnityEngine.Random.Range(0, directions.Length)];

            CurrentDirection = newDir;
        }

        public void Init(Vector2 dir)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _sorter = GetComponent<DefaultYSorter>();

            CurrentDirection = dir;
            SetVolume();

            _state = new AnimalRunState(this, transform);
            GameSettings.OnVolumeChanged += SetVolume;
        }

        public void SetState(AnimalState state)
        {
            _state = state;
        }

        public void SetAnimatorTrigger(int hash)
        {
            _animator.SetTrigger(hash);
        }

        public void SetVisualDirection()
        {
            _spriteRenderer.flipX = (CurrentDirection.x < 0 || CurrentDirection.y < 0) ? true : false;
        }

        public void PlayEatAudio()
        {
            _audioSource.PlayOneShot(_eatClip);
        }

        public void SetVolume()
        {
            _audioSource.volume = AudioManager.EffectSoundVolume * GameSettings.GlobalVolumeCoeficient;
        }

        public void Sort()
        {
            _sorter.Sort();
        }
    }
}