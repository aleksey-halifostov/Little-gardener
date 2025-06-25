using UnityEngine;
using System.Collections;
using LittleGardener.ItemsBehaviour;
using LittleGardener.SpriteSorting;
using LittleGardener.Building;

namespace LittleGardener.Garden
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GardenBed : BuildingBase, ISortable
    {
        private bool _isWatered = false;
        private PlantItem _currentPlant;
        private Coroutine _wateringCoroutine;
        private Coroutine _growingCoroutine;
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Plant _plant;
        [SerializeField] Sprite _wetBedSprite;
        [SerializeField] Sprite _dryBedSprite;

        public bool IsFree => _currentPlant == null;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private IEnumerator GrowPlant(PlantItem currentPlantItem)
        {
            WaitForSeconds wait = new WaitForSeconds(_currentPlant.GrowTime);

            _plant.SetSprite(_currentPlant.GetFirstProgressSprite());

            while (!_currentPlant.IsGrown)
            {
                yield return wait;
                yield return new WaitUntil(() => _isWatered);

                _plant.SetSprite(_currentPlant.Grow());
            }
        }

        private IEnumerator DryBed()
        {
            yield return new WaitForSeconds(90);

            _isWatered = false;
            _spriteRenderer.sprite = _dryBedSprite;
        }

        public void SetPlant(PlantItem newPlant)
        {
            if (IsFree)
            {
                _currentPlant = newPlant;
                _growingCoroutine = StartCoroutine(GrowPlant(_currentPlant));
            }
        }

        public void WaterBed()
        {
            _isWatered = true;
            _spriteRenderer.sprite = _wetBedSprite;

            if (_wateringCoroutine != null)
            {
                StopCoroutine(_wateringCoroutine);
            }

            _wateringCoroutine = StartCoroutine(DryBed());
        }

        public bool IsReadyToCollect()
        {
            return !IsFree && _currentPlant.IsGrown;
        }

        public PlantItem CheckPlant()
        {
            return _currentPlant;
        }

        public void RemovePlant()
        {
            _currentPlant = null;
            _plant.RemovePlant();

            if (_growingCoroutine != null)
            {
                StopCoroutine(_growingCoroutine);
                _growingCoroutine = null;
            }
        }

        public void Sort()
        {
            _plant.GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 10);
        }
    }
}
