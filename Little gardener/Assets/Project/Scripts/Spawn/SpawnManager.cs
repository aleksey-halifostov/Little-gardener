using UnityEngine;
using System.Collections.Generic;
using LittleGardener.Animal;
using LittleGardener.GameManagement;
using System;

namespace LittleGardener.Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        private const int _animalMaxCount = 5;
        private List<AnimalController> _animalControllers = new List<AnimalController>();
        
        [SerializeField] private GameObject[] _animalPrefabs;

        public const int XSpawnBound = 30;
        public const int YSpawnBound = 20;

        private void OnEnable()
        {
            AnimalController.OnAnimalDestroyed += RemoveAnimal;
        }

        private void OnDisable()
        {
            AnimalController.OnAnimalDestroyed -= RemoveAnimal;
        }

        private void Update()
        {
            if (_animalControllers.Count < _animalMaxCount)
            {
                List<Action> actions = new() { SpawnTop, SpawnBottom, SpawnLeft, SpawnRight};

                Action spawner = actions[UnityEngine.Random.Range(0, actions.Count)];
                spawner.Invoke();
            }
        }

        private GameObject GetRandomAnimalPrefab()
        {
            return _animalPrefabs[UnityEngine.Random.Range(0, _animalPrefabs.Length - 1)];
        }

        private void AddAnimal(AnimalController controller)
        {
            _animalControllers.Add(controller);
        }

        private void RemoveAnimal(AnimalController animal)
        {
            _animalControllers.Remove(animal);
        }

        private void SpawnRandomAnimal(Vector2 position, Vector2 direction)
        {
            AnimalController animal = Instantiate(GetRandomAnimalPrefab(), position, Quaternion.identity).GetComponent<AnimalController>();
            animal.Init(direction);
            AddAnimal(animal);
        }

        private void SpawnTop()
        {
            Vector2 position = new Vector2(UnityEngine.Random.Range(-WorldLimiter.XWorldMax, WorldLimiter.XWorldMax),
                                           YSpawnBound);
            SpawnRandomAnimal(position, Vector2.down);
        }

        private void SpawnBottom()
        {
            Vector2 position = new Vector2(UnityEngine.Random.Range(-WorldLimiter.XWorldMax, WorldLimiter.XWorldMax),
                                           -YSpawnBound);
            SpawnRandomAnimal(position, Vector2.up);
        }

        private void SpawnLeft()
        {
            Vector2 position = new Vector2(-XSpawnBound,
                                           UnityEngine.Random.Range(-WorldLimiter.YWorldMax, WorldLimiter.YWorldMax));
            SpawnRandomAnimal(position, Vector2.right);
        }

        private void SpawnRight()
        {
            Vector2 position = new Vector2(XSpawnBound,
                                           UnityEngine.Random.Range(-WorldLimiter.YWorldMax, WorldLimiter.YWorldMax));
            SpawnRandomAnimal(position, Vector2.left);
        }
    }
}
